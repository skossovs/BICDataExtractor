﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    public interface IStatusMessage
    {
        EProcessStatus ProcessStatus { get; set; }
    }
}
