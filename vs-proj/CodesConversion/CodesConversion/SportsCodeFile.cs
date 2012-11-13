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
        private const String HEADER_1 = "GameBreaker 10\n<version5.4>\n<ID>\t{0:0}\n" +
                                        "<Movie PKG ID>\tPKG_ID 000000000000 unimplemented\t</Movie PKG ID>\n" +
                                        "<long name Movie>\t</long name Movie>\n\n";

        // {0},{1},{2} = File Paths
        private const String HEADER_2 = "<timeline full parent path>\n1\t" + "\xff\xfe" +
                                        "file://{0}" + "\x03" + "\n" +
                                        "</timeline full parent path>\n\n" +
                                        "<timeline full file path>\n1\t" + "\xff\xfe" +
                                        "file://{1}" + "\x03" + "\n" +
                                        "</timeline full file path>\n\n" +
                                        "<linked movie full file path>\n1\t" + "\xff\xfe" +
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
        private const String HEADER_5 = "{0}\t-102\t34016\t0.00000000\t{1}\t{2}\t3435077624\t\n\n";

        // {0} = String of Instance Start/End Headers "start:#\tend:#\t"
        private const String TABLE_HEADER = "Category: \trow colour:\t# instances\t{0}\n";

        private const String TABLE_HEADER_2 = "Category:\tInstances:\tNum labels\txLabel data format - label type, then data eg string type, then string, for vectors - include 3 values\n";

        private const String TABLE_HEADER_3 = "\n0\n\n0\n\n0\t0\t0\t0\n\n" + "CODE_MATRIX_ORGANISER_RECT\t800\t86\t1270\t400\n" + "NEW COLOURS\n";

        private const String TABLE_HEADER_4 = "TXT:	0\nCHAP:\n0\n1586\n0\n\nUNIQUE IDs\n";

        private const String TABLE_FOOTER_4 = "STRIP MARKERS:\t0\t-100000.0000000000\t100000.0000000000\t-10000.0000000000\t100000.0000000000\n\n" +
                                              "<start DRAWING - instance info>\n" +
                                              "<Nu instances:>\t0\n" +
                                              "<end DRAWING>\n\n";

        private const String TABLE_FOOTER_5 = "<free text CFStrings>\n<count>\t0\n</free text CFStrings>\n\n" +
                                              "<free text Multibyte CFStrings>\n<count>\t0\n</free text Multibyte CFStrings>\nPopup menu:\t1\n<Static text control>\t0\n\n" +
                                              "<instance label variation data>\n</instance label variation data>\n\n" +
                                              "<mutable array labels>\n</mutable array labels>\n\n" +
                                              "<labels list data>\n<no popup window>\n</labels list data>\n\n";

        private const String TABLE_FOOTER_6 = "<Overlay text control>\t0\n\n" +
                                              "<TIMELINE_MOVIETIME_DISPLAY_OPTIONS>\n" +
                                              "0.0000000000\t0\t0\n\n";

        private const String TABLE_FOOTER_7 = "<note_lines>\n" +
                                              "3\n\n" +
                                              "<END TIMELINE>\n";


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

                    sw.Write(String.Format(HEADER_2, "1","2","3"));

                    sw.Write(HEADER_3);

                    int totalInstances = 0;
                    foreach (Code code in file)
                    {
                        totalInstances += code.InstanceCount;
                    }


                    sw.Write(HEADER_4, file.CodeCount.ToString(), Path.GetFileName(theFilePath), totalInstances.ToString(), "");
                    
                    sw.Write(HEADER_5, "", "", "");

                    StringBuilder temp = new StringBuilder();
                    for (int i = 0; i < file.CodeCount; i++)
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

                        sw.Write("\n");
                    }

                    sw.Write("\n" + TABLE_HEADER_2);

                    foreach (Code code in file)
                    {
                        sw.Write(code.Name + "\t\xca\xca" + code.InstanceCount + "\t\n");

                        for (int i = 0; i < code.InstanceCount; i++)
                        {
                            sw.Write("\t" + (i+1).ToString() + "\t0\t\n");
                        }
                    }

                    sw.Write(TABLE_HEADER_3);

                    foreach (Code code in file)
                    {
                        sw.Write(code.R + "\t" + code.G + "\t" + code.B + "\n");
                    }

                    sw.Write(TABLE_HEADER_4);

                    foreach (Code code in file)
                    {
                        for (int i = 0; i < code.InstanceCount; i++)
                        {
                            sw.Write("x\t");
                        }

                        sw.Write("\n");
                    }

                    sw.Write(TABLE_FOOTER_4);

                    sw.Write("TL ID:\n" + String.Format("{0:0}",randID) + "\n\n");

                    sw.Write(TABLE_FOOTER_5);

                    sw.Write("<row names CF>\n");

                    int j = 1;
                    foreach (Code code in file)
                    {
                        sw.Write(j.ToString() + "\t\xff\xfe");
                        ++j;

                        foreach (char c in code.Name)
                        {
                            sw.Write(c);
                            sw.Write("\x00");
                        }

                        sw.Write("\x03\n");
                    }

                    sw.Write("</row names CF>\n\n");

                    sw.Write(TABLE_FOOTER_6);

                    sw.Write("<UNIQUE ROW IDs>\n");

                    foreach (Code code in file)
                    {
                        double rowID = random.NextDouble();
                        while (rowID < 10000000000000000000)
                        {
                            rowID *= 10;
                        }

                        sw.Write(String.Format("{0:0}\n", rowID));
                    }

                    sw.Write("\n\n");

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
