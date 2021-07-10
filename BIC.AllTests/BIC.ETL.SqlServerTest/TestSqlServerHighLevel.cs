using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIC.ETL.SqlServerTest
{
    [TestClass]
    public class TestSqlServerHighLevel
    {
        [TestMethod]
        public void TestFileProcessor()
        {
            using (var processor = new ETL.SqlServer.FileProcessor())
            {
                processor.Do();
            }
        }
    }
}
