//using System.Collections.Generic;
//using System.Linq;
//using Erp.Infrastructure.Identity;
//using Infrastructure.Identity;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.Infrastructure;

//namespace Erp.Infrastructure.Persistence
//{
//    public class Seed
//    {

//        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager, IActionDescriptorCollectionProvider provider)
//        {
//            if (!userManager.Users.Any())
//            {
//                // Create some roles
//                var roles = new List<Role>
//                {
//                    new Role {Name = "Admin"},
//                    new Role {Name = "Employee"},
//                    new Role {Name = "Moderator"}
//                };

//                foreach (var role in roles)
//                {
//                    roleManager.CreateAsync(role).Wait();
//                }

//                //Create admin user
//                var adminUser = new User
//                {
//                    UserName = "Admin",
//                    EmployeeId = "01-25-01-001"
//                };

//                var result = userManager.CreateAsync(adminUser, "admin").Result;

//                if (result.Succeeded)
//                {
//                    var admin = userManager.FindByNameAsync("Admin").Result;
//                    userManager.AddToRolesAsync(admin, new[] { "Admin" });
//                }
//            }

//            List<string> actionEntities = new List<string>();
//            var controllerList = provider.ActionDescriptors.Items.Select(x => x.RouteValues["Controller"]).Distinct().ToList();

//            foreach (var controller in controllerList)
//            {
//                var actionList = provider.ActionDescriptors.Items.Where(c => c.RouteValues["Controller"] == controller).Select(x => x.RouteValues["Action"]).ToList();
//                foreach (var action in actionList)
//                {
//                    string actionEntity = controller + "_" + action;
//                    actionEntities.Add(actionEntity);
//                }
//            }

//            AddActionToRole(actionEntities, roleManager);
//        }

//        private static void AddActionToRole(List<string> entities, RoleManager<Role> roleManager)
//        {

//            var roles = new List<Role>();

//            foreach (var entity in entities)
//            {
//                var role = new Role { Name = entity };
//                roles.Add(role);
//            }


//            var allRole = roleManager.Roles.Where(c => c.Name != "Admin");


//            foreach (var role in roles)
//            {
//                var hasInRole = allRole.FirstOrDefault(c => c.Name == role.Name);
//                if (hasInRole != null)
//                    continue;
//                roleManager.CreateAsync(role).Wait();

//            }
//        }
//    }
//}
