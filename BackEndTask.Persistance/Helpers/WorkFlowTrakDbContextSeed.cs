using BackEndTask.Data.Entites;
using BackEndTask.Data.Utilities;
using BackEndTask.Persistance.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Persistance.Helpers
{
    public class WorkFlowTrakDbContextSeed
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager, ILogger<WorkFlowTrakDbContextSeed> logger)
        {
            try
            {
                // Example: Seed default roles
                var rols = new List<string>
                {
                    Roles.EMPLOYEE,Roles.MANAGER,Roles.FINANCE
                };
                foreach (var role in rols)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new ApplicationRole
                        {
                            Name =role
                        });
                    }
                }
                logger.LogInformation("Database seeding completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during database seeding");
                throw;
            }
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, ILogger<WorkFlowTrakDbContextSeed> logger)
        {
            try
            {

                if (!userManager.Users.Any())
                {
                    var manger = new ApplicationUser
                    {
                        UserName="MANAGER",

                    };
                    var employee = new ApplicationUser
                    {
                        UserName="Employee1",

                    };

                    await userManager.CreateAsync(manger, "11@@22##aaAA");
                    await userManager.AddToRoleAsync(manger, Roles.MANAGER);
                    await userManager.CreateAsync(employee, "11@@22##aaAA");
                    await userManager.AddToRoleAsync(employee, Roles.EMPLOYEE);
                    logger.LogInformation("Users seeding completed.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during Users seeding");
                throw;
            }
        }
    }

}
