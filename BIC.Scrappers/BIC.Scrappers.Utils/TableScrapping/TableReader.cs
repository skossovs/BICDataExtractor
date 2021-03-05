using BIC.Scrappers.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils.TableScrapping
{
    public class TableReader<T> where T : class, new()
    {
        private enum AllowedTypes { StringType, IntType, DecimalType, DateType };
        private class Mapping
        {
            public int          ColumnIndex;
            public int          PropertyIndex;
            public AllowedTypes PropertyType;
        };

        private List<Mapping> _propertyMapping;
        public bool MapHeader(string[] headers)
        {
            int propIndex = 0;
            _propertyMapping = new List<Mapping>();
            var properties = typeof(T).GetProperties();

            Func<Type, AllowedTypes> typeConvertFunc = (t) =>
            {
                if (t.Name == "Int32")
                    return AllowedTypes.IntType;
                else if (t.Name == "Decimal")
                    return AllowedTypes.DecimalType;
                else if (t.Name == "DateTime")
                    return AllowedTypes.DateType;
                else if (t.Name == "String")
                    return AllowedTypes.StringType;
                else
                    throw new Exception(string.Format("Wrong type {0}", t.Name));
            };

            foreach(var prop in properties)
            {
                var propertyAttributes = Attribute.GetCustomAttributes(prop);
                var siteColumnName = (propertyAttributes.Where(atr => atr is PropertyMappingAttribute)?.FirstOrDefault() as PropertyMappingAttribute)?.ColumnNameOnTheSite;
                var siteColumnIndex = Array.FindIndex(headers, s => s == siteColumnName);
                _propertyMapping.Add(new Mapping() { ColumnIndex = siteColumnIndex, PropertyIndex = propIndex, PropertyType = typeConvertFunc(prop.PropertyType)});
                propIndex++;
            }

            return true;
        }

        public IEnumerable<T> GenerateDataSet(IEnumerable<string[]> scrappedRecords)
        {
            if (_propertyMapping == null)
                throw new Exception("Mapping is not ready. One must call TableReader<T>.MapHeader method first.");

            var props = typeof(T).GetProperties();
            var results = new T[scrappedRecords.Count()];
            int i = 0;
            foreach (var record in scrappedRecords)
            {
                T obj = new T();
                foreach (var m in _propertyMapping)
                { // TODO: nullable conversion is needed
                    switch(m.PropertyType)
                    {
                        case AllowedTypes.DateType:
                            props[m.PropertyIndex].SetValue(Convert.ToDateTime(obj), record[m.ColumnIndex]);
                            break;
                        case AllowedTypes.DecimalType:
                            props[m.PropertyIndex].SetValue(Convert.ToDecimal(obj), record[m.ColumnIndex]);
                            break;
                        case AllowedTypes.IntType:
                            props[m.PropertyIndex].SetValue(Convert.ToInt32(obj), record[m.ColumnIndex]);
                            break;
                        case AllowedTypes.StringType:
                            props[m.PropertyIndex].SetValue(obj, record[m.ColumnIndex]);
                            break;
                    }
                }

                results[i] = obj;
                i++;
            }
            return results;
        }
    }
}
