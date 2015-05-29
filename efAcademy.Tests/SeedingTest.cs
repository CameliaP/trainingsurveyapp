using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using efAcademy.Entities;
using efAcademy.Seeding.Scripts;

namespace efAcademy.Tests
{
    [TestClass]
    public class SeedingTest
    {
        [TestMethod]
        public void TestUploadCourses()
        {
            //call the seeding method direct
            Cleanup.Trainings();
            Cleanup.Courses();
            Upload.Courses();
        }
        [TestMethod]
        public void TestUploadUnits() { 
            //tis test for the upload of the units
            Cleanup.Employees();
            Cleanup.Units();
            Upload.Units();
        }
        [TestMethod]
        public void TestUploadRoles()
        {
            //tis test for the upload of the units
            Cleanup.Employees();
            Cleanup.Roles();
            Upload.Roles();
        }
        [TestMethod]
        public void TestUploadProjects()
        {
            //tis test for the upload of the units
            Cleanup.Employees();
            Cleanup.Projects();
            Upload.Projects();
        }
        [TestMethod]
        public void TestUploadEmployees()
        {
            Cleanup.Employees();
            Upload.Employees();
        }
        //[TestMethod]
        //public void TestUploadLocations()
        //{
        //    //tis test for the upload of the units
        //    Cleanup.Locations();
        //    Upload.Locations();
        //}
        //[TestMethod]
       
        //[TestMethod]
        //public void TestUploadFdbQuestions()
        //{
        //    //tis test for the upload of the units
        //    Cleanup.FdbQuestions();
        //    Upload.FdbQuestions();
        //}
        //[TestMethod]
        //public void TestUploadFdbCategories()
        //{
        //    //tis test for the upload of the units
        //    Cleanup.FdbCategories();
        //    Upload.FdbCategories();
        //}
        //[TestMethod]
        //public void TestUploadFdbOptions()
        //{
        //    //tis test for the upload of the units
        //    Cleanup.FdbOptions();
        //    Upload.FdbOptions();
        //}
      
    }
}
