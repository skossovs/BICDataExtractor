﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using BIC.Foundation.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.Data
{
    public class ProcessDetails
    {
        public ProcessStartInfo ProcessInfo     { get; set; }
        public bool             IsRunning       { get; set; }
        public Process          ProcessObject   { get; set; }
        public ProcessResult    LastExitCode    { get; set; }
    }
}
