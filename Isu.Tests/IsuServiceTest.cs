using System;
using Isu.Services;
using Isu.Services;
using Isu.Tools;
using Isu.Modules;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3210");
            Student name = _isuService.AddStudent(group, "Alexander");
 
            Assert.True(name != null);
            Assert.True(name == _isuService.FindStudent(name.Name));
            Assert.True(_isuService.FindStudents("M3210").Contains(name));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3110");
 
                for (int i = 0; i < 25; i++)
                {
                    _isuService.AddStudent(group, "name");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group invalidGroup = _isuService.AddGroup("00000000000");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group8 = _isuService.AddGroup("M3208");
            Group group9 = _isuService.AddGroup("M3209");
 
            Student student1 = _isuService.AddStudent(group8, "Artyom");
            Student student2 = _isuService.AddStudent(group9, "Zhorik");
 
            _isuService.ChangeStudentGroup(student2, group8);
 
            Assert.True(_isuService.FindStudents("M3208").Contains(student1));
            Assert.True(!_isuService.FindStudents("M3209").Contains(student2));
        }
    }
}