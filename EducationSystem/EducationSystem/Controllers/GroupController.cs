using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
using System;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EducationSystem.Core.CustomExceptions;
using Microsoft.AspNetCore.Http;
using EducationSystem.API.Controllers;
using EducationSystem.Core.Enums;

namespace EducationSystem.Controllers
{
    // https://localhost:44365/api/group/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private IGroupService _service;
        private ICourseService _courseService;
        private ILessonService _lessonService;
        private IUserService _userService;
        private IMaterialService _materialService;
        private IMapper _mapper;
        public GroupController(IMapper mapper, IGroupService groupService, IMaterialService materialService,
            ICourseService courseService, ILessonService lessonService, IUserService userService)
        {
            _service = groupService;
            _courseService = courseService;
            _lessonService = lessonService;
            _userService = userService;
            _materialService = materialService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the groups with their courses and groupStatuses
        /// </summary>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<List<GroupOutputModel>> GetGroups()
        {
            var groups = _service.GetGroups();
            if (groups.Count==0)
                return NotFound("Группы еще не созданы");

            var result = _mapper.Map<List<GroupOutputModel>>(groups);

            return Ok(result);
        }

        /// <summary>
        /// Gets only one group by its id with its course and groupStatus
        /// </summary>
        /// <param name="id"> is used to find necessary group</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/3
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<GroupOutputModel> GetGroupById(int id)
        {
            var group = _service.GetGroupById(id);
            if (group == null)
                return NotFound($"группа c id {id} не найдена");

            var userGroup = this.SupplyUserGroupsList(_service);
            if (!User.IsInRole("Администратор") && !userGroup.Contains(group.Id))
            {
                return Forbid($"Пользователь не связан с группой {group.Id}");
            }
            GroupWithUsersOutputModel result = _mapper.Map<GroupWithUsersOutputModel>(group);
            return Ok(result);
            
        }

        /// <summary>
        /// Gets a report with number of lessons for groups to complete the theme
        /// </summary>
        /// <param name="themeId"> is used to find necessary groups by theme-id</param>
        /// <returns>Returns the report</returns>
        // https://localhost:44365/api/group/3
        [ProducesResponseType(typeof(List<NumberOfLessonsForGroupToCompleteTheThemeOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("by-theme/{themeId}")]
        [Authorize]
        public ActionResult<List<NumberOfLessonsForGroupToCompleteTheThemeOutputModel>> GetGroupsByThemeId(int themeId)
        {
            if (_courseService.GetThemeById(themeId) == null)
                return NotFound($"Тема с id:{themeId} не найдена");

            var groups = _service.GetGroupByThemeId(themeId);
            List<NumberOfLessonsForGroupToCompleteTheThemeOutputModel> result = _mapper.Map<List<NumberOfLessonsForGroupToCompleteTheThemeOutputModel>>(groups);
            return Ok(result);
        }

        /// <summary>
        /// Gets all the groups that do not have a tutor with their courses and groupStatuses
        /// </summary>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/without-tutors
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("without-tutors")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<List<GroupOutputModel>> GetGroupsWithoutTutors()
        {
            var result = _mapper.Map<List<GroupOutputModel>>(_service.GetGroupsWithoutTutors());
            return Ok(result);
        }

        /// <summary>
        /// Gets only one group by its id with its course and list of themes
        /// </summary>
        /// <param name="groupId"> is used to find necessary groups by id</param>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/2/programs
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{groupId}/program")]
        [Authorize(Roles = "Администратор, Менеджер, Методист, Преподаватель")]
        public ActionResult<GroupOutputModel> GetGroupProgramsByGroupId(int groupId)
        {
            var group = _service.GetGroupProgramsByGroupId(groupId);
            if (group is null)
                return NotFound($"Группы с id {groupId} не существует");

            var userGroup = this.SupplyUserGroupsList(_service);
            if (User.IsInRole("Преподаватель") && !userGroup.Contains(group.Id))
            {
                return Forbid($"Пользователь не связан с группой {group.Id}");
            }

            GroupOutputModel result = _mapper.Map<GroupOutputModel>(group);
            return Ok(result);
        }

        /// <summary>
        /// Creates Course
        /// </summary>
        /// <param name="group"> is used to get all the information about new group that is necessary to create it</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<GroupOutputModel> AddNewGroup([FromBody] GroupInputModel group)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);          
            var addedGroupId = _service.AddGroup(_mapper.Map<GroupDto>(group));
            var result = _mapper.Map<GroupOutputModel>(_service.GetGroupById(addedGroupId));
            return Ok(result);
        }

        /// <summary>
        /// Updates group
        /// </summary>
        /// /// <param name="id"> is used to find the group user wants to update</param>
        /// <param name="group"> is used to provide new information about selected group</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/2
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<GroupOutputModel> UpdateGroupInfo(int id, [FromBody] GroupInputModel group)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            if (_service.GetGroupById(id) == null)
            {
                return NotFound( $"Ошибка! Отсутствует группа с введенным id {id}!");
            }

            var groupDto = _mapper.Map<GroupDto>(group);
            _service.UpdateGroup(groupDto);
            var result = _mapper.Map<GroupOutputModel>(_service.GetGroupById(id));
            return Ok(result);
        }

        /// <summary>
        /// Deletes group
        /// </summary>
        /// /// <param name="id"> is used to find the group user wants to delete</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/2
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<GroupOutputModel> DeleteGroup(int id)
        {
            var group = _service.GetGroupById(id);
            if (group is null)
                return NotFound($"группа c id {id} не найдена");

            _service.DeleteGroup(id);
            return NoContent();
        }

        /// <summary>
        /// Creates the connection between one group and one material
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with material</param>
        /// <param name="materialId"> is used to find the material which one user wants to connect with group</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/material/id
        [HttpPost("{groupId}/material/{materialId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult AddGroup_Material(int groupId, int materialId)
        {
            var group = _service.GetGroupById(groupId);
            if (group == null)
                return NotFound($"Группа c id {groupId} не найдена");

            var material = _materialService.GetMaterialById(materialId);
            if (material == null)
                return NotFound($"Материал c id {materialId} не найден");

            var userGroup = this.SupplyUserGroupsList(_service);
            if (!User.IsInRole("Администратор") && !userGroup.Contains(group.Id))
            {
                return Forbid($"Пользователь не связан с группой {group.Id}");
            }

            _service.AddGroup_Material(groupId, materialId);
            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Deletes the connection between one group and one material
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with material</param>
        /// <param name="materialId"> is used to find the material which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/material/id
        [HttpDelete("{groupId}/material/{materialId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult DeleteGroup_Material(int groupId, int materialId)
        {
            var group = _service.GetGroupById(groupId);
            if (group is null)
                return NotFound($"Группа c id {groupId} не найдена");

            var material = _materialService.GetMaterialById(materialId);
            if (material is null)
                return NotFound($"Материал c id {materialId} не найден");

            var userGroup = this.SupplyUserGroupsList(_service);
            if (!User.IsInRole("Администратор") && !userGroup.Contains(group.Id))
            {
                return Forbid($"Пользователь не связан с группой {group.Id}");
            }

            _service.DeleteGroup_Material(groupId, materialId);
  
            return NoContent();
        }

        /// <summary>
        /// Deletes the connection between one group and one teacher
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with teacher</param>
        /// <param name="userId"> is used to find the teacher which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/teacher/id
        [HttpDelete("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult DeleteTeacherGroup(int groupId, int userId)
        {
            var group = _service.GetGroupById(groupId);
            if (group is null)
                return NotFound($"Группа c id {groupId} не найдена");

            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound($"Пользователь c id {userId} не найден");

            _service.DeleteTeacherGroup(userId, groupId);
            return NoContent();
        }

        /// <summary>
        /// Creates the connection between one group and one teacher
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with teacher</param>
        /// <param name="userId"> is used to find the teacher which one user wants to connect with group</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/teacher/id
        [HttpPost("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult AddTeacherGroup(int groupId, int userId)
        {
            var group = _service.GetGroupById(groupId);
            if (group is null)
                return NotFound($"Группа c id {groupId} не найдена");

            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound($"Пользователь c id {userId} не найден");

            _service.AddTeacherGroup(groupId, userId);
            return StatusCode(StatusCodes.Status201Created);
        }


        /// <summary>
        /// Deletes the connection between one group and one student
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with student</param>
        /// <param name="userId"> is used to find the student which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/student/id
        [HttpDelete("{groupId}/student/{userId}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult DeleteStudentGroup(int groupId, int userId)
        {
            var group = _service.GetGroupById(groupId);
            if (group is null)
                return NotFound($"Группа c id {groupId} не найдена");

            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound($"Пользователь c id {userId} не найден");

            _service.DeleteStudentGroup(userId, groupId);
            return NoContent();
        }

        /// <summary>
        /// Creates the connection between one group and one student
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with student</param>
        /// <param name="userId"> is used to find the student which one user wants to connect with group</param>
        /// <param name="studentGroupInputModel"> request body with contract number</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/student/id
        [HttpPost("{groupId}/student/{userId}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult AddStudentGroup(int groupId, int userId, [FromBody] StudentGroupInputModel studentGroupInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            
            var group = _service.GetGroupById(groupId);
            if (group is null) 
                return NotFound($"Группа c id {groupId} не найдена");

            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound($"Пользователь c id {userId} не найден");

            var studentGroupDto = _mapper.Map<StudentGroupDto>(studentGroupInputModel);
            var id = _service.AddStudentGroup(groupId, userId, studentGroupDto);
            var outputModel = _mapper.Map<StudentGroupOutputModel>(_service.GetStudentGroupById(id));
            return Ok(outputModel);
        }

        /// <summary>
        /// Deletes the connection between one group and one tutor
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with tutor</param>
        /// <param name="userId"> is used to find the tutor which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/tutor/id
        [HttpDelete("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult DeleteTutorGroups(int groupId, int userId)
        {
            var group = _service.GetGroupById(groupId);
            if (group is null)
                return NotFound($"Группа c id {groupId} не найдена");

            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound($"Пользователь c id {userId} не найден");

            _service.DeleteTutorGroup(userId, groupId);
            return NoContent();
        }


        /// <summary>
        /// Creates the connection between one group and one tutor
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with tutor</param>
        /// <param name="userId"> is used to find the teacher which one user wants to connect with group</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:XXXXX/api/group/id/tutor/id
        [HttpPost("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult AddTutorToGroup(int groupId, int userId)
        {
            var group = _service.GetGroupById(groupId);
            if (group is null)
                return NotFound( $"Группа c id {groupId} не найдена");

            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound($"Пользователь c id {userId} не найден");

            _service.AddTutorToGroup(userId, groupId);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Gets all the reports
        /// </summary>
        /// <returns>Returns the list of GroupReportOutputModel</returns>
        [ProducesResponseType(typeof(List<GroupReportOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // https://localhost:XXXXX/api/group/report
        [HttpGet("report")]
        [Authorize(Roles = "Администратор, Менеджер, Преподаватель, Тьютор, Методист")]
        public ActionResult<List<GroupReportOutputModel>> GetReport()
        {
            var reportDto = _service.GenerateReport();
            if (reportDto.Count == 0)
                return NotFound("Отчет не был создан");

            var report = _mapper.Map<List<GroupReportOutputModel>>(reportDto);
                return Ok(report);
        }


        /// <summary>
        /// Gets a list of themes not covered by the group
        /// </summary>
        /// <param name="id"> is used to find necessary themes by groupId</param>
        /// <returns>Returns the list of ThemeOutputModel</returns>
        [ProducesResponseType(typeof(List<ThemeOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:50221/api/group/2/uncovered-themes
        [HttpGet("{id}/uncovered-themes")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult<List<ThemeOutputModel>> GetUncoveredThemesByGroupId(int id)
        {
            var group = _service.GetGroupById(id);
            if (group is null)
                return NotFound($"Группа c id {id} не найдена");

            var userGroup = this.SupplyUserGroupsList(_service);
            if (!User.IsInRole("Администратор") && !userGroup.Contains(group.Id))
            {
                return Forbid($"Пользователь не связан с группой {group.Id}");
            }

            var result = _mapper.Map<List<ThemeOutputModel>>(_courseService.GetUncoveredThemesByGroupId(id));
            return Ok(result);
        }

        /// <summary>
        /// Gets only one students which have percent of skipped lessons more than param percent
        /// </summary>
        /// <param name="groupId"> is used to find necessary students by groupId</param>
        /// <param name="percent"> is used to find necessary students which have percent of skipped lessons more than this param</param>
        /// <returns>Returns the lust of AttendanceReportOutputModel</returns>
        [ProducesResponseType(typeof(List<AttendanceReportOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // https://localhost:44365/api/group/3/percent-of-skip/0
        [HttpGet("{groupId}/percent-of-skip/{percent}")]
        [Authorize(Roles = "Администратор, Преподаватель, Менеджер")]
        public ActionResult<List<AttendanceReportOutputModel>> GetStudentsByPercentOfSkip(int groupId, int percent)
        {
            var group = _service.GetGroupById(groupId);
            if (group is null)
                return NotFound($"Группа c id {groupId} не найдена");

            if (percent < 0 || percent > 100)
                return StatusCode(StatusCodes.Status409Conflict, "Вы вышли за допустимые рамки числа процентов");


            var userGroup = this.SupplyUserGroupsList(_service);
            if (!User.IsInRole("Администратор") && !userGroup.Contains(group.Id))
            {
                return Forbid($"Пользователь не связан с группой {group.Id}");
            }

            return Ok(_mapper.Map<List<AttendanceReportOutputModel>>(_lessonService.GetStudentByPercentOfSkip(percent, groupId)));
        }

        [HttpGet("statuses")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<DictionaryOutputModel>), StatusCodes.Status200OK)]
        public ActionResult<List<DictionaryOutputModel>> GetGroupStatuses()
        {
            var result = new List<DictionaryOutputModel>();
            foreach (int i in Enum.GetValues(typeof(GroupStatus)))
                result.Add(new DictionaryOutputModel { Id = i, Name = FriendlyNames.GetFriendlyGroupStatusName(((GroupStatus)i)) });
            return result;
        }
    }
    
}




