using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;

namespace Test
{
    [TestClass]
    public class LoginTests : DBResetter
    {
        [TestMethod]
        public void LoginAccount_johninator_pw_fortytwo()
        {
            var user = new User();

            byte[] token = user.Login("johninator", "fortytwo");

            Assert.AreEqual(User.tokenSize, token.Length);
        }

        [TestMethod]
        public void CreateAccountAndLogin()
        {
            var user = new User();

            user.Create("manspider", "trashman", new Member("Frank Reynolds", 0));

            byte[] b = user.Login("manspider", "trashman");

            Assert.AreEqual(User.tokenSize, b.Length);
        }
    }
}
