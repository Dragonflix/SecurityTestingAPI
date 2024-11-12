using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityTestingTests
{
    public class UnitTestHelper
    {
        public static DbContextOptions<TestingDBContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<TestingDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new TestingDBContext(options))
            {
                SeedData(context);  
            }

            return options;
        }

        public static void SeedData(TestingDBContext context)
        {
            var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
            var userRole = new Role { Id = Guid.NewGuid(), Name = "User" };

            context.Roles.AddRange(adminRole, userRole);

            context.Users.AddRange(
                new User { Id = Guid.NewGuid(), UserName = "TestUser1", Password = "Password1", Roles = new List<Role> { adminRole } },
                new User { Id = Guid.NewGuid(), UserName = "TestUser2", Password = "Password2", Roles = new List<Role> { userRole } });
            context.SaveChanges();
        }
    }
}
