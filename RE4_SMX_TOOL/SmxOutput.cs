using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_SMX_TOOL
{
    public static class SmxOutput
    {
        public static void ToIdxSmx(List<SMX> SMXlist, FileInfo info) 
        {
            var inv = System.Globalization.CultureInfo.InvariantCulture;

            var text = info.CreateText();
            text.WriteLine(Program.headerText());
            text.WriteLine();
            text.WriteLine();

            for (int i = 0; i < SMXlist.Count; i++)
            {
                var smx = SMXlist[i];

                text.WriteLine("UseSMXID:" + smx.UseSMXID.ToString("D3", inv));
                text.WriteLine("Mode:" + smx.Mode.ToString("X2", inv));
                text.WriteLine("OpacityHierarchy:" + smx.OpacityHierarchy.ToString("X2", inv));
                text.WriteLine("FaceCulling:" + smx.FaceCulling.ToString("X2", inv));
                PrintLightSwitch(text, smx.LightSwitch);
                text.WriteLine("AlphaHierarchy:" + smx.AlphaHierarchy.ToString("X2", inv));
                text.WriteLine("UnknownX09:" + smx.UnknownX09.ToString("X2", inv));
                text.WriteLine("UnknownX0A:" + smx.UnknownX0A.ToString("X2", inv));
                text.WriteLine("UnknownX0B:" + smx.UnknownX0B.ToString("X2", inv));
                text.WriteLine("ColorRGB:" + smx.ColorRGB[0].ToString("X2", inv) + smx.ColorRGB[1].ToString("X2", inv) + smx.ColorRGB[2].ToString("X2", inv)); // 3 bytes
                text.WriteLine("ColorAlpha:" + smx.ColorAlpha.ToString("X2", inv));

                //----------
                //mode 0x02
                if (smx.Mode == 2)
                {
                    text.WriteLine("Swing0:" + smx.Swing0.ToString("F9", inv));
                    text.WriteLine("Swing1:" + smx.Swing1.ToString("F9", inv));
                    text.WriteLine("Swing2:" + smx.Swing2.ToString("F9", inv));
                    text.WriteLine("Swing3:" + smx.Swing3.ToString("F9", inv));
                    text.WriteLine("Swing4:" + smx.Swing4.ToString("F9", inv));
                    text.WriteLine("Swing5:" + smx.Swing5.ToString("F9", inv));
                    text.WriteLine("Swing6:" + smx.Swing6.ToString("F9", inv));
                    text.WriteLine("Swing7:" + smx.Swing7.ToString("F9", inv));
                    text.WriteLine("Swing8:" + smx.Swing8.ToString("F9", inv));
                    text.WriteLine("Swing9:" + smx.Swing9.ToString("F9", inv));
                    text.WriteLine("SwingA:" + smx.SwingA.ToString("F9", inv));
                    text.WriteLine("SwingB:" + smx.SwingB.ToString("F9", inv));
                    text.WriteLine("SwingC:" + smx.SwingC.ToString("F9", inv));
                }


                //----------
                //mode 0x01
                if (smx.Mode == 1)
                {
                    text.WriteLine("RotationSpeed_X:" + smx.RotationSpeed_X.ToString("F9", inv));
                    text.WriteLine("RotationSpeed_Y:" + smx.RotationSpeed_Y.ToString("F9", inv));
                    text.WriteLine("RotationSpeed_Z:" + smx.RotationSpeed_Z.ToString("F9", inv));
                    text.WriteLine("RotationSpeed_W:" + smx.RotationSpeed_W.ToString("F9", inv));
                    text.WriteLine("Unknown_GTU:" + smx.Unknown_GTU.ToString("X8", inv));
                    text.WriteLine("Unknown_GTV:" + smx.Unknown_GTV.ToString("X8", inv));
                }

                //----------
                //mode 0x00
                if (smx.Mode != 1 && smx.Mode != 2)
                {
                    text.WriteLine("UnknownU10:" + smx.UnknownU10.ToString("X8", inv));
                    text.WriteLine("UnknownU14:" + smx.UnknownU14.ToString("X8", inv));
                    text.WriteLine("UnknownU18:" + smx.UnknownU18.ToString("X8", inv));
                    text.WriteLine("UnknownU1C:" + smx.UnknownU1C.ToString("X8", inv));
                    text.WriteLine("UnknownU20:" + smx.UnknownU20.ToString("X8", inv));
                }
                if (smx.Mode != 2)
                {
                    text.WriteLine("UnknownU24:" + smx.UnknownU24.ToString("X8", inv));
                    text.WriteLine("UnknownU28:" + smx.UnknownU28.ToString("X8", inv));
                    text.WriteLine("UnknownU2C:" + smx.UnknownU2C.ToString("X8", inv));
                    text.WriteLine("UnknownU30:" + smx.UnknownU30.ToString("X8", inv));
                    text.WriteLine("UnknownU34:" + smx.UnknownU34.ToString("X8", inv));
                    text.WriteLine("UnknownU38:" + smx.UnknownU38.ToString("X8", inv));
                    text.WriteLine("UnknownU3C:" + smx.UnknownU3C.ToString("X8", inv));
                    text.WriteLine("UnknownU40:" + smx.UnknownU40.ToString("X8", inv));
                }
                
                text.WriteLine("UnknownU44:" + smx.UnknownU44.ToString("X8", inv));
                text.WriteLine("UnknownU48:" + smx.UnknownU48.ToString("X8", inv));
                text.WriteLine("UnknownU4C:" + smx.UnknownU4C.ToString("X8", inv));
                text.WriteLine("UnknownU50:" + smx.UnknownU50.ToString("X8", inv));
                text.WriteLine("UnknownU54:" + smx.UnknownU54.ToString("X8", inv));
                text.WriteLine("UnknownU58:" + smx.UnknownU58.ToString("X8", inv));
                text.WriteLine("UnknownU5C:" + smx.UnknownU5C.ToString("X8", inv));
                text.WriteLine("UnknownU60:" + smx.UnknownU60.ToString("X8", inv));
                text.WriteLine("UnknownU64:" + smx.UnknownU64.ToString("X8", inv));
                text.WriteLine("UnknownU68:" + smx.UnknownU68.ToString("X8", inv));
                text.WriteLine("UnknownU6C:" + smx.UnknownU6C.ToString("X8", inv));
                text.WriteLine("UnknownU70:" + smx.UnknownU70.ToString("X8", inv));
                text.WriteLine("UnknownU74:" + smx.UnknownU74.ToString("X8", inv));
                text.WriteLine("UnknownU78:" + smx.UnknownU78.ToString("X8", inv));
                text.WriteLine("UnknownU7C:" + smx.UnknownU7C.ToString("X8", inv));
                text.WriteLine("UnknownU80:" + smx.UnknownU80.ToString("X8", inv));
                text.WriteLine("UnknownU84:" + smx.UnknownU84.ToString("X8", inv));
                text.WriteLine("TextureMovement_X:" + smx.TextureMovement_X.ToString("F9", inv));
                text.WriteLine("TextureMovement_Y:" + smx.TextureMovement_Y.ToString("F9", inv));

                text.WriteLine();
                text.WriteLine();
            }

            text.Close();
        }

        private static void PrintLightSwitch(StreamWriter text, uint LightSwitch) 
        {
            text.WriteLine("LightSwitch_00:" + LightSwitchValue(LightSwitch, 0x01));
            text.WriteLine("LightSwitch_01:" + LightSwitchValue(LightSwitch, 0x02));
            text.WriteLine("LightSwitch_02:" + LightSwitchValue(LightSwitch, 0x04));
            text.WriteLine("LightSwitch_03:" + LightSwitchValue(LightSwitch, 0x08));
            text.WriteLine("LightSwitch_04:" + LightSwitchValue(LightSwitch, 0x10));
            text.WriteLine("LightSwitch_05:" + LightSwitchValue(LightSwitch, 0x20));
            text.WriteLine("LightSwitch_06:" + LightSwitchValue(LightSwitch, 0x40));
            text.WriteLine("LightSwitch_07:" + LightSwitchValue(LightSwitch, 0x80));
            text.WriteLine("LightSwitch_08:" + LightSwitchValue(LightSwitch, 0x0100));
            text.WriteLine("LightSwitch_09:" + LightSwitchValue(LightSwitch, 0x0200));
            text.WriteLine("LightSwitch_10:" + LightSwitchValue(LightSwitch, 0x0400));
            text.WriteLine("LightSwitch_11:" + LightSwitchValue(LightSwitch, 0x0800));
            text.WriteLine("LightSwitch_12:" + LightSwitchValue(LightSwitch, 0x1000));
            text.WriteLine("LightSwitch_13:" + LightSwitchValue(LightSwitch, 0x2000));
            text.WriteLine("LightSwitch_14:" + LightSwitchValue(LightSwitch, 0x4000));
            text.WriteLine("LightSwitch_15:" + LightSwitchValue(LightSwitch, 0x8000));
            text.WriteLine("LightSwitch_16:" + LightSwitchValue(LightSwitch, 0x010000));
            text.WriteLine("LightSwitch_17:" + LightSwitchValue(LightSwitch, 0x020000));
            text.WriteLine("LightSwitch_18:" + LightSwitchValue(LightSwitch, 0x040000));
            text.WriteLine("LightSwitch_19:" + LightSwitchValue(LightSwitch, 0x080000));
            text.WriteLine("LightSwitch_20:" + LightSwitchValue(LightSwitch, 0x100000));
            text.WriteLine("LightSwitch_21:" + LightSwitchValue(LightSwitch, 0x200000));
            text.WriteLine("LightSwitch_22:" + LightSwitchValue(LightSwitch, 0x400000));
            text.WriteLine("LightSwitch_23:" + LightSwitchValue(LightSwitch, 0x800000));
            text.WriteLine("LightSwitch_24:" + LightSwitchValue(LightSwitch, 0x01000000));
            text.WriteLine("LightSwitch_25:" + LightSwitchValue(LightSwitch, 0x02000000));
            text.WriteLine("LightSwitch_26:" + LightSwitchValue(LightSwitch, 0x04000000));
            text.WriteLine("LightSwitch_27:" + LightSwitchValue(LightSwitch, 0x08000000));
            text.WriteLine("LightSwitch_28:" + LightSwitchValue(LightSwitch, 0x10000000));
            text.WriteLine("LightSwitch_29:" + LightSwitchValue(LightSwitch, 0x20000000));
            text.WriteLine("LightSwitch_30:" + LightSwitchValue(LightSwitch, 0x40000000));
            text.WriteLine("LightSwitch_31:" + LightSwitchValue(LightSwitch, 0x80000000));
        }

        private static string LightSwitchValue(uint LightSwitch, uint mask)
        {
            uint check = LightSwitch & mask;
            if (check == mask)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }


    }
}
