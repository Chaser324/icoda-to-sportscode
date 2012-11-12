using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CodesConversion
{
    public class CodaFile
    {
        #region Private Fields

        private String theFilePath = String.Empty;
        private Dictionary<String,Code> theCodes = new Dictionary<string,Code>();
        
        #endregion

        #region Constructor

        public CodaFile(String aFilePath)
        {
            theFilePath = aFilePath;
        }

        #endregion

        #region Public Methods

        public bool ParseFile()
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

        public TreeNode BuildTree()
        {
            TreeNode node = new TreeNode("Codes");
            foreach (KeyValuePair<string,Code> entry in theCodes)
            {
                node.Nodes.Add(entry.Value.BuildTree());
            }

            return node;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Fields

        #endregion

    }
}
