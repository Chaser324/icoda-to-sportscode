using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodesConversion
{
    public class Instance
    {
        #region Private Fields

        private uint theID = 0;
        private double theStartTime = 0.0;
        private double theEndTime = 0.0;
        private String theCode = String.Empty;

        #endregion

        #region Public Methods

        public TreeNode BuildTreeNode()
        {
            TreeNode node = new TreeNode(theID.ToString());

            node.Nodes.Add("Start: " + GetMinutes(theStartTime) + "m" + GetSeconds(theStartTime) + "s");
            node.Nodes.Add("End: " + GetMinutes(theEndTime) + "m" + GetSeconds(theEndTime) + "s");

            return node;
        }

        #endregion

        #region Private Methods

        private int GetMinutes(double time)
        {
            int minutes = (int)time / 60;
            return minutes;
        }

        private double GetSeconds(double time)
        {
            double seconds = time % 60.0;
            return seconds;
        }

        #endregion

        #region Public Fields

        public uint ID 
        {
            get { return theID; }
            set { theID = value; } 
        }
        public double Start
        {
            get { return theStartTime; }
            set { theStartTime = value; }
        }
        public double End
        {
            get { return theEndTime; }
            set { theEndTime = value; }
        }
        public String Code
        {
            get { return theCode; }
            set { theCode = value; }
        }

        #endregion
    }
}
