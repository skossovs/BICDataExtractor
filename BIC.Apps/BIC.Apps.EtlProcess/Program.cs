﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.EtlProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var processor = new ETL.SqlServer.FileProcessor())
            {
                processor.Do();
            }
        }
    }
}
