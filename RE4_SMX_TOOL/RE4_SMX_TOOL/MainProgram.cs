using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SimpleEndianBinaryIO;

namespace RE4_SMX_TOOL
{
    internal class MainProgram
    {
        private static readonly string Version = "V.1.1 (2025-07-06)";

        public static string HeaderText()
        {
            return "# RE4_SMX_TOOL" + Environment.NewLine +
                   "# by: JADERLINK" + Environment.NewLine +
                   "# youtube.com/@JADERLINK" + Environment.NewLine +
                   "# github.com/JADERLINK" + Environment.NewLine +
                  $"# Version {Version}";
        }

        public static void Start(string[] args, Endianness endianness, bool isPS2)
        {
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Console.WriteLine("# by: JADERLINK");
            Console.WriteLine("# youtube.com/@JADERLINK");
            Console.WriteLine("# github.com/JADERLINK");
            Console.WriteLine($"# Version {Version}");
            Console.WriteLine("");

            bool usingBatFile = false;
            int start = 0;
            if (args.Length > 0 && args[0].ToLowerInvariant() == "-bat")
            {
                usingBatFile = true;
                start = 1;
            }

            for (int i = start; i < args.Length; i++)
            {
                if (File.Exists(args[i]))
                {
                    try
                    {
                        Continue(args[i], endianness, isPS2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + Environment.NewLine + ex);
                    }
                }
                else
                {
                    Console.WriteLine("File specified does not exist: " + args[i]);
                }

            }

            if (args.Length == 0)
            {
                Console.WriteLine("For more information read:");
                Console.WriteLine("https://github.com/JADERLINK/RE4-SMX-TOOL");
                Console.WriteLine("Press any key to close the console.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Finished!!!");
                if (!usingBatFile)
                {
                    Console.WriteLine("Press any key to close the console.");
                    Console.ReadKey();
                }
            }

        }

        private static void Continue(string filePath, Endianness endianness, bool isPS2)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine(fileInfo.Name);

            if (fileInfo.Extension.ToUpperInvariant() == ".SMX")
            {
                var stream = fileInfo.OpenRead();
                var lines = SMXextract.Extract(stream);
                stream.Close();
                var smxList = SMXextract.ToSmx(lines, endianness, isPS2);
                FileInfo idxFile = new FileInfo(Path.ChangeExtension(filePath, ".idxsmx"));
                SmxOutput.ToIdxSmx(smxList, idxFile);
            }
            else if (fileInfo.Extension.ToUpperInvariant() == ".IDXSMX")
            {
                var stream = fileInfo.OpenRead();
                var smxArr = ReadIdxSmx.Read(stream);
                stream.Close();
                FileInfo smxFile = new FileInfo(Path.ChangeExtension(filePath, ".SMX"));
                SmxRepack.ToSmx(smxArr, smxFile, endianness, isPS2);
            }
            else
            {
                Console.WriteLine("Invalid file!");
            }
        }

    }
}
