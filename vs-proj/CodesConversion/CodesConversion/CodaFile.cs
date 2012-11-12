using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CodesConversion
{
    public class CodaFile : BaseFile
    {
        #region Private Fields

        
        #endregion

        #region Constructor

        public CodaFile(String aFilePath)
        {
            theFilePath = aFilePath;
        }

        #endregion

        #region Public Methods

        public override bool ParseFile()
        {
            bool retVal = false;

            if (!File.Exists(theFilePath))
            {
                return false;
            }

            try
            {
                XDocument xmlDoc = XDocument.Load(theFilePath);

                // Parse each "instance" from "ALL_INSTANCES"
                List<Instance> instances =
                    (from instance in xmlDoc.Descendants("instance")
                     select new Instance
                     {
                         ID =  UInt32.Parse(instance.Element("ID").Value),
                         Start = Double.Parse(instance.Element("start").Value),
                         End = Double.Parse(instance.Element("end").Value),
                         Code = instance.Element("code").Value,
                     }).ToList<Instance>();

                // Parse each "row" from "ROWS"
                List<Code> codes =
                    (from code in xmlDoc.Descendants("row")
                     select new Code
                     {
                         R = UInt32.Parse(code.Element("R").Value),
                         G = UInt32.Parse(code.Element("G").Value),
                         B = UInt32.Parse(code.Element("B").Value),
                         Name = code.Element("code").Value,
                     }).ToList<Code>();

                // Add all codes to dictionary
                foreach (Code code in codes)
                {
                    if (!theCodes.ContainsKey(code.Name))
                    {
                        theCodes.Add(code.Name, code);
                    }
                }

                // Record the instances of each code
                foreach (Instance instance in instances)
                {
                    if (theCodes.ContainsKey(instance.Code))
                    {
                        theCodes[instance.Code].AddInstance(instance);
                    }
                }

                retVal = true;
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public override bool ConvertFile(IBaseFile file)
        {
            bool retVal = false;

            try
            {
                XDocument outputDoc = new XDocument(
                    new XElement("file",
                        new XElement("ALL_INSTANCES"),
                        new XElement("ROWS")));

                XElement outputInstances = outputDoc.Element("file").Element("ALL_INSTANCES");
                XElement outputRows = outputDoc.Element("file").Element("ROWS");

                uint ID = 1;

                foreach (Code code in file)
                {
                    outputRows.Add(new XElement("row",
                        new XElement("code", code.Name),
                        new XElement("R", code.R),
                        new XElement("G", code.G),
                        new XElement("B", code.B)));

                    foreach (Instance instance in code)
                    {
                        outputInstances.Add(new XElement("instance",
                            new XElement("ID", ID.ToString()),
                            new XElement("start", instance.Start.ToString("0.00")),
                            new XElement("end", instance.End.ToString("0.00")),
                            new XElement("code", instance.Code)));

                        ++ID;
                    }
                }

                outputDoc.Save(theFilePath);

                retVal = true;
            }
            catch
            {
            }

            return retVal;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Fields

        #endregion

    }
}
