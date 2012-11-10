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

        }

        private void theInputFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog inputFileDiag = new OpenFileDialog();
            inputFileDiag.Filter = INPUT_FILE_FILTER;

            if (inputFileDiag.ShowDialog() == DialogResult.OK)
            {
                theInputFilePath = inputFileDiag.FileName;
            }
        }

        private void theVideoFileBrowseButton_Click(object sender, EventArgs e)
        {

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

        #endregion

    }
}
