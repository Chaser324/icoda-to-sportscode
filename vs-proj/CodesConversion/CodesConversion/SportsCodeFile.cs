using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CodesConversion
{
    public class SportsCodeFile : BaseFile
    {
        #region Private Constants

        // {0} = File ID
        private const String HEADER_1 = "GameBreaker 10\n<version 5.4><ID>\t{0}\n" +
                                        "<Movie PKG ID>\tPKG_ID 000000000000 unimplemented\t</Movie PKG ID>\n" +
                                        "<long name Movie>\t</long name Movie>\n\n";

        // {0},{1},{2} = File Paths
        private const String HEADER_2 = "<timeline full parent path>\n1\t" + "\xfffe" +
                                        "file://{0}" + "\x03" + "\n" +
                                        "</timeline full parent path>\n\n" +
                                        "<timeline full file path>\n1\t" + "\xfffe" +
                                        "file://{1}" + "\x03" + "\n" +
                                        "</timeline full file path>\n\n" +
                                        "<linked movie full file path>\n1\t" + "\xfffe" +
                                        "file://{2}" + "\x03" + "\n" +
                                        "</linked movie full file path>\n\n";

        private const String HEADER_3 = "+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\n" +
                                        "</version5.4>\n";

        // {0} = Number of Codes
        // {1} = TLCodes Filename
        // {2} = Total Number of Instances
        // {3} = Title
        private const String HEADER_4 = "{0}\n{1}\n" +
                                        "5\t484\t1278\t{2}\n" +
                                        "140\t-174\t186\t0\n" +
                                        "1\t{3}\t2\t85\n" +
                                        "645\t445\n";

        // {0} = Video Filename
        // {1} = Last Instance Timestamp?
        // {2} = Length of Video
        private const String HEADER_5 = "{0}\t-102\t34016\t0.00000000\t{1}\t{2}\t3435077624	";

        // {0} = String of Instance Start/End Headers "start:#\tend:#\t"
        private const String TABLE_HEADER = "Category: \trow colour:\t# instances\t{0}\n";

        /* "0xca0xca" - start of player names , ETX = 0x03 */

        #endregion

        #region Private Fields


        #endregion

        #region Constructor

        public SportsCodeFile(String aFilePath)
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
                using (StreamReader sr = new StreamReader(theFilePath, Encoding.UTF8))
                using (StreamReader sr2 = new StreamReader(theFilePath, Encoding.UTF8))
                {
                    // Read lines until we reach table of instances and table of colors
                    while (!(sr.ReadLine().StartsWith("Category:")));
                    while (!(sr2.ReadLine().StartsWith("NEW COLOURS"))) ;

                    // Read in all instances
                    bool reading = true;
                    while (reading)
                    {
                        string line = sr.ReadLine();
                        string colorLine = sr2.ReadLine();

                        if (line.Length == 0)
                        {
                            reading = false;
                            continue;
                        }
                        else
                        {
                            // Parse the line
                            string[] splitLine = line.Split('\t');
                            string[] splitColorLine = colorLine.Split('\t');

                            Code parsedCode = new Code();
                            parsedCode.Name = splitLine[0];
                            parsedCode.R = UInt32.Parse(splitColorLine[0]);
                            parsedCode.G = UInt32.Parse(splitColorLine[1]);
                            parsedCode.B = UInt32.Parse(splitColorLine[2]);

                            int numberOfInstances = Int32.Parse(splitLine[2]);

                            for (int i = 0; i < numberOfInstances; i++)
                            {
                                Instance parsedInstance = new Instance();
                                parsedInstance.Start = Double.Parse(splitLine[3 + (i * 2)]);
                                parsedInstance.End = Double.Parse(splitLine[4 + (i * 2)]);
                                parsedInstance.Code = parsedCode.Name;

                                parsedCode.AddInstance(parsedInstance);
                            }

                            theCodes.Add(parsedCode.Name, parsedCode);
                        }
                    }
                }

                retVal = true;
            }
            catch
            {
            }

            return retVal;
        }

        public override bool ConvertFile(IBaseFile file)
        {
            bool retVal = false;



            return retVal;
        }

        #endregion

    }
}
