using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CodesConversion
{
    public abstract class BaseFile : IBaseFile
    {
        #region Protected Fields

        protected String theFilePath = String.Empty;
        protected Dictionary<String, Code> theCodes = new Dictionary<string, Code>();

        #endregion

        #region Constructors

        

        #endregion

        #region Public Methods

        public abstract bool ParseFile();
        public abstract bool ConvertFile(IBaseFile file);

        public TreeNode BuildTree()
        {
            TreeNode node = new TreeNode("Codes");
            foreach (KeyValuePair<string, Code> entry in theCodes)
            {
                node.Nodes.Add(entry.Value.BuildTree());
            }

            return node;
        }

        #endregion

        #region Public Properties

        public int CodeCount
        {
            get { return theCodes.Count; }
        }

        public Code this[int index]
        {
            get { return theCodes.Values.ElementAt(index); }
        }

        public IEnumerator<Code> GetEnumerator()
        {
            return theCodes.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
