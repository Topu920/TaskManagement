//using Microsoft.AspNetCore.Mvc.Infrastructure;

//namespace Erp.Infrastructure.Persistence
//{
//    public class SeedActions
//    {
//        public static void SeedUsers(IActionDescriptorCollectionProvider provider, ApplicationDbContext _dbContext)
//        {

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

//            //AddActionToRole(actionEntities);

//            //Action Data Save

//            var actions = new List<ActionModel>();
//            foreach (var entity in actionEntities)
//            {
//                var action = new ActionModel { ActionName = entity };
//                actions.Add(action);
//            }

//            foreach (var action in actions)
//            {
//                var hasInAction = _dbContext.ActionsList.FirstOrDefault(c => c.ActionName == action.ActionName);
//                if (hasInAction != null)
//                    continue;
//                _dbContext.ActionsList.AddAsync(action).Wait();
//            }
//            _dbContext.SaveChangesAsync();
//            _dbContext.Dispose();
//        }

//        //private static void AddActionToRole(List<string> entities)
//        //{
//        //    ApplicationDbContext _dbContext = new ApplicationDbContext();
//        //    var actions = new List<ActionModel>();

//        //    foreach (var entity in entities)
//        //    {
//        //        var action = new ActionModel { ActionName = entity };
//        //        actions.Add(action);
//        //    }


//        //    //var allAction = _dbContext.ActionsList.Where(x=>x.ActionName!= "Admin");

//        //    foreach (var action in actions)
//        //    {
//        //        var hasInAction = _dbContext.ActionsList.FirstOrDefault(c => c.ActionName == action.ActionName);
//        //        if (hasInAction != null)
//        //            continue;
//        //        _dbContext.ActionsList.AddAsync(action).Wait();
//        //    }
//        //    var result = _dbContext.SaveChangesAsync();
//        //    _dbContext.Dispose();
//        //}
//    }
//}
