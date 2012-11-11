using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodesConversion
{
    public partial class MainForm : Form
    {
        #region Private Constants

        private const String INPUT_FILE_FILTER = "iCode File (*.xml)|*.xml|All files (*.*)|*.*";
        private const String OUTPUT_FILE_FILTER = "SportsCode File (*.TLcodes)|*.TLcodes|All files (*.*)|*.*";
        private const String VIDEO_FILE_FILTER = "Video File (*.mp4;*.mov)|*.mp4;*.mov|All files (*.*)|*.*";

        #endregion

        #region Private Fields

        private String theInputFilePath = String.Empty;
        private String theOutputFilePath = String.Empty;
        private String theVideoFilePath = String.Empty;

        private CodaFile theCodaFile = null;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        #region Event Handlers

        private void theConvertButton_Click(object sender, EventArgs e)
        {
            InitCodaFile();
        }

        private void theInputFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog inputFileDiag = new OpenFileDialog();
            inputFileDiag.Filter = INPUT_FILE_FILTER;

            if (inputFileDiag.ShowDialog() == DialogResult.OK)
            {
                theInputFilePath = inputFileDiag.FileName;
                theInputFileTextBox.Text = theInputFilePath;
            }
        }

        private void theVideoFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog inputFileDiag = new OpenFileDialog();
            inputFileDiag.Filter = VIDEO_FILE_FILTER;

            if (inputFileDiag.ShowDialog() == DialogResult.OK)
            {
                theInputFilePath = inputFileDiag.FileName;
            }
        }

        private void theOutputFileBrowseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDiag = new SaveFileDialog();
            saveFileDiag.Filter = OUTPUT_FILE_FILTER;
            saveFileDiag.DefaultExt = "TLcodes";

            if (theInputFilePath != String.Empty && theOutputFilePath == String.Empty)
            {
                saveFileDiag.FileName = theInputFilePath.Replace(".xml",".TLCodes");
            }

            if (saveFileDiag.ShowDialog() == DialogResult.OK)
            {
                theInputFilePath = saveFileDiag.FileName;
            }
        }

        #endregion

        private bool InitCodaFile()
        {
            bool retVal = false;

            theCodaFile = new CodaFile(theInputFilePath);
            if (theCodaFile.ParseFile())
            {
                theCodesTreeView.Nodes.Clear();

                // Add Rows/Codes and Instances Nodes to TreeView
                theCodesTreeView.Nodes.Add(theCodaFile.BuildTree());
                theCodesTreeView.Nodes[0].Expand();

                retVal = true;
            }

            return retVal;
        }

        #endregion

    }
}
