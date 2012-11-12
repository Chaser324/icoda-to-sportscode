using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CodesConversion
{
    public class Code : IEnumerable<Instance>
    {
        #region Private Fields

        private String theCodeName = String.Empty;
        private uint theRedValue = 0;
        private uint theGreenValue = 0;
        private uint theBlueValue = 0;

        private List<Instance> theInstances = new List<Instance>();

        #endregion

        #region Constructor

        #endregion

        #region Public Methods

        public bool AddInstance(Instance anInstance)
        {
            if (anInstance.Code != theCodeName)
            {
                return false;
            }

            theInstances.Add(anInstance);

            return true;
        }

        public TreeNode BuildTree()
        {
            TreeNode node = new TreeNode(theCodeName);

            TreeNode colorNode = new TreeNode("Color");
            colorNode.Nodes.Add("R: " + theRedValue.ToString());
            colorNode.Nodes.Add("G: " + theGreenValue.ToString());
            colorNode.Nodes.Add("B: " + theBlueValue.ToString());

            node.Nodes.Add(colorNode);

            foreach (Instance instance in theInstances)
            {
                instance.ID = (uint)(theInstances.IndexOf(instance));
                node.Nodes.Add(instance.BuildTreeNode());
            }

            return node;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Fields

        public String Name
        {
            get { return theCodeName; }
            set { theCodeName = value; }
        }
        public uint R
        {
            get { return theRedValue; }
            set { theRedValue = value; }
        }
        public uint G
        {
            get { return theGreenValue; }
            set { theGreenValue = value; }
        }
        public uint B
        {
            get { return theBlueValue; }
            set { theBlueValue = value; }
        }

        public Instance this[int index]
        {
            get
            {
                return theInstances[index];
            }
        }

        public IEnumerator<Instance> GetEnumerator()
        {
            return theInstances.GetEnumerator();
        }

        public int InstanceCount
        {
            get
            {
                return theInstances.Count;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
