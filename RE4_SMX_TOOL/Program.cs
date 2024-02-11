using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_SMX_TOOL
{
    class Program
    {
        public static string Version = "B.1.0.0.0 (2024-02-10)";

        public static string headerText()
        {
            return "# RE4_SMX_TOOL" + Environment.NewLine +
                   "# by: JADERLINK" + Environment.NewLine +
                  $"# Version {Version}";
        }

        static void Main(string[] args)
        {
            Console.WriteLine(headerText());

            if (args.Length == 0)
            {
                Console.WriteLine("For more information read:");
                Console.WriteLine("https://github.com/JADERLINK/RE4-SMX-TOOL");
            }
            else if (args.Length >= 1 && File.Exists(args[0]))
            {
                bool isPS2 = false;

                if (args.Length >= 2 && args[1].ToUpper().Contains("TRUE"))
                {
                    isPS2 = true;
                }

                FileInfo fileInfo = new FileInfo(args[0]);
                string baseName = fileInfo.FullName.Remove(fileInfo.FullName.Length - fileInfo.Extension.Length, fileInfo.Extension.Length);
                Console.WriteLine(fileInfo.Name);

                if (fileInfo.Extension.ToUpperInvariant() == ".SMX")
                {
                    try
                    {
                        var stream = fileInfo.OpenRead();
                        var lines = SMXextract.extract(stream);
                        stream.Close();
                        var smxList = SMXextract.ToSmx(lines, isPS2);
                        FileInfo idxFile = new FileInfo(baseName + ".idxsmx");
                        SmxOutput.ToIdxSmx(smxList, idxFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }
                }
                else if (fileInfo.Extension.ToUpperInvariant() == ".IDXSMX")
                {
                    try
                    {
                        var stream = fileInfo.OpenRead();
                        var smxArr = ReadIdxSmx.Read(stream);
                        stream.Close();
                        FileInfo smxFile = new FileInfo(baseName + ".SMX");
                        SmxRepack.ToSmx(smxArr, smxFile, isPS2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }

                }
                else
                {
                    Console.WriteLine("Wrong file");
                }

            }
            else
            {
                Console.WriteLine("The file does not exist");
            }


            Console.WriteLine("End");


        }
    }
}
