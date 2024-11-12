using DAL;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SecurityTestingTests
{
    public class TestsDAL
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task UserRepository_GetAllAsync_ShouldReturnAllUsers()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var users = await repo.GetAllAsync();
            Assert.AreEqual(2, users.Count);
        }

        [Test]
        public async Task UserRepository_GetOneAsync_ShouldReturnUserById()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var existingUser = await context.Users.FirstAsync();
            var user = await repo.GetOneAsync(existingUser.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual(existingUser.UserName, user.UserName);
        }

        [Test]
        public async Task UserRepository_GetOneAsync_ShouldReturnNullIfUserNotFound()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var user = await repo.GetOneAsync(Guid.NewGuid());
            Assert.IsNull(user);
        }

        [Test]
        public async Task UserRepository_GetByUsername_ShouldReturnUserByUserName()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var user = await repo.GetByUsername("TestUser1");
            Assert.IsNotNull(user);
            Assert.AreEqual("TestUser1", user.UserName);
        }

        [Test]
        public async Task UserRepository_GetByUsername_ShouldReturnNullIfUserNameNotFound()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var user = await repo.GetByUsername("NonExistentUser");
            Assert.IsNull(user);
        }

        [Test]
        public async Task UserRepository_CreateAsync_ShouldAddNewUser()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var newUser = new User { UserName = "NewUser", Password = "NewPassword" };
            await repo.CreateAsync(newUser);
            await context.SaveChangesAsync();

            var createdUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "NewUser");
            Assert.IsNotNull(createdUser);
            Assert.AreEqual("NewUser", createdUser.UserName);
        }

        [Test]
        public async Task UserRepository_UpdateAsync_ShouldModifyUser()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var existingUser = await context.Users.FirstAsync();
            existingUser.Password = "UpdatedPassword";
            await repo.UpdateAsync(existingUser);
            await context.SaveChangesAsync();

            var updatedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == existingUser.Id);
            Assert.AreEqual("UpdatedPassword", updatedUser.Password);
        }

        [Test]
        public async Task UserRepository_RemoveAsync_ShouldDeleteUser()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var userToRemove = await context.Users.FirstAsync();
            await repo.RemoveAsync(userToRemove.Id);
            await context.SaveChangesAsync();

            var deletedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userToRemove.Id);
            Assert.IsNull(deletedUser);
        }

        [Test]
        public async Task UserRepository_CreateAsync_ShouldGenerateNewGuidForUser()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var newUser = new User { UserName = "UniqueUser", Password = "Password123" };
            await repo.CreateAsync(newUser);
            await context.SaveChangesAsync();

            Assert.AreNotEqual(Guid.Empty, newUser.Id);
        }

        [Test]
        public async Task UserRepository_UpdateAsync_ShouldThrowExceptionForNonexistentUser()
        {
            var context = new TestingDBContext(UnitTestHelper.GetUnitTestDbOptions());
            var repo = new UserRepository(context);

            var nonExistentUser = new User { Id = Guid.NewGuid(), UserName = "FakeUser", Password = "FakePassword" };
            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                await repo.UpdateAsync(nonExistentUser);
                await context.SaveChangesAsync();
            });
        }

    }
}