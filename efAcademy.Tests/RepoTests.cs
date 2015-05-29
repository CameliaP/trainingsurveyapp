using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingSurveyApi.Models;
using TrainingSurveyApi.Repository;
using efAcademy.Entities;
using efAcademy.Entities.Web;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace efAcademy.Tests
{
    [TestClass]
    public class RespositoryTest
    {
        [TestMethod]
        public void testRolesRepo()
        {
            IGet<WebRole> iGet = new RolesRepo();
            WebRole[] result = iGet.GETEmployeesIndex(10, 0).ToArray<WebRole>();
            //spanning out the results for inspection
            Trace.WriteLine("");
            Trace.WriteLine("Test for getting 10 results with no skipping");
            Trace.WriteLine("----------------------------------------------");
            Trace.WriteLine("");

            Array.ForEach<WebRole>(result, r => {
                Trace.WriteLine(String.Format("Role: {0} level: {1}", r.Title,r.Level));
            });
            Assert.IsNotNull(result, "the result is null");
            Assert.IsTrue(result.Count() == 10, "Incorrect number of result from the query");

            //test for getting zero results
            result = iGet.GETEmployeesIndex(0, 0).ToArray<WebRole>();
            Trace.WriteLine("");
            Trace.WriteLine("Test for getting zero results");
            Trace.WriteLine("----------------------------------------------");
            Trace.WriteLine("");
            Array.ForEach<WebRole>(result, r =>
            {
                Trace.WriteLine(String.Format("Role: {0} level: {1}", r.Title, r.Level));
            });
            Assert.IsNotNull(result, "the result is null");
            Assert.IsTrue(result.Count() == 0, "Incorrect number of result from the query");

            //test for skipping results
            result = iGet.GETEmployeesIndex(10, 10).ToArray<WebRole>();
            Trace.WriteLine("");
            Trace.WriteLine("Test for getting 10 results with 10 skipping");
            Trace.WriteLine("----------------------------------------------");
            Trace.WriteLine("");
            Array.ForEach<WebRole>(result, r =>
            {
                Trace.WriteLine(String.Format("Role: {0} level: {1}", r.Title, r.Level));
            });
            Assert.IsNotNull(result, "the result is null");
            Assert.IsTrue(result.Count() == 10, "Incorrect number of result from the query");

            //test for a phrase
            result = iGet.OfString("manager").ToArray<WebRole>();
            Trace.WriteLine("");
            Trace.WriteLine("Test for getting the likely phrase in the role title");
            Trace.WriteLine("----------------------------------------------");
            Trace.WriteLine("");
            Array.ForEach<WebRole>(result, r =>
            {
                Trace.WriteLine(String.Format("Role: {0} level: {1}", r.Title, r.Level));
            });
            Assert.IsNotNull(result, "the result is null");
            Assert.IsTrue(result.Count() != 0, "Incorrect number of result from the query");

            result = iGet.OfInteger(6).ToArray<WebRole>();
            Trace.WriteLine("");
            Trace.WriteLine("Test for getting the all the roles for a level");
            Trace.WriteLine("----------------------------------------------");
            Trace.WriteLine("");
            Array.ForEach<WebRole>(result, r =>
            {
                Trace.WriteLine(String.Format("Role: {0} level: {1}", r.Title, r.Level));
            });
            Assert.IsNotNull(result, "the result is null");
            Assert.IsTrue(result.Count() != 0, "Incorrect number of result from the query");


            //add test on the repo
            Trace.WriteLine("");
            Trace.WriteLine("Test for adding and deleting new roles to the store");
            Trace.WriteLine("----------------------------------------------");
            Trace.WriteLine("");
            WebRole wr = new WebRole() {  Title="Test Role", Level=8};
            IPost<WebRole> iPost = iGet as RolesRepo;
            try
            {
                iPost.Add(wr);
                Assert.IsTrue(iGet.OfString("Test Role").Count() != 0, "Role was not added to the database");
                IDelete<WebRole> iDelete = iGet as RolesRepo;
                try
                {
                    iDelete.Delete(wr);
                    Assert.IsTrue(iGet.OfString("Test Role").Count() == 0, "Test Role was not deleted from the database");
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
            catch (Exception err)
            {
                
                throw err;
            }
            Trace.WriteLine("");
            Trace.WriteLine("Test for updating the role");
            Trace.WriteLine("----------------------------------------------");
            Trace.WriteLine("");
            wr = new WebRole() { Title = "Consultant", Level = 8 };
            IPut<WebRole> iPut = (iGet as RolesRepo);
            try
            {
                iPut.Update(wr);
                Trace.WriteLine(String.Format("Update operation complete"));
                Assert.IsTrue(iGet.OfString("Consultant").FirstOrDefault().Level != 8, "The level of the role has not been updated");
                //switching it back to 5 , to what it was originally
                Trace.WriteLine(String.Format("Changes have been reinstated"));
                wr.Level = 5;
                iPut.Update(wr);
            }
            catch (Exception x)
            {
                
                throw x;
            }

        }
    }
}
