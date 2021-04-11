using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    /// <summary>
    /// Works with single record.
    /// Can parse the record populating object as a whole or just a part of existing object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<T>
    {
        T ParseObject(string fragment);
    }
}
