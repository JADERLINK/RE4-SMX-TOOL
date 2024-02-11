using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_SMX_TOOL
{
    public static class SmxRepack
    {
        public static void ToSmx(SMX[] SMXarr, FileInfo info, bool isPS2)
        {
            byte amount = (byte)SMXarr.Length;
            byte[] header = new byte[0x10];
            header[0x00] = 0x10;
            header[0x01] = amount;

            var bw = new BinaryWriter(info.Create());
            bw.Write(header);

            for (int i = 0; i < amount; i++)
            {
                MakeSmxLine(ref bw, SMXarr[i], isPS2);
            }

            bw.Close();
        }

        private static void MakeSmxLine(ref BinaryWriter bw, SMX smx, bool isPS2) 
        {
            byte[] line = new byte[144];

            line[0x00] = smx.UseSMXID;
            line[0x01] = smx.Mode;
            line[0x02] = smx.OpacityHierarchy;
            line[0x03] = smx.FaceCulling;
            BitConverter.GetBytes(smx.LightSwitch).CopyTo(line, 0x04);
            line[0x08] = smx.AlphaHierarchy;
            line[0x09] = smx.UnknownX09;
            line[0x0A] = smx.UnknownX0A;
            line[0x0B] = smx.UnknownX0B;
            line[0x0C] = smx.ColorRGB[0];
            line[0x0D] = smx.ColorRGB[1];
            line[0x0E] = smx.ColorRGB[2];
            line[0x0F] = smx.ColorAlpha;

            //----------
            //mode 0x00
            BitConverter.GetBytes(smx.UnknownU10).CopyTo(line, 0x10);
            BitConverter.GetBytes(smx.UnknownU14).CopyTo(line, 0x14);
            BitConverter.GetBytes(smx.UnknownU18).CopyTo(line, 0x18);
            BitConverter.GetBytes(smx.UnknownU1C).CopyTo(line, 0x1C);
            BitConverter.GetBytes(smx.UnknownU20).CopyTo(line, 0x20);
            BitConverter.GetBytes(smx.UnknownU24).CopyTo(line, 0x24);
            BitConverter.GetBytes(smx.UnknownU28).CopyTo(line, 0x28);
            BitConverter.GetBytes(smx.UnknownU2C).CopyTo(line, 0x2C);
            BitConverter.GetBytes(smx.UnknownU30).CopyTo(line, 0x30);
            BitConverter.GetBytes(smx.UnknownU34).CopyTo(line, 0x34);
            BitConverter.GetBytes(smx.UnknownU38).CopyTo(line, 0x38);
            BitConverter.GetBytes(smx.UnknownU3C).CopyTo(line, 0x3C);
            BitConverter.GetBytes(smx.UnknownU40).CopyTo(line, 0x40);
            BitConverter.GetBytes(smx.UnknownU44).CopyTo(line, 0x44);
            BitConverter.GetBytes(smx.UnknownU48).CopyTo(line, 0x48);
            BitConverter.GetBytes(smx.UnknownU4C).CopyTo(line, 0x4C);
            BitConverter.GetBytes(smx.UnknownU50).CopyTo(line, 0x50);
            BitConverter.GetBytes(smx.UnknownU54).CopyTo(line, 0x54);
            BitConverter.GetBytes(smx.UnknownU58).CopyTo(line, 0x58);
            BitConverter.GetBytes(smx.UnknownU5C).CopyTo(line, 0x5C);
            BitConverter.GetBytes(smx.UnknownU60).CopyTo(line, 0x60);
            BitConverter.GetBytes(smx.UnknownU64).CopyTo(line, 0x64);
            BitConverter.GetBytes(smx.UnknownU68).CopyTo(line, 0x68);
            BitConverter.GetBytes(smx.UnknownU6C).CopyTo(line, 0x6C);
            BitConverter.GetBytes(smx.UnknownU70).CopyTo(line, 0x70);
            BitConverter.GetBytes(smx.UnknownU74).CopyTo(line, 0x74);
            BitConverter.GetBytes(smx.UnknownU78).CopyTo(line, 0x78);
            BitConverter.GetBytes(smx.UnknownU7C).CopyTo(line, 0x7C);
            BitConverter.GetBytes(smx.UnknownU80).CopyTo(line, 0x80);
            BitConverter.GetBytes(smx.UnknownU84).CopyTo(line, 0x84);
            //TextureMovement
            BitConverter.GetBytes(smx.TextureMovement_X).CopyTo(line, 0x88);
            BitConverter.GetBytes(smx.TextureMovement_Y).CopyTo(line, 0x8C);

            //----------
            //mode 0x02
            if (smx.Mode == 0x02)
            {
                BitConverter.GetBytes(smx.Swing0).CopyTo(line, 0x10);
                BitConverter.GetBytes(smx.Swing1).CopyTo(line, 0x14);
                BitConverter.GetBytes(smx.Swing2).CopyTo(line, 0x18);
                BitConverter.GetBytes(smx.Swing3).CopyTo(line, 0x1C);
                BitConverter.GetBytes(smx.Swing4).CopyTo(line, 0x20);
                BitConverter.GetBytes(smx.Swing5).CopyTo(line, 0x24);
                BitConverter.GetBytes(smx.Swing6).CopyTo(line, 0x28);
                BitConverter.GetBytes(smx.Swing7).CopyTo(line, 0x2C);
                BitConverter.GetBytes(smx.Swing8).CopyTo(line, 0x30);
                BitConverter.GetBytes(smx.Swing9).CopyTo(line, 0x34);
                BitConverter.GetBytes(smx.SwingA).CopyTo(line, 0x38);
                BitConverter.GetBytes(smx.SwingB).CopyTo(line, 0x3C);
                BitConverter.GetBytes(smx.SwingC).CopyTo(line, 0x40);
            }

            //---------
            //mode 0x01
            if (smx.Mode == 0x01)
            {
                BitConverter.GetBytes(smx.RotationSpeed_X).CopyTo(line, 0x10);
                BitConverter.GetBytes(smx.RotationSpeed_Y).CopyTo(line, 0x14);
                BitConverter.GetBytes(smx.RotationSpeed_Z).CopyTo(line, 0x18);
                if (isPS2)
                {
                    BitConverter.GetBytes(smx.RotationSpeed_W).CopyTo(line, 0x1C);
                    BitConverter.GetBytes(smx.Unknown_GTU).CopyTo(line, 0x20);
                }
                else
                {
                    BitConverter.GetBytes(smx.Unknown_GTU).CopyTo(line, 0x1C);
                    BitConverter.GetBytes(smx.Unknown_GTV).CopyTo(line, 0x20);
                }
            }

            bw.Write(line);
        }


    }
}