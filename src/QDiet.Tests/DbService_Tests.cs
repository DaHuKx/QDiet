using QDiet.Domain.Models.DataBase;
using QDiet.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDiet.Tests
{
    [TestClass]
    public class DbService_Tests
    {
        [TestMethod]
        [DataRow(1, "dahuk")]
        [DataRow(3, "dahukx")]
        [DataRow(4, "t4pok")]
        [DataRow(5, null)]
        public async Task GetUserTests(long id, string expectedUserName)
        {
            User? user = await DbService.GetUserAsync(id);

            Assert.AreEqual(user?.Username, expectedUserName);
        }

        [TestMethod]
        [DataRow("dahuk", true)]
        [DataRow("dahukx", true)]
        [DataRow("danill", false)]
        [DataRow("ddd", false)]
        public async Task UserNameExist_Test(string userName, bool expectedResult)
        {
            bool result = await DbService.UserNameExistAsync(userName);

            Assert.AreEqual(result, expectedResult);
        }
    }
}
