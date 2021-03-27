using EducationSystem.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    public static class ControllerBaseExtension
    {

        public static List<int> SupplyUserGroupsList(this ControllerBase controllerBase, IGroupService _groupService)
        {

            List<int> _userGroups = new List<int>();
            int _userId = Convert.ToInt32(controllerBase.User.FindFirst("id").Value);
            if (!controllerBase.User.IsInRole("Администратор"))
            {
                if (controllerBase.User.IsInRole("Преподаватель"))
                    _userGroups.AddRange(_groupService.GetGroupsByTeacherId(_userId));
                if (controllerBase.User.IsInRole("Тьютор"))
                    _userGroups.AddRange(_groupService.GetGroupsByTutorId(_userId));
                if (controllerBase.User.IsInRole("Студент"))
                    _userGroups.AddRange(_groupService.GetGroupsByStudentId(_userId));

            }
            return _userGroups;
        }
    }
}
