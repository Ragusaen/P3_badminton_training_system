using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Server.Controller;

namespace Test
{
    public class LoginTests
    {
        [Test]
        public void login_with_hansemand_password123()
        {
            User ui = new User();

            byte[] token = ui.Login("hansemand", Encoding.ASCII.GetBytes("password123"));

            Assert.AreEqual(32, token.Length);
        }
    }
}
