﻿using BIC.Scrappers.Utils.Attributes;
using BIC.Utils;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils.TableScrapping
{
    public class TableArrayConverter<T> where T : class, new()
    {
        private readonly ILog _logger = LogServiceProvider.Logger;
        private enum AllowedTypes { StringType, IntType, DecimalType, DateType };
        private readonly char[] EmptyCharacters = new char[] { '-' };

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
                if (t.FullName.Contains("System.Int32"))
                    return AllowedTypes.IntType;
                else if (t.FullName.Contains("System.Decimal"))
                    return AllowedTypes.DecimalType;
                else if (t.FullName.Contains("System.DateTime"))
                    return AllowedTypes.DateType;
                else if (t.FullName.Contains("System.String"))
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
            try
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
                    {
                        Action<string, AllowedTypes> fCreateErrorMessage = (msg, allowedTypes) =>
                        {
                            var s = new StringBuilder();
                            s.Append("Type:" + allowedTypes.ToString() + ";");
                            s.Append("Value:" + obj + ";");
                            s.Append("PropertyName:" + props[m.PropertyIndex].Name + ";");
                            s.Append("PropertyIndex:" + Convert.ToString(m.PropertyIndex) + ";");
                            s.Append("ErrorMessage: " + msg);
                            _logger.Error(s.ToString());
                        };

                        if (m.ColumnIndex == -1)
                        {
                            _logger.Error($"Column index is -1 for the following property : {props[m.PropertyIndex].Name}");
                            continue;
                        }
                        switch (m.PropertyType)
                        {
                            case AllowedTypes.DateType:
                                props[m.PropertyIndex].SetValue(obj, record[m.ColumnIndex].DirtyDateStringToDate(e => fCreateErrorMessage(e.Message, AllowedTypes.DateType), EmptyCharacters));
                                break;
                            case AllowedTypes.DecimalType:
                                props[m.PropertyIndex].SetValue(obj, record[m.ColumnIndex].AllSpecialsStringToDecimal(e => fCreateErrorMessage(e.Message, AllowedTypes.DecimalType), EmptyCharacters));
                                break;
                            case AllowedTypes.IntType:
                                props[m.PropertyIndex].SetValue(obj, record[m.ColumnIndex].StringToInt(e => fCreateErrorMessage(e.Message, AllowedTypes.IntType), EmptyCharacters));
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
            catch(Exception ex)
            {
                _logger.ReportException(ex);
                throw;
            }
        }
    }
}
