using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DAL;

namespace Test
{
    [TestClass]
    public class DBTests : DBResetter
    {
        [TestMethod]
        public void CanGetConnectionsString()
        {
            Assert.IsNotNull(ConfigurationManager.ConnectionStrings["DBConnString"]);
        }
    }
}
