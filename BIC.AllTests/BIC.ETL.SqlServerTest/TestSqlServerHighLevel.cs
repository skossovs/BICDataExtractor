﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIC.ETL.SqlServerTest
{
    [TestClass]
    public class TestSqlServerHighLevel
    {
        [TestMethod]
        public void TestFileProcessor()
        {
            var processor = new SqlServer.FileProcessor();
            processor.Do();
        }
    }
}
