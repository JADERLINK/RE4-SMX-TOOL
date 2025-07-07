using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4_SMX_TOOL_UHD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("# RE4_SMX_TOOL_UHD");
            RE4_SMX_TOOL.MainProgram.Start(args, SimpleEndianBinaryIO.Endianness.LittleEndian, false);
        }
    }
}
