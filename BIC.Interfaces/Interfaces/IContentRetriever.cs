﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    public enum ERetrieverType { Finviz };
    public interface IContentRetriever
    {
        string GetData(string url);
    }
}