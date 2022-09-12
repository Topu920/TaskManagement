using Application.Requests.MemberInfo;
using Domain.Entities.Models.HrmsModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TaskApp
{
    public class MemberInfoService : HrmBaseRepository<HumanResourceEmployeeBasic>, IMemberInfoService
    {

        public MemberInfoService(CoreERPContext coreErpContext) : base(coreErpContext)
        {

        }

        public async Task<List<MemberInfoDto>> GetMemberList(string requestId)
        {
            try
            {
                var cmnDpt = DbContext.CommonDepartments.ToList();
                var cmnDesign = DbContext.CommonDesignations.ToList();
                var data = await DbContext.HumanResourceEmployeeBasics.Where(a => a.EmpStatusId == 1 && (a.DepartmentId==2||a.DepartmentId==1 || a.EmpCode=="006117")).AsNoTracking()
                    .Select(a => new MemberInfoDto
                    {
                        EmpId = a.EmpId,
                        Name = a.Name,
                        EmpCode = a.EmpCode,
                        DesignationId = a.DesignationId,
                        DepartmentId = a.DepartmentId,
                        DepartmentName = GetDptName(cmnDpt, (int)a.DepartmentId),
                        DesignationName = GetDesignName(cmnDesign, (int)a.DesignationId),


                    }).OrderBy(x=>x.Name).ToListAsync();

                List<MemberInfoDto> model = new();
                foreach (var v in data)
                {
                    v.FullInfoLine = v.EmpCode + ", " + v.Name + ", " + v.DepartmentName + ", " + v.DesignationName;

                    model.Add(v);



                }

                #region comment

                //if (!String.IsNullOrEmpty(requestId) && data != null)
                //{
                //    requestId = requestId.ToUpper();

                //    foreach (var v in data)
                //    {
                //        string str = v.EmpCode + "/" + v.Name + "/" + v.DepartmentName + "/" + v.DesignationName ;
                //        if (str.ToUpper().Contains(requestId.Trim()))
                //        {
                //            model.Add(v);
                //        }


                //    }


                //}
                //else
                //{
                //    model = data;
                //}
                //  return model.Take(100).ToList();

                #endregion

                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }




        public MemberInfoDto GetMember(long? memberId)
        {
            try
            {
                var cmnDpt = DbContext.CommonDepartments.ToList();
                var cmnDesign = DbContext.CommonDesignations.ToList();
                var contact = DbContext.HumanResourceEmployeeContacts.ToList();
                var data = DbContext.HumanResourceEmployeeBasics.Where(a => a.EmpStatusId == 1 && a.EmpId == memberId).AsNoTracking()
                    .Select(a => new MemberInfoDto
                    {
                        EmpId = a.EmpId,
                        Name = a.Name,
                        EmpCode = a.EmpCode,
                        DesignationId = a.DesignationId,
                        DepartmentId = a.DepartmentId,
                        DepartmentName = GetDptName(cmnDpt, (int)a.DepartmentId),
                        DesignationName = GetDesignName(cmnDesign, (int)a.DesignationId),
                        EmailAddress =  GetEmail(contact, (int)a.EmpId),


                    }).FirstOrDefault();


                return data ?? new MemberInfoDto();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static string GetEmail(IEnumerable<HumanResourceEmployeeContact> contact, int aEmpId)
        {
            var email = contact.FirstOrDefault(c => c.EmpId == aEmpId)?.EmailOffice;
            return email ?? "";
        }

        private static string GetDesignName(IEnumerable<CommonDesignation> cmnDesign, int aDesignationId)
        {
            string? designationName = cmnDesign.FirstOrDefault(a => a.DesignationId == aDesignationId)?.DesignationName;

            return designationName ?? "";
        }

        private static string GetDptName(IEnumerable<CommonDepartment> cmnDpt, int aDepartmentId)
        {
            string? departmentName = cmnDpt.FirstOrDefault(a => a.DepartmentId == aDepartmentId)?.DepartmentName;

            return departmentName ?? "";
        }
    }
}
