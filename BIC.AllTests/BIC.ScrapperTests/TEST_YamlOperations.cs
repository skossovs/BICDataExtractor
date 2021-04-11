using BIC.Scrappers.FinvizScrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_YamlOperations
    {
        [TestMethod]
        public void TestYamlObjectRead()
        {
            FinvizFilterComboboxes objectFromYaml;
            using (StreamReader rd = new StreamReader("FinvizFilterComboboxes.yaml"))
            {
                objectFromYaml = Utils.YamlOperations<FinvizFilterComboboxes>.ReadObjectFromStream(rd);
            }

            Assert.IsNotNull(objectFromYaml);
        }


        // This test is just for understanding the structure of serialized object
        [TestMethod]
        public void WriteObjectToYamlFile()
        {
            FinvizFilterComboboxes objectToYaml = new FinvizFilterComboboxes()
            {
                CountryFilter = new List<FinvizFilterComboboxes.Item>()
                {
                    new FinvizFilterComboboxes.Item() { Label = "USA"              , Value = "geo_usa"    },
                    new FinvizFilterComboboxes.Item() { Label = "Foreighn (ex-USA)", Value = "geo_notusa" },
                }
            };

            using (TextWriter writer = new StreamWriter("FinvizFilterComboboxesTest.yaml"))
            {
                Utils.YamlOperations<FinvizFilterComboboxes>.WriteObjectToYamlFile(objectToYaml, writer);
            }

        }
    }
}
