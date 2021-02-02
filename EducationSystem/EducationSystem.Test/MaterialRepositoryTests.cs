using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Data.Models;
using NUnit.Framework;


namespace EducationSystem.Data.Tests
{
    class MaterialRepositoryTests
    {
        List<MaterialDto> _materials;
        MaterialDto _materialDto;
        MaterialRepository _materialRepository;

        [SetUp]
        public void MaterialRepositorySetUp()
        {
            _materialDto = new MaterialDto();
            _materialRepository = new MaterialRepository();
            _materials = new List<MaterialDto>();
        }

        [Test]
        public void GetMaterialsTest()
        {
            GetMaterialsMock();
            List<MaterialDto> expected = _materials;
            List<MaterialDto> actual = _materialRepository.GetMaterials();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void GetMaterialByIdTest(int a)
        {
            GetMaterialByIdMock(a);
            MaterialDto expected = _materialDto;
            MaterialDto actual = _materialRepository.GetMaterialById(a);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void AddMaterialTest(int caseOfMock)
        {
            MaterialDto expected = AddMaterialMock(caseOfMock);
            int actual = _materialRepository.AddMaterial(expected);
            Assert.AreEqual(1, actual);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void DeleteMaterialTest(int caseOfMock)
        {
            int actual = _materialRepository.DeleteMaterialById(caseOfMock);
            Assert.AreEqual(1, actual);
        }

        public MaterialDto AddMaterialMock(int a)
        {
            switch (a)
            {
                case 4:
                    _materialDto.Link = "";
                    _materialDto.Description = "Base C#";
                    break;
                case 5:
                    _materialDto.Link = "";
                    _materialDto.Description = "Front-end";
                    break;
                case 6:
                    _materialDto.Link = "";
                    _materialDto.Description = "Back-end";
                    break;
                case 7:
                    _materialDto.Link = "";
                    _materialDto.Description = "Kotlin";
                    break;
            }
            return _materialDto;
        }
        public void GetMaterialByIdMock(int a)
        {
            switch (a)
            {
                case 4:
                    _materialDto = new MaterialDto
                        {
                            Id = 4,
                            Link = "",
                            Description = "Base C#",
                            IsDeleted = false
                        };
                    break;
                case 5:
                    _materialDto = new MaterialDto
                         {
                             Id = 5,
                             Link = "",
                             Description = "Front-end",
                             IsDeleted = false
                         };
                    break;
                case 6:
                    _materialDto = new MaterialDto
                         {
                             Id = 6,
                             Link = "",
                             Description = "Back-end",
                             IsDeleted = false
                         };
                    break;
                case 7:
                    _materialDto = new MaterialDto
                         {
                             Id = 7,
                             Link = "",
                             Description = "Kotlin",
                             IsDeleted = false
                         };
                    break;
            }
        }
        public void GetMaterialsMock()
        {
            _materials = new List<MaterialDto>
            {
                new MaterialDto
                {
                    Id = 4,
                    Link = "",
                    Description = "Base C#",
                    IsDeleted = false
                },
                new MaterialDto
                {
                    Id = 5,
                    Link = "",
                    Description = "Front-end",
                    IsDeleted = false
                },
                new MaterialDto
                {
                    Id = 6,
                    Link = "",
                    Description = "Back-end",
                    IsDeleted = false
                },
                new MaterialDto
                {
                    Id = 7,
                    Link = "",
                    Description = "Kotlin",
                    IsDeleted = false
                }
            };
        }
        [TearDown]
        public void ClearMaterials()
        {
            _materials.Clear();
        }
    }
}
