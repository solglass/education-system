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

        [OneTimeSetUp]
        public void MaterialRepositorySetUp()
        {
            _materialDto = new MaterialDto();
            _materialRepository = new MaterialRepository();
            _materials = GetMaterialsMock();
        }

        [Test, Order(1)]
        public void GetMaterialsTest()
        {
            List<MaterialDto> expected = GetMaterialsMock();
            List<MaterialDto> actual = _materialRepository.GetMaterials();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void GetMaterialByIdTest(int a)
        {
            MaterialDto expected = GetMaterialByIdMock(a);
            MaterialDto actual = _materialRepository.GetMaterialById(a);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void AddMaterialTest(int caseOfMock)
        {
            MaterialDto materialDto = AddMaterialMock(caseOfMock);
            _materials.Add(materialDto);
            List<MaterialDto> expected = _materials;
            if (_materialRepository.AddMaterial(materialDto) != 1)
            {
                throw new Exception("Addiction failed");
            }
            List<MaterialDto> actual = _materialRepository.GetMaterials();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void DeleteMaterialTest(int caseOfMock)
        {
            _materials.RemoveAt(0);
            List<MaterialDto> expected = _materials;
            if (_materialRepository.DeleteMaterialById(caseOfMock) != 1)
                throw new Exception("Delete failed");
            List<MaterialDto> actual = _materialRepository.GetMaterials();
            Assert.AreEqual(expected, actual);
        }

        public List<MaterialDto> DeleteMaterialMock(int a)
        {
            List<MaterialDto> materials = GetMaterialsMock();
            switch(a)
            {
                case 4:
                    materials.Add(AddMaterialMock(5));
                    materials.Add(AddMaterialMock(6));
                    materials.Add(AddMaterialMock(7));
                    break;
                case 5:
                    materials.Add(AddMaterialMock(4));
                    materials.Add(AddMaterialMock(6));
                    materials.Add(AddMaterialMock(7));
                    break;
                case 6:
                    materials.Add(AddMaterialMock(4));
                    materials.Add(AddMaterialMock(5));
                    materials.Add(AddMaterialMock(7));
                    break;
                case 7:
                    materials.Add(AddMaterialMock(4));
                    materials.Add(AddMaterialMock(5));
                    materials.Add(AddMaterialMock(6));
                    break;
            }
            return materials;
        }
        public MaterialDto AddMaterialMock(int a)
        {
            MaterialDto materialDto = new MaterialDto();
            switch (a)
            {
                case 4:
                    materialDto.Link = "";
                    materialDto.Description = "Base C#";
                    break;
                case 5:
                    materialDto.Link = "";
                    materialDto.Description = "Front-end";
                    break;
                case 6:
                    materialDto.Link = "";
                    materialDto.Description = "Back-end";
                    break;
                case 7:
                    materialDto.Link = "";
                    materialDto.Description = "Kotlin";
                    break;
                case 8:
                    materialDto.Link = "";
                    materialDto.Description = "Base C#";
                    break;
                case 9:
                    materialDto.Link = "";
                    materialDto.Description = "Front-end";
                    break;
                case 10:
                    materialDto.Link = "";
                    materialDto.Description = "Back-end";
                    break;
                case 12:
                    materialDto.Link = "https://metanit.com/sharp/";
                    materialDto.Description = "";
                    break;
                case 13:
                    materialDto.Link = "";
                    materialDto.Description = "React";
                    break;
                default:
                    throw new Exception("Case does not exist");
            }
            return materialDto;
        }
        public MaterialDto GetMaterialByIdMock(int a)
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
                case 8:
                    _materialDto = new MaterialDto
                    {
                        Id = 8,
                        Link = "",
                        Description = "Base C#",
                        IsDeleted = false
                    };
                    break;
                case 9:
                    _materialDto = new MaterialDto
                    {
                        Id = 9,
                        Link = "",
                        Description = "Front-end",
                        IsDeleted = false
                    };
                    break;
                case 10:
                    _materialDto = new MaterialDto
                    {
                        Id = 10,
                        Link = "",
                        Description = "Back-end",
                        IsDeleted = false
                    };
                    break;
                case 12:
                    _materialDto = new MaterialDto
                    {
                        Id = 12,
                        Link = "https://metanit.com/sharp/",
                        Description = "",
                        IsDeleted = false
                    };
                    break;
                case 13:
                    _materialDto = new MaterialDto
                    {
                        Id = 13,
                        Link = "",
                        Description = "React",
                        IsDeleted = false
                    };
                    break;
                default:
                    throw new Exception("Case does not exist");
            }
            return _materialDto;
        }
        public List<MaterialDto> GetMaterialsMock()
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
                },
                new MaterialDto
                    {
                        Id = 8,
                        Link = "",
                        Description = "Base C#",
                        IsDeleted = false
                    },
                new MaterialDto
                    {
                        Id = 9,
                        Link = "",
                        Description = "Front-end",
                        IsDeleted = false
                    },
                  new MaterialDto
                    {
                        Id = 10,
                        Link = "",
                        Description = "Back-end",
                        IsDeleted = false
                    },
                  new MaterialDto
                    {
                        Id = 12,
                        Link = "https://metanit.com/sharp/",
                        Description = "",
                        IsDeleted = false
                    },
                  new MaterialDto
                    {
                        Id = 13,
                        Link = "",
                        Description = "React",
                        IsDeleted = false
                    }
        };
            return _materials;
        }
        [TearDown]
        public void ClearMaterials()
        {
            //_materials.Clear();
        }
    }
}
