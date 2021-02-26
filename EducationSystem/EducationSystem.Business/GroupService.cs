using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class GroupService : IGroupService
    {
        private GroupRepository _groupRepository;

        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }

        public List<GroupDto> GetGroups()
        {
            return _groupRepository.GetGroups();
        }

        public GroupDto GetGroupById(int id)
        {
            return _groupRepository.GetGroupById(id);
        }

        public List<GroupDto> GetGroupsWithoutTutors()
        {
            return _groupRepository.GetGroupsWithoutTutors();
        }

        public GroupDto GetGroupProgramsByGroupId(int id)
        {
            return _groupRepository.GetGroupProgramsByGroupId(id);
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
    }
}
