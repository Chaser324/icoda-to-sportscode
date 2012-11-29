using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace CodesConversion
{
    public class SportsCodeFile : BaseFile
    {
        #region Private Constants

        // {0} = File ID
        private const String HEADER_1 = "GameBreaker\t10\r<version5.4>\r<ID>\t{0:0}\r" +
                                        "<Movie PKG ID>\tPKG_ID 000000000000 unimplemented\t</Movie PKG ID>\r" +
                                        "<long name Movie>\t</long name Movie>\r\r";

        // {0},{1},{2} = File Paths
        private const String HEADER_2A = "<timeline full parent path>\r1\t" + "\x3f\x3f";
                                        //"file://{0}" + "\x03" + "\r" +
        private const String HEADER_2B = "</timeline full parent path>\r\r";

        private const String HEADER_2C = "<timeline full file path>\r1\t" + "\x3f\x3f";
                                        //"file://{0}" + "\x03" + "\r" +
        private const String HEADER_2D = "</timeline full file path>\r\r";

        private const String HEADER_2E = "<linked movie full file path>\r";
                                        //"file://{0}" + "\x03" + "\r" +
        private const String HEADER_2F = "</linked movie full file path>\r\r";

        private const String HEADER_3 = "+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\t+\r" +
                                        "</version5.4>\r";

        // {0} = Number of Codes
        // {1} = TLCodes Filename
        // {2} = Total Number of Instances
        private const String HEADER_4 = "{0}\r{1}\r" +
                                        "5\t484\t1278\t{2}\r" +
                                        "140\t-174\t186\t0\r";

        // {0} = Video Filename
        // {1} = Last Instance Timestamp?
        // {2} = Length of Video
        private const String HEADER_5 = "0\tno movie attached\t\r\r";

        // {0} = String of Instance Start/End Headers "start:#\tend:#\t"
        private const String TABLE_HEADER = "Category: \trow colour:\t# instances\t{0}\r";

        private const String TABLE_HEADER_2 = "Category:\tInstances:\tNum labels\txLabel data format - label type, then data eg string type, then string, for vectors - include 3 values\r";

        private const String TABLE_HEADER_3 = "\r0\r\r0\r\r0\t0\t0\t0\r\r" + "CODE_MATRIX_ORGANISER_RECT\t800\t86\t1270\t400\r" + "NEW COLOURS\r";

        private const String TABLE_HEADER_4 = "TXT:	0\rCHAP:\r0\r1586\r0\r\rUNIQUE IDs\r";

        private const String TABLE_FOOTER_4 = "STRIP MARKERS:\t0\t-100000.0000000000\t100000.0000000000\t-10000.0000000000\t100000.0000000000\r\r" +
                                              "<start DRAWING - instance info>\r" +
                                              "<Nu instances:>\t0\r" +
                                              "<end DRAWING>\r\r";

        private const String TABLE_FOOTER_5 = "<free text CFStrings>\r<count>\t0\r</free text CFStrings>\r\r" +
                                              "<free text Multibyte CFStrings>\r<count>\t0\r</free text Multibyte CFStrings>\rPopup menu:\t1\r<Static text control>\t0\r\r" +
                                              "<instance label variation data>\r</instance label variation data>\r\r" +
                                              "<mutable array labels>\r</mutable array labels>\r\r" +
                                              "<labels list data>\r<no popup window>\r</labels list data>\r\r";

        private const String TABLE_FOOTER_6 = "<Overlay text control>\t0\r\r" +
                                              "<TIMELINE_MOVIETIME_DISPLAY_OPTIONS>\r" +
                                              "0.0000000000\t0\t0\r\r";

        private const String TABLE_FOOTER_7 = "<note_lines>\r" +
                                              "3\r\r" +
                                              "<END TIMELINE>\r";


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

            try
            {
                using (StreamWriter sw = new StreamWriter(theFilePath))
                {
                    Random random = new Random();

                    double randID = random.NextDouble();
                    while (randID < 10000000000000000000)
                    {
                        randID *= 10;
                    }

                    sw.Write(String.Format(HEADER_1, randID));

                    String filename1 = "file://localhost/Users/chasepettit/Desktop";
                    String filename2 = "file://localhost/Users/chasepettit/test-updates.TLcodes";

                    sw.Write(HEADER_2A);
                    foreach (char c in filename1)
                    {
                        sw.Write(c);
                        sw.Write("\x00");
                    }
                    sw.Write("\x03" + "\r" + HEADER_2B);

                    sw.Write(HEADER_2C);
                    foreach (char c in filename2)
                    {
                        sw.Write(c);
                        sw.Write("\x00");
                    }
                    sw.Write("\x03" + "\r" + HEADER_2D);

                    sw.Write(HEADER_2E + "0\t\r" + HEADER_2F);
                    

                    sw.Write(HEADER_3);

                    int totalInstances = 0;
                    foreach (Code code in file)
                    {
                        totalInstances += code.InstanceCount;
                    }
                    
                    sw.Write(HEADER_4, file.CodeCount.ToString(), Path.GetFileName(theFilePath), totalInstances.ToString(), "");
                    
                    sw.Write(HEADER_5);

                    StringBuilder temp = new StringBuilder();
                    for (int i = 0; i < file.MaxInstances; i++)
                    {
                        temp.Append("start:" + (i + 1).ToString() + "\tend:" + (i + 1).ToString() + "\t");
                    }

                    sw.Write(TABLE_HEADER, temp.ToString());

                    foreach (Code code in file)
                    {
                        sw.Write(code.Name + "\t19\t" + code.InstanceCount + "\t");

                        foreach (Instance instance in code)
                        {
                            sw.Write(instance.Start.ToString("0.0000000000") + "\t" + instance.End.ToString("0.0000000000") + "\t");
                        }

                        sw.Write("\r");
                    }

                    sw.Write("\r" + TABLE_HEADER_2);

                    foreach (Code code in file)
                    {
                        sw.Write(code.Name + "\t\x3f\x3f" + code.InstanceCount + "\t\r");

                        for (int i = 0; i < code.InstanceCount; i++)
                        {
                            sw.Write("\t" + (i+1).ToString() + "\t0\t\r");
                        }
                    }

                    sw.Write(TABLE_HEADER_3);

                    foreach (Code code in file)
                    {
                        sw.Write(code.R + "\t" + code.G + "\t" + code.B + "\r");
                    }

                    sw.Write(TABLE_HEADER_4);

                    foreach (Code code in file)
                    {
                        for (int i = 0; i < code.InstanceCount; i++)
                        {
                            sw.Write("x\t");
                        }

                        sw.Write("\r");
                    }

                    sw.Write(TABLE_FOOTER_4);

                    sw.Write("TL ID:\r" + String.Format("{0:0}",randID) + "\r\r");

                    sw.Write(TABLE_FOOTER_5);

                    sw.Write("<row names CF>\r");

                    int j = 1;
                    foreach (Code code in file)
                    {
                        sw.Write(j.ToString() + "\t\x3f\x3f");
                        ++j;

                        foreach (char c in code.Name)
                        {
                            sw.Write(c);
                            sw.Write("\x00");
                        }

                        sw.Write("\x03\r");
                    }

                    sw.Write("</row names CF>\r\r");

                    sw.Write(TABLE_FOOTER_6);

                    sw.Write("<UNIQUE ROW IDs>\r");

                    foreach (Code code in file)
                    {
                        double rowID = random.NextDouble();
                        while (rowID < 10000000000000000000)
                        {
                            rowID *= 10;
                        }

                        sw.Write(String.Format("{0:0}\r", rowID));
                    }

                    sw.Write("\r\r");

                    sw.Write(TABLE_FOOTER_7);
                }

                retVal = true;
            }
            catch
            {
            }

            return retVal;
        }

        #endregion

    }
}
