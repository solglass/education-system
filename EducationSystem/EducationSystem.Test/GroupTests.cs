using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class GroupTests : BaseTest
    {
        private IGroupRepository _groupRepository;
        private ICourseRepository _courseRepository;
        private IMaterialRepository _materialRepository;

        private List<int> _addedMaterialMockIds;
        private List<int> _addedGroupIds;
        private List<int> _addedCourseIds;
        private List<(int, int)> _addedMaterialGroupIds;

        [OneTimeSetUp]
        public void GroupOneTimeSetUp()
        {
            _materialRepository = new MaterialRepository(_options);
            _groupRepository = new GroupRepository(_options);
            _courseRepository = new CourseRepository(_options);

            _addedMaterialMockIds = new List<int>();
            _addedGroupIds = new List<int>();
            _addedCourseIds = new List<int>();
            _addedMaterialGroupIds = new List<(int, int)>();
        }

        [OneTimeTearDown]
        public void GroupOneTimeTearDown()
        {
            DeleteMaterialGroups();
            DeleteMaterials();
            DeleteGroups();
            DeleteCourses();
        }

        public void DeleteMaterials()
        {
            _addedMaterialMockIds.ForEach(id =>
            {
                _materialRepository.HardDeleteMaterial(id);
            });
        }
        public void DeleteGroups()
        {
            _addedGroupIds.ForEach(id =>
            {
                _groupRepository.HardDeleteGroup(id);
            });
        }
        public void DeleteMaterialGroups()
        {
            _addedMaterialGroupIds.ForEach(id =>
            {
                _groupRepository.DeleteGroup_Material(id.Item1, id.Item2);
            });
        }
        public void DeleteCourses()
        {
            _addedCourseIds.ForEach(id =>
            {
                _courseRepository.HardDeleteCourse(id);
            });
        }
    }
}
