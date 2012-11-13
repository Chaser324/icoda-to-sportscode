using System;
using System.Windows.Forms;
using System.IO;

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

        private String theCodaFilePath = String.Empty;
        private String theTLcodesFilePath = String.Empty;

        private CodaFile theCodaFile = null;
        private SportsCodeFile theSportsCodeFile = null;

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
            if (File.Exists(theCodaFilePath) && theTLcodesFilePath != String.Empty)
            {
                InitCodaFile();

                theSportsCodeFile = new SportsCodeFile(theTLcodesFilePath);
                theSportsCodeFile.ConvertFile(theCodaFile);
            }
            else
            {
                // Error. Not all files specified.
            }
        }

        private void theCodaToTLcodesButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(theTLcodesFilePath) && theCodaFilePath != String.Empty)
            {
                InitTLcodesFile();

                theCodaFile = new CodaFile(theCodaFilePath);
                theCodaFile.ConvertFile(theSportsCodeFile);
            }
            else
            {
                // Error. Not all files specified.
            }
        }

        private void theParseCodaFileButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(theCodaFilePath))
            {
                InitCodaFile();
            }
        }

        private void theParseTlCodesButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(theTLcodesFilePath))
            {
                InitTLcodesFile();
            }
        }

        private void theInputFileBrowseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog inputFileDiag = new SaveFileDialog();
            inputFileDiag.CheckFileExists = false;
            inputFileDiag.DefaultExt = "xml";
            inputFileDiag.CheckPathExists = false;
            inputFileDiag.OverwritePrompt = false;
            inputFileDiag.Filter = INPUT_FILE_FILTER;

            if (inputFileDiag.ShowDialog() == DialogResult.OK)
            {
                theCodaFilePath = inputFileDiag.FileName;
                theInputFileTextBox.Text = theCodaFilePath;
            }
        }

        private void theOutputFileBrowseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDiag = new SaveFileDialog();
            saveFileDiag.Filter = OUTPUT_FILE_FILTER;
            saveFileDiag.DefaultExt = "TLcodes";
            saveFileDiag.CheckFileExists = false;
            saveFileDiag.CheckPathExists = false;
            saveFileDiag.OverwritePrompt = false;

            if (theCodaFilePath != String.Empty && theTLcodesFilePath == String.Empty)
            {
                saveFileDiag.FileName = theCodaFilePath.Replace(".xml",".TLCodes");
            }

            if (saveFileDiag.ShowDialog() == DialogResult.OK)
            {
                theTLcodesFilePath = saveFileDiag.FileName;
                theOutputFileTextBox.Text = theTLcodesFilePath;
            }
        }

        #endregion

        private bool InitCodaFile()
        {
            bool retVal = false;

            theCodaFile = new CodaFile(theCodaFilePath);
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

        private bool InitTLcodesFile()
        {
            bool retVal = false;

            theSportsCodeFile = new SportsCodeFile(theTLcodesFilePath);
            if (theSportsCodeFile.ParseFile())
            {
                theCodesTreeView.Nodes.Clear();

                // Add Rows/Codes and Instances Nodes to TreeView
                theCodesTreeView.Nodes.Add(theSportsCodeFile.BuildTree());
                theCodesTreeView.Nodes[0].Expand();

                retVal = true;
            }

            return retVal;
        }

        #endregion

    }
}
