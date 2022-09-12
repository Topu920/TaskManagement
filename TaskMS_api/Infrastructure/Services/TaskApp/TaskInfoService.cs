using Application.Requests.GroupInfo;
using Application.Requests.MemberInfo;
using Application.Requests.StatuesInfo;
using Application.Requests.TaskInfo;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TaskApp
{
    public class TaskInfoService : BaseRepository<ProjectTask>, ITaskInfoService
    {
        private readonly IMemberInfoService _memberInfoService;
        private readonly IGroupInfoService _groupInfoService;
        private readonly IStatuesService _statuesService;
        public TaskInfoService(TaskManagementContext dbContext, IMemberInfoService memberInfoService, IGroupInfoService groupInfoService, IStatuesService statuesService) : base(dbContext)
        {
            _memberInfoService = memberInfoService;
            _groupInfoService = groupInfoService;
            _statuesService = statuesService;
        }

        public async Task<List<TaskInfoDto>> GetAllTaskInfo()
        {
            try
            {
                var memberList = await _memberInfoService.GetMemberList("");
                var groupList = await _groupInfoService.GetAllGroupList();
                var statuesList = await _statuesService.GetStatuesByFlagNo(2);
                var taskAssign = await DbContext.TaskAssignments.ToListAsync();
                var data = await DbContext.ProjectTasks
                    .Include(x => x.Project)
                    .Include(x => x.Status)
                    .Select(x => new TaskInfoDto()
                    {
                        TaskId = x.TaskId,
                        TaskName = x.TaskName,
                        ProjectId = x.ProjectId,
                        ProjectName = x.Project.ProjectName,
                        StatusId = x.StatusId,
                        StatueName = x.Status.StatusName,
                        TaskDescription = x.TaskDescription ?? "",
                        Eddate = x.Eddate,
                        StartingDate = x.StartingDate,
                        FinishingDate = x.FinishingDate,
                        FinishedBy = x.FinishedBy,
                        CreateBy = x.CreateBy,
                        CreateDate = x.CreateDate,
                        MemberInfo = GetAssignMemberList(memberList, taskAssign, x.TaskId),
                        GroupInfoDto = GetAssignGroupList(groupList, taskAssign, x.TaskId,x.CreateBy),
                        StatuesInfoDto = GetStatues(statuesList, x.StatusId),
                        IsActive = x.Project.IsActive
                    }).OrderByDescending(x => x.CreateDate)
                    .Where(p=>p.IsActive!=false)
                    .ToListAsync();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        public async Task<List<TaskInfoDto>> GetAllTaskInfoByProjectId(Guid requestId)
        {
            try
            {
                var taskAssign = await DbContext.TaskAssignments.Where(x => x.IsUserActive == true).ToListAsync();
                var memberList = await _memberInfoService.GetMemberList("");
                var groupList = await _groupInfoService.GetAllGroupList();
                var statuesList = await _statuesService.GetStatuesByFlagNo(2);
                var data = await DbContext.ProjectTasks.Where(x => x.ProjectId == requestId)
                    .Include(x => x.Project)
                    .Include(x => x.Status)
                    .Select(x => new TaskInfoDto()
                    {
                        TaskId = x.TaskId,
                        TaskName = x.TaskName,
                        ProjectId = x.ProjectId,
                        ProjectName = x.Project.ProjectName,
                        ProjectDescription = x.Project.ProjectDescription ?? "",
                        StatusId = x.StatusId,
                        StatueName = x.Status.StatusName,
                        TaskDescription = x.TaskDescription ?? "",
                        Eddate = x.Eddate,
                        StartingDate = x.StartingDate,
                        FinishingDate = x.FinishingDate,
                        FinishedBy = x.FinishedBy,
                        CreateBy = x.CreateBy,
                        CreateDate = x.CreateDate,
                        MemberInfo = GetAssignMemberList(memberList, taskAssign, x.TaskId),
                        GroupInfoDto = GetAssignGroupList(groupList, taskAssign, x.TaskId,x.CreateBy),
                        StatuesInfoDto = GetStatues(statuesList, x.StatusId),
                        IsActive = x.Project.IsActive
                    }).OrderByDescending(x => x.CreateDate)
                    .Where(p => p.IsActive != false)
                    .ToListAsync();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //public void SaveMember(long? requestMemberId, Guid taskId)
        //{
        //    var check = _dbContext.TaskAssignments.FirstOrDefault(x=>x.TaskId==taskId && x.EmpId==requestMemberId);
        //    if(check!=null)
        //    {

        //    }
        //}

        //public void SaveGroup(Guid? requestGroupId,  Guid taskId)
        //{
        //    var check = _dbContext.TaskAssignments.FirstOrDefault(x => x.TaskId == taskId && x.GroupId == requestGroupId);
        //}

        private static List<GroupInfoDto> GetAssignGroupList(IReadOnlyCollection<GroupInfoDto> groupList, IEnumerable<TaskAssignment> taskAssign, Guid taskId, long? createBy)
        {
            groupList = groupList.Where(m => m.CreateBy == createBy || m.IsPrivate == "N").ToList();
            var model = taskAssign.Where(t => t.TaskId == taskId).ToList();
            List<GroupInfoDto> data= model.Select(item => groupList.FirstOrDefault(m => m.GroupId == item.GroupId )).Where(check => check != null).ToList()!;
            return data;
        }

        private static List<MemberInfoDto> GetAssignMemberList(IReadOnlyCollection<MemberInfoDto> memberList, IEnumerable<TaskAssignment> taskAssign, Guid taskId)
        {
            var model = taskAssign.Where(t => t.TaskId == taskId).ToList();
            return model.Select(item => memberList.FirstOrDefault(m => m.EmpId == item.EmpId)).Where(check => check != null).ToList()!;
        }
        private static StatuesInfoDto GetStatues(IEnumerable<StatuesInfoDto> statuesList, Guid statusId)
        {
            var model = statuesList.FirstOrDefault(x => x.StatusId == statusId);
            return model ?? new StatuesInfoDto();
        }

        //public void SaveMember(IEnumerable<MemberInfoDto> requestMemberInfo, Guid taskTaskId)
        //{
        //    var removeList = DbContext.TaskAssignments.Where(x =>
        //        x.TaskId == taskTaskId && x.EmpId != null).ToList();
        //    var saveList = requestMemberInfo.Select(item => new TaskAssignment() { TaskAssignId = new Guid(), TaskId = taskTaskId, EmpId = item.EmpId, CreateDate = DateTime.Now }).ToList();

        //    if (removeList.Count > 0)
        //    {
        //        DbContext.TaskAssignments.RemoveRange(removeList);
        //        DbContext.SaveChanges();
        //    }

        //    if (saveList.Count <= 0)
        //    {
        //        return;
        //    }

        //    DbContext.TaskAssignments.AddRange(saveList);
        //    DbContext.SaveChanges();
        //    SaveEmail(requestMemberInfo,taskTaskId);
        //}
        public void SaveMember(IEnumerable<MemberInfoDto> requestMemberInfo, Guid taskTaskId)
        {
            var previousMember = DbContext.TaskAssignments.Where(x => x.TaskId == taskTaskId && x.EmpId != null && x.IsUserActive == true).ToList();

            if (requestMemberInfo.Count() == 0 && previousMember.Count == 0)
            {
                return;
            }
            else
            {
                if (previousMember.Count == 0)
                {
                    var saveList = requestMemberInfo.Select(item => new TaskAssignment() { TaskAssignId = new Guid(), TaskId = taskTaskId, EmpId = item.EmpId, CreateDate = DateTime.Now, IsUserActive=item.IsUserActive }).ToList();


                    DbContext.TaskAssignments.AddRange(saveList);
                    DbContext.SaveChanges();
                    SaveEmail(requestMemberInfo, taskTaskId);
                    return;
                }
                else
                {
                    var findRemovedMembers = previousMember.Where(p => !requestMemberInfo.Any(r => r.EmpId == p.EmpId)).ToList();
                    if (findRemovedMembers.Any())
                    {
                        //deactive user 
                        foreach (var item in findRemovedMembers)
                        {
                            item.IsUserActive = false;
                        }
                        DbContext.TaskAssignments.UpdateRange(findRemovedMembers);
                        DbContext.SaveChanges();

                    }
                    var findNewMember = requestMemberInfo.Where(r => !previousMember.Any(p => p.EmpId == r.EmpId)).ToList();
                    if (findNewMember.Any())
                    {
                        var newMember = findNewMember.Select(item => new TaskAssignment() { TaskAssignId = new Guid(), TaskId = taskTaskId, EmpId = item.EmpId, CreateDate = DateTime.Now, IsUserActive = item.IsUserActive }).ToList();


                        DbContext.TaskAssignments.AddRange(newMember);
                        DbContext.SaveChanges();

                        SaveEmail(findNewMember, taskTaskId);
                    }

                }
            }
        }

        //public void SaveGroup(IEnumerable<GroupInfoDto> requestGroupInfoDto, Guid taskTaskId)
        //{
        //    var removeList = DbContext.TaskAssignments.Where(x =>
        //        x.TaskId == taskTaskId && x.GroupId != null).ToList();
        //    var saveList = requestGroupInfoDto.Select(item => new TaskAssignment() { TaskAssignId = new Guid(), TaskId = taskTaskId, GroupId = item.GroupId, CreateDate = DateTime.Now }).ToList();

        //    if (removeList.Count > 0)
        //    {
        //        DbContext.TaskAssignments.RemoveRange(removeList);
        //        DbContext.SaveChanges();
        //    }

        //    if (saveList.Count <= 0)
        //    {
        //        return;
        //    }

        //    DbContext.TaskAssignments.AddRange(saveList);
        //    DbContext.SaveChanges();
        //    GetMemberForEmail(requestGroupInfoDto, taskTaskId);
        //}

        public void SaveGroup(IEnumerable<GroupInfoDto> requestGroupInfoDto, Guid taskTaskId)
        {
            var previousGroup = DbContext.TaskAssignments.Where(x => x.TaskId == taskTaskId && x.GroupId != null && x.IsUserActive == true).ToList();

            if (requestGroupInfoDto.Count() == 0 && previousGroup.Count == 0)
            {
                return;
            }
            else
            {
                if (previousGroup.Count == 0)
                {
                    var saveList = requestGroupInfoDto.Select(item => new TaskAssignment() { TaskAssignId = new Guid(), TaskId = taskTaskId, GroupId = item.GroupId, CreateDate = DateTime.Now, IsUserActive = item.IsUserActive }).ToList();

                    DbContext.TaskAssignments.AddRange(saveList);
                    DbContext.SaveChanges();
                    GetMemberForEmail(requestGroupInfoDto, taskTaskId);
                    return;
                }
                else
                {
                    var findRemovedGroup = previousGroup.Where(p => !requestGroupInfoDto.Any(r => r.GroupId == p.GroupId)).ToList();
                    if (findRemovedGroup.Any())
                    {
                        //deactive user 
                        foreach (var item in findRemovedGroup)
                        {
                            item.IsUserActive = false;
                        }
                        DbContext.TaskAssignments.UpdateRange(findRemovedGroup);
                        DbContext.SaveChanges();

                    }
                    var findNewGroup = requestGroupInfoDto.Where(r => !previousGroup.Any(p => p.GroupId == r.GroupId)).ToList();
                    if (findNewGroup.Any())
                    {
                        var newGroup = findNewGroup.Select(item => new TaskAssignment() { TaskAssignId = new Guid(), TaskId = taskTaskId, GroupId = item.GroupId, CreateDate = DateTime.Now, IsUserActive = item.IsUserActive }).ToList();

                        DbContext.TaskAssignments.AddRange(newGroup);
                        DbContext.SaveChanges();
                        GetMemberForEmail(findNewGroup, taskTaskId);
                    }

                }
            }
        }

        public async Task<List<TaskInfoDto>> GetAllTaskListByUser(long requestEmpId)
        {
            var taskAssignList = await DbContext.TaskAssignments.Where(x => x.EmpId == requestEmpId && x.IsUserActive == true).ToListAsync();
            var groupList = await DbContext.GroupMemberDetails.Where(x => x.MemberUserId == requestEmpId).ToListAsync();
            List<TaskAssignment> groupTaskList = new();
            List<TaskInfoDto> taskList = new();

            foreach (var it in taskAssignList)
            {
                var model = await DbContext.ProjectTasks
                    .Include(x => x.Project)
                    .Include(x => x.Status)
                    .FirstOrDefaultAsync(x => x.TaskId == it.TaskId);
                if (model == null)
                {
                    continue;
                }

                var check = taskList.FirstOrDefault(t => t.TaskId == model.TaskId);
                if (check != null)
                {
                    continue;
                }

                TaskInfoDto task = new()
                {
                    TaskId = model.TaskId,
                    TaskName = model.TaskName,
                    ProjectId = model.ProjectId,
                    ProjectName = model.Project.ProjectName,
                    ProjectDescription = model.Project.ProjectDescription ?? "",
                    StatusId = model.StatusId,
                    StatueName = model.Status.StatusName,
                    TaskDescription = model.TaskDescription ?? "",
                    Eddate = model.Eddate,
                    StartingDate = model.StartingDate,
                    FinishingDate = model.FinishingDate,
                    FinishedBy = model.FinishedBy,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    IsActive = model.Project.IsActive
                };
                taskList.Add(task);
            }
            foreach (var it in groupList)
            {
                var taskAssign = await DbContext.TaskAssignments.Where(g => g.GroupId == it.GroupId).ToListAsync();
                groupTaskList.AddRange(taskAssign);
            }
            foreach (var it in groupTaskList)
            {
                var model = await DbContext.ProjectTasks
                    .Include(x => x.Project)
                    .Include(x => x.Status)
                    .FirstOrDefaultAsync(x => x.TaskId == it.TaskId);
                if (model == null)
                {
                    continue;
                }

                var check = taskList.FirstOrDefault(t => t.TaskId == model.TaskId);
                if (check != null)
                {
                    continue;
                }

                TaskInfoDto task = new()
                {
                    TaskId = model.TaskId,
                    TaskName = model.TaskName,
                    ProjectId = model.ProjectId,
                    ProjectName = model.Project.ProjectName,
                    ProjectDescription = model.Project.ProjectDescription ?? "",
                    StatusId = model.StatusId,
                    StatueName = model.Status.StatusName,
                    TaskDescription = model.TaskDescription ?? "",
                    Eddate = model.Eddate,
                    StartingDate = model.StartingDate,
                    FinishingDate = model.FinishingDate,
                    FinishedBy = model.FinishedBy,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    IsActive = model.Project.IsActive
                };
                taskList.Add(task);
            }

            return taskList.Where(p => p.IsActive != false).OrderByDescending(x=>x.CreateDate).ToList();

        }

        public async Task<List<TaskInfoDto>> GetOverAllTaskListByUser(long requestEmpId)
        {
            var taskList = new List<TaskInfoDto>();
            var taskAssign = await DbContext.TaskAssignments.Where(x => x.IsUserActive == true).ToListAsync();
            var memberList = await _memberInfoService.GetMemberList("");
            var groupList = await _groupInfoService.GetAllGroupList();
            var statuesList = await _statuesService.GetStatuesByFlagNo(2);
            var assignTaskList = await GetAllTaskListByUser(requestEmpId);
            var createdTask = await DbContext.ProjectTasks.Where(t => t.CreateBy == requestEmpId)
                .Include(x => x.Project)
                .Include(x => x.Status)
                .Select(t=>new TaskInfoDto()
                {
                    TaskId = t.TaskId,
                    TaskName = t.TaskName,
                    ProjectId = t.ProjectId,
                    ProjectName = t.Project.ProjectName,
                    ProjectDescription = t.Project.ProjectDescription ?? "",
                    StatusId = t.StatusId,
                    StatueName = t.Status.StatusName,
                    TaskDescription = t.TaskDescription ?? "",
                    Eddate = t.Eddate,
                    StartingDate = t.StartingDate,
                    FinishingDate = t.FinishingDate,
                    FinishedBy = t.FinishedBy,
                    CreateBy = t.CreateBy,
                    CreateDate = t.CreateDate,
                    IsActive = t.Project.IsActive
                }).OrderByDescending(t=>t.CreateBy)
                .ToListAsync();
            
            taskList.AddRange(createdTask);
            taskList.AddRange(from task in assignTaskList let check = createdTask.Any(x => x.TaskId == task.TaskId) where !check select task);
            foreach (var item in taskList)
            {
                item.MemberInfo = GetAssignMemberList(memberList, taskAssign, item.TaskId);
                item.GroupInfoDto = GetAssignGroupList(groupList, taskAssign, item.TaskId, item.CreateBy);
                item.StatuesInfoDto = GetStatues(statuesList, item.StatusId);
                item.CreateByName = _memberInfoService.GetMember(item.CreateBy).Name; 

            }
            return taskList.Where(p => p.IsActive != false).OrderByDescending(x => x.CreateDate).ToList();
        }

        
        private void SaveEmail(IEnumerable<MemberInfoDto> requestMemberInfo, Guid taskTaskId)
        {
            var member = new List<MemberInfoDto>();
            var task = DbContext.ProjectTasks
                .Include(x => x.Project).FirstOrDefault(x=>x.TaskId==taskTaskId);
            var sender = _memberInfoService.GetMember(task?.CreateBy);

            foreach (var mem in requestMemberInfo)
            {
                var receiver = _memberInfoService.GetMember(mem.EmpId);

                var emailed = new EmailTracker()
                {
                    TaskId = taskTaskId,
                    SenderId = sender.EmpId,
                    ReceiverId = receiver.EmpId,
                    SenderEmailAddress = sender.EmailAddress??"",
                    RecieverEmailAddress = receiver.EmailAddress??"",
                    EmailMessage = $@"<div>
                        <p> Dear <strong> {receiver.Name} </strong>,</p>
                    <p> You Have Assigned A New Task <strong> {task?.TaskName} </strong>
                    On <strong> {task?.Project.ProjectName} </strong> Project. Please Login Your Task Management System Account.
                    </p>
                    <br>
                    <p> Sincerely,</p>
                    <p> {sender.Name} </p>
                    <br>
                    <p> Please click on this link for further process: <a href = 'http://202.22.203.87:607'> http://202.22.203.87:607</a></p>

                    </div>"
                };

                DbContext.EmailTrackers.Add(emailed);
                DbContext.SaveChanges();
            }


        }
        private void GetMemberForEmail(IEnumerable<GroupInfoDto> requestGroupInfoDto, Guid taskTaskId)
        {
            var member = new List<MemberInfoDto>();
            var assinmember = new List<GroupMemberDetail>();
            foreach (var g in requestGroupInfoDto)
            {
                 assinmember.AddRange( DbContext.GroupMemberDetails.Where(x => x.GroupId == g.GroupId).ToList());

            }

            foreach (var item in assinmember)
            {
                member.Add(_memberInfoService.GetMember(item.MemberUserId));
            }
            SaveEmail(member, taskTaskId);

        }
    }
}
