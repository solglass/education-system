using EducationSystem.Business.Model;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationSystem.Business
{
    public class GroupService : IGroupService
    {
        private IGroupRepository _groupRepository;
        private ICourseRepository _courseRepository;
        private int _daysInOneWeek = 7;

        public GroupService(IGroupRepository groupRepository, ICourseRepository courseRepository)
        {
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
        }

        public List<GroupDto> GetGroups()
        {
            var dtoList = _groupRepository.GetGroups();
            foreach (var dto in dtoList)
            {
                dto.EndDate = dto.StartDate.AddDays(dto.Course.Duration * _daysInOneWeek);
            }
            return dtoList;
        }

        public GroupDto GetGroupById(int id)
        {
            var dto = _groupRepository.GetGroupById(id);
            dto.EndDate = dto.StartDate.AddDays(dto.Course.Duration * _daysInOneWeek);
            return dto;
        }
        public List<int> GetGroupsByStudentId(int id)
        {
            return _groupRepository.GetGroupsByStudentId(id);
        }
        public List<int> GetGroupsByTeacherId(int id)
        {
            return _groupRepository.GetGroupsByTeacherId(id);
        }
        public List<int> GetGroupsByTutorId(int id)
        {
            return _groupRepository.GetGroupsByTutorId(id);
        }

        public List<GroupDto> GetGroupsWithoutTutors()
        {
            var dtoList = _groupRepository.GetGroupsWithoutTutors();
            foreach (var dto in dtoList)
            {
                dto.EndDate = dto.StartDate.AddDays(dto.Course.Duration * _daysInOneWeek);
            }
            return dtoList;
        }

        public List<NumberOfLessonsForGroupToCompleteTheThemeDto> GetGroupByThemeId(int themeId)
        {
            var dtoList = _groupRepository.GetGroupByThemeId(themeId);
            foreach (var dto in dtoList)
            {
                dto.EndDate = dto.StartDate.AddDays(dto.Course.Duration * _daysInOneWeek);
            }
            return dtoList;
        }
        public GroupDto GetGroupProgramByGroupId(int id)
        {
            var group = _groupRepository.GetGroupById(id);
            var program = _courseRepository.GetCourse_Program(id);
            group.Course.Themes = program.Select(obj =>
            new OrderedThemeDto
            {
                Id = obj.Theme.Id,
                Name = obj.Theme.Name,
                Order = obj.Order
            }).ToList();
            return group;
        }

        public int AddGroup(GroupDto groupDto)
        {
            return _groupRepository.AddGroup(groupDto);
        }

        public int UpdateGroup(GroupDto groupDto)
        {
            return _groupRepository.UpdateGroup(groupDto);
        }

        public int DeleteGroup(int id)
        {
            return _groupRepository.DeleteGroup(id);
        }

        public int AddGroup_Material(int groupId, int materialId)
        {
            return _groupRepository.AddGroup_Material(groupId, materialId);
        }

        public int DeleteGroup_Material(int groupId, int materialId)
        {
            return _groupRepository.DeleteGroup_Material(groupId, materialId);
        }
        public int DeleteTeacherGroup(int groupId, int userId)
        {
            return _groupRepository.DeleteTeacherGroup(groupId, userId);
        }
        public int AddTeacherGroup(int groupId, int userId)
        {
            return _groupRepository.AddTeacherGroup(groupId, userId);
        }
        public int DeleteStudentGroup(int userId, int groupId)
        {
            return _groupRepository.DeleteStudentGroup(userId, groupId);
        }
        public int AddStudentGroup(int groupId, int userId, StudentGroupDto studentGroupDto)
        {
            studentGroupDto.Group = new GroupDto { Id = groupId };
            studentGroupDto.User = new UserDto { Id = userId };
            return _groupRepository.AddStudentGroup(studentGroupDto);
        }
        public StudentGroupDto GetStudentGroupById(int userGroupId)
        {
            return _groupRepository.GetStudentGroupById(userGroupId);
        }
        public int DeleteTutorGroup(int groupId, int userId)
        {
            return _groupRepository.DeleteTutorGroup(userId, groupId);
        }
        public int AddTutorToGroup(int groupId, int userId)
        {
            return _groupRepository.AddTutorToGroup(userId, groupId);
        }
        public List<GroupReportDto> GenerateReport()
        {
            return _groupRepository.GenerateReport();
        }
    }
}
