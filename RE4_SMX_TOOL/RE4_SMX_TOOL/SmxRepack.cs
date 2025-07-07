using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SimpleEndianBinaryIO;

namespace RE4_SMX_TOOL
{
    public static class SmxRepack
    {
        public static void ToSmx(SMX[] SMXarr, FileInfo info, Endianness endianness, bool isPS2)
        {
            byte amount = (byte)SMXarr.Length;
            byte[] header = new byte[0x10];
            header[0x00] = 0x10;
            header[0x01] = amount;

            var bw = new EndianBinaryWriter(info.Create(), endianness);
            bw.Write(header);

            for (int i = 0; i < amount; i++)
            {
                MakeSmxLine(ref bw, SMXarr[i], endianness, isPS2);
            }

            bw.Close();
        }

        private static void MakeSmxLine(ref EndianBinaryWriter bw, SMX smx, Endianness endianness, bool isPS2) 
        {
            byte[] line = new byte[144];

            line[0x00] = smx.UseSMXID; //uint8_t ModelNo; // Index
            line[0x01] = smx.Mode; // uint8_t Id; // Type (enum MOVE_TYPE)
            line[0x02] = smx.OpacityHierarchy; //uint8_t OtType; // Render Heirarchy (enum OT_TYPES)
            line[0x03] = smx.FaceCulling; //uint8_t CullMode; (enum CULL_MODE)
            EndianBitConverter.GetBytes(smx.LightSwitch, endianness).CopyTo(line, 0x04); //uint32_t LitSelectMask;

            if (endianness == Endianness.LittleEndian) //uint32_t Flag; (enum SMX_FLAGS)
            {
                line[0x08] = smx.AlphaHierarchy;
                line[0x09] = smx.UnknownX09;
                line[0x0A] = smx.UnknownX0A;
                line[0x0B] = smx.UnknownX0B;
            }
            else 
            {
                line[0x0B] = smx.AlphaHierarchy;
                line[0x0A] = smx.UnknownX09;
                line[0x09] = smx.UnknownX0A;
                line[0x08] = smx.UnknownX0B;
            }

            //GXColor MaterialColor; (struct GXColor) no alpha
            line[0x0C] = smx.ColorRGB[0];
            line[0x0D] = smx.ColorRGB[1];
            line[0x0E] = smx.ColorRGB[2];
            // (enum BLENDING_TYPES)
            line[0x0F] = smx.ColorAlpha;

            /* "union" fields here */

            //é sempre lido como little endian para compatibilidade;
            BitConverter.GetBytes(smx.UnknownU84).CopyTo(line, 0x84);

            //TextureMovement
            EndianBitConverter.GetBytes(smx.TextureMovement_X, endianness).CopyTo(line, 0x88); //TexU; (float value)
            EndianBitConverter.GetBytes(smx.TextureMovement_Y, endianness).CopyTo(line, 0x8C); //TexV; (float value)


            //----------
            //mode 0x00
            EndianBitConverter.GetBytes(smx.UnknownU10, endianness).CopyTo(line, 0x10);
            EndianBitConverter.GetBytes(smx.UnknownU14, endianness).CopyTo(line, 0x14);
            EndianBitConverter.GetBytes(smx.UnknownU18, endianness).CopyTo(line, 0x18);
            EndianBitConverter.GetBytes(smx.UnknownU1C, endianness).CopyTo(line, 0x1C);
            EndianBitConverter.GetBytes(smx.UnknownU20, endianness).CopyTo(line, 0x20);
            EndianBitConverter.GetBytes(smx.UnknownU24, endianness).CopyTo(line, 0x24);
            EndianBitConverter.GetBytes(smx.UnknownU28, endianness).CopyTo(line, 0x28);
            EndianBitConverter.GetBytes(smx.UnknownU2C, endianness).CopyTo(line, 0x2C);
            EndianBitConverter.GetBytes(smx.UnknownU30, endianness).CopyTo(line, 0x30);
            EndianBitConverter.GetBytes(smx.UnknownU34, endianness).CopyTo(line, 0x34);
            EndianBitConverter.GetBytes(smx.UnknownU38, endianness).CopyTo(line, 0x38);
            EndianBitConverter.GetBytes(smx.UnknownU3C, endianness).CopyTo(line, 0x3C);
            EndianBitConverter.GetBytes(smx.UnknownU40, endianness).CopyTo(line, 0x40);
            EndianBitConverter.GetBytes(smx.UnknownU44, endianness).CopyTo(line, 0x44);
            EndianBitConverter.GetBytes(smx.UnknownU48, endianness).CopyTo(line, 0x48);
            EndianBitConverter.GetBytes(smx.UnknownU4C, endianness).CopyTo(line, 0x4C);
            EndianBitConverter.GetBytes(smx.UnknownU50, endianness).CopyTo(line, 0x50);
            EndianBitConverter.GetBytes(smx.UnknownU54, endianness).CopyTo(line, 0x54);
            EndianBitConverter.GetBytes(smx.UnknownU58, endianness).CopyTo(line, 0x58);
            EndianBitConverter.GetBytes(smx.UnknownU5C, endianness).CopyTo(line, 0x5C);
            EndianBitConverter.GetBytes(smx.UnknownU60, endianness).CopyTo(line, 0x60);
            EndianBitConverter.GetBytes(smx.UnknownU64, endianness).CopyTo(line, 0x64);
            EndianBitConverter.GetBytes(smx.UnknownU68, endianness).CopyTo(line, 0x68);
            EndianBitConverter.GetBytes(smx.UnknownU6C, endianness).CopyTo(line, 0x6C);
            EndianBitConverter.GetBytes(smx.UnknownU70, endianness).CopyTo(line, 0x70);
            EndianBitConverter.GetBytes(smx.UnknownU74, endianness).CopyTo(line, 0x74);
            EndianBitConverter.GetBytes(smx.UnknownU78, endianness).CopyTo(line, 0x78);
            EndianBitConverter.GetBytes(smx.UnknownU7C, endianness).CopyTo(line, 0x7C);
            EndianBitConverter.GetBytes(smx.UnknownU80, endianness).CopyTo(line, 0x80);
          

            //----------
            //mode 0x02
            if (smx.Mode == 0x02)
            {
                EndianBitConverter.GetBytes(smx.Swing0, endianness).CopyTo(line, 0x10); //m_StartZ
                EndianBitConverter.GetBytes(smx.Swing1, endianness).CopyTo(line, 0x14); //m_RangeZ
                EndianBitConverter.GetBytes(smx.Swing2, endianness).CopyTo(line, 0x18); //m_SpeedZ
                EndianBitConverter.GetBytes(smx.Swing3, endianness).CopyTo(line, 0x1C); //m_Time
                EndianBitConverter.GetBytes(smx.Swing4, endianness).CopyTo(line, 0x20); //m_StartX
                EndianBitConverter.GetBytes(smx.Swing5, endianness).CopyTo(line, 0x24); //m_RangeX
                EndianBitConverter.GetBytes(smx.Swing6, endianness).CopyTo(line, 0x28); //m_SpeedX
                EndianBitConverter.GetBytes(smx.Swing7, endianness).CopyTo(line, 0x2C); //m_StartY
                EndianBitConverter.GetBytes(smx.Swing8, endianness).CopyTo(line, 0x30); //m_RangeY
                EndianBitConverter.GetBytes(smx.Swing9, endianness).CopyTo(line, 0x34); //m_SpeedY
                EndianBitConverter.GetBytes(smx.SwingA, endianness).CopyTo(line, 0x38); //m_InitAngX
                EndianBitConverter.GetBytes(smx.SwingB, endianness).CopyTo(line, 0x3C); //m_InitAngY
                EndianBitConverter.GetBytes(smx.SwingC, endianness).CopyTo(line, 0x40); //m_InitAngZ
            }

            //---------
            //mode 0x01
            if (smx.Mode == 0x01)
            {
                EndianBitConverter.GetBytes(smx.RotationSpeed_X, endianness).CopyTo(line, 0x10); //m_tagVec Rot; (x) float
                EndianBitConverter.GetBytes(smx.RotationSpeed_Y, endianness).CopyTo(line, 0x14); //m_tagVec Rot; (y) float
                EndianBitConverter.GetBytes(smx.RotationSpeed_Z, endianness).CopyTo(line, 0x18); //m_tagVec Rot; (z) float
                if (isPS2)
                {
                    EndianBitConverter.GetBytes(smx.RotationSpeed_W, endianness).CopyTo(line, 0x1C); //m_tagVec Rot; (w) float
                    // é sempre lido como little endian para compatibilidade;
                    BitConverter.GetBytes(smx.Unknown_GTU).CopyTo(line, 0x20); //uint8_t m_Flag; (unknown type, can be 0 or 1)
                }
                else
                {
                    // é sempre lido como little endian para compatibilidade;
                    BitConverter.GetBytes(smx.Unknown_GTU).CopyTo(line, 0x1C); //uint8_t m_Flag; (unknown type, can be 0 or 1)
                    //nada, o mesmo que UnknownU20
                    EndianBitConverter.GetBytes(smx.Unknown_GTV, endianness).CopyTo(line, 0x20);
                }
            }

            bw.Write(line);
        }


    }
}