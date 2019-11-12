using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class UserTests : DBResetter
    {
        [TestMethod]
        public void LoginAccount_johninator_pw_fortytwo()
        {
            var user = new UserManager();

            byte[] token = user.Login("johninator", "fortytwo");

            Assert.AreEqual(UserManager.TokenSize, token.Length);
        }

        [TestMethod]
        public void CreateAccountAndLogin()
        {
            var user = new UserManager();

            user.Create("manspider", "trashman");

            byte[] b = user.Login("manspider", "trashman");

            Assert.AreEqual(UserManager.TokenSize, b.Length);
        }
    }
}
