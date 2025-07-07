using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace RE4_SMX_TOOL
{
    public static class ReadIdxSmx
    {
        public static SMX[] Read(Stream stream) 
        {
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);

            Dictionary<byte, SMX> SmxDic = new Dictionary<byte, SMX>();

            SMX temp = new SMX();
            temp.ColorRGB = new byte[3];

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Trim().ToUpperInvariant();

                if (line == null
                    || line.Length == 0
                    || line.StartsWith("\\")
                    || line.StartsWith("/")
                    || line.StartsWith("#")
                    || line.StartsWith(":")
                    || line.StartsWith("!")
                    || line.StartsWith("@")
                    || line.StartsWith("=")
                    )
                {
                    continue;
                }
                else if (line.StartsWith("USESMXID"))
                {
                    temp = new SMX();
                    temp.ColorRGB = new byte[3];

                    var split = line.Split(':');
                    if (split.Length >= 2)
                    {
                        int ID = -1;
                        byte smdID = 0;

                        try
                        {
                            ID = byte.Parse(Utils.ReturnValidDecValue(split[1]), NumberStyles.Integer, CultureInfo.InvariantCulture);
                            smdID = (byte)ID;
                        }
                        catch (Exception)
                        {
                        }

                        if (ID > -1 && !SmxDic.ContainsKey(smdID))
                        {
                            temp.RotationSpeed_W = 1.0f;
                            temp.UseSMXID = smdID;
                            SmxDic.Add(smdID, temp);
                        }
                    }
                }

                else 
                {
                    _ = SetByteHex(ref line, "MODE", ref temp.Mode)
                        || SetByteHex(ref line, "OPACITYHIERARCHY", ref temp.OpacityHierarchy)
                        || SetByteHex(ref line, "FACECULLING", ref temp.FaceCulling)
                        || SetByteHex(ref line, "ALPHAHIERARCHY", ref temp.AlphaHierarchy)
                        || SetByteHex(ref line, "UNKNOWNX09", ref temp.UnknownX09)
                        || SetByteHex(ref line, "UNKNOWNX0A", ref temp.UnknownX0A)
                        || SetByteHex(ref line, "UNKNOWNX0B", ref temp.UnknownX0B)
                        || SetColorRGB(ref line, "COLORRGB", ref temp.ColorRGB)
                        || SetByteHex(ref line, "COLORALPHA", ref temp.ColorAlpha)
                        || SetUintHex(ref line, "UNKNOWNU10", ref temp.UnknownU10)
                        || SetUintHex(ref line, "UNKNOWNU14", ref temp.UnknownU14)
                        || SetUintHex(ref line, "UNKNOWNU18", ref temp.UnknownU18)
                        || SetUintHex(ref line, "UNKNOWNU1C", ref temp.UnknownU1C)
                        || SetUintHex(ref line, "UNKNOWNU20", ref temp.UnknownU20)
                        || SetUintHex(ref line, "UNKNOWNU24", ref temp.UnknownU24)
                        || SetUintHex(ref line, "UNKNOWNU28", ref temp.UnknownU28)
                        || SetUintHex(ref line, "UNKNOWNU2C", ref temp.UnknownU2C)
                        || SetUintHex(ref line, "UNKNOWNU30", ref temp.UnknownU30)
                        || SetUintHex(ref line, "UNKNOWNU34", ref temp.UnknownU34)
                        || SetUintHex(ref line, "UNKNOWNU38", ref temp.UnknownU38)
                        || SetUintHex(ref line, "UNKNOWNU3C", ref temp.UnknownU3C)
                        || SetUintHex(ref line, "UNKNOWNU40", ref temp.UnknownU40)
                        || SetUintHex(ref line, "UNKNOWNU44", ref temp.UnknownU44)
                        || SetUintHex(ref line, "UNKNOWNU48", ref temp.UnknownU48)
                        || SetUintHex(ref line, "UNKNOWNU4C", ref temp.UnknownU4C)
                        || SetUintHex(ref line, "UNKNOWNU50", ref temp.UnknownU50)
                        || SetUintHex(ref line, "UNKNOWNU54", ref temp.UnknownU54)
                        || SetUintHex(ref line, "UNKNOWNU58", ref temp.UnknownU58)
                        || SetUintHex(ref line, "UNKNOWNU5C", ref temp.UnknownU5C)
                        || SetUintHex(ref line, "UNKNOWNU60", ref temp.UnknownU60)
                        || SetUintHex(ref line, "UNKNOWNU64", ref temp.UnknownU64)
                        || SetUintHex(ref line, "UNKNOWNU68", ref temp.UnknownU68)
                        || SetUintHex(ref line, "UNKNOWNU6C", ref temp.UnknownU6C)
                        || SetUintHex(ref line, "UNKNOWNU70", ref temp.UnknownU70)
                        || SetUintHex(ref line, "UNKNOWNU74", ref temp.UnknownU74)
                        || SetUintHex(ref line, "UNKNOWNU78", ref temp.UnknownU78)
                        || SetUintHex(ref line, "UNKNOWNU7C", ref temp.UnknownU7C)
                        || SetUintHex(ref line, "UNKNOWNU80", ref temp.UnknownU80)
                        || SetUintHex(ref line, "UNKNOWNU84", ref temp.UnknownU84)
                        || SetUintHex(ref line, "UNKNOWN_GTU", ref temp.Unknown_GTU)
                        || SetUintHex(ref line, "UNKNOWN_GTV", ref temp.Unknown_GTV)
                        || SetFloatDec(ref line, "TEXTUREMOVEMENT_X", ref temp.TextureMovement_X)
                        || SetFloatDec(ref line, "TEXTUREMOVEMENT_Y", ref temp.TextureMovement_Y)
                        || SetFloatDec(ref line, "ROTATIONSPEED_X", ref temp.RotationSpeed_X)
                        || SetFloatDec(ref line, "ROTATIONSPEED_Y", ref temp.RotationSpeed_Y)
                        || SetFloatDec(ref line, "ROTATIONSPEED_Z", ref temp.RotationSpeed_Z)
                        || SetFloatDec(ref line, "ROTATIONSPEED_W", ref temp.RotationSpeed_W)
                        || SetFloatDec(ref line, "SWING0", ref temp.Swing0)
                        || SetFloatDec(ref line, "SWING1", ref temp.Swing1)
                        || SetFloatDec(ref line, "SWING2", ref temp.Swing2)
                        || SetFloatDec(ref line, "SWING3", ref temp.Swing3)
                        || SetFloatDec(ref line, "SWING4", ref temp.Swing4)
                        || SetFloatDec(ref line, "SWING5", ref temp.Swing5)
                        || SetFloatDec(ref line, "SWING6", ref temp.Swing6)
                        || SetFloatDec(ref line, "SWING7", ref temp.Swing7)
                        || SetFloatDec(ref line, "SWING8", ref temp.Swing8)
                        || SetFloatDec(ref line, "SWING9", ref temp.Swing9)
                        || SetFloatDec(ref line, "SWINGA", ref temp.SwingA)
                        || SetFloatDec(ref line, "SWINGB", ref temp.SwingB)
                        || SetFloatDec(ref line, "SWINGC", ref temp.SwingC)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_00", ref temp.LightSwitch, 0x01)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_01", ref temp.LightSwitch, 0x02)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_02", ref temp.LightSwitch, 0x04)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_03", ref temp.LightSwitch, 0x08)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_04", ref temp.LightSwitch, 0x10)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_05", ref temp.LightSwitch, 0x20)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_06", ref temp.LightSwitch, 0x40)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_07", ref temp.LightSwitch, 0x80)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_08", ref temp.LightSwitch, 0x0100)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_09", ref temp.LightSwitch, 0x0200)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_10", ref temp.LightSwitch, 0x0400)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_11", ref temp.LightSwitch, 0x0800)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_12", ref temp.LightSwitch, 0x1000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_13", ref temp.LightSwitch, 0x2000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_14", ref temp.LightSwitch, 0x4000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_15", ref temp.LightSwitch, 0x8000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_16", ref temp.LightSwitch, 0x010000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_17", ref temp.LightSwitch, 0x020000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_18", ref temp.LightSwitch, 0x040000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_19", ref temp.LightSwitch, 0x080000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_20", ref temp.LightSwitch, 0x100000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_21", ref temp.LightSwitch, 0x200000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_22", ref temp.LightSwitch, 0x400000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_23", ref temp.LightSwitch, 0x800000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_24", ref temp.LightSwitch, 0x01000000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_25", ref temp.LightSwitch, 0x02000000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_26", ref temp.LightSwitch, 0x04000000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_27", ref temp.LightSwitch, 0x08000000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_28", ref temp.LightSwitch, 0x10000000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_29", ref temp.LightSwitch, 0x20000000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_30", ref temp.LightSwitch, 0x40000000)
                        || SetLightSwitch(ref line, "LIGHTSWITCH_31", ref temp.LightSwitch, 0x80000000)
                        ;
                }

            }


            return SmxDic.Values.OrderBy(v => v.UseSMXID).ToArray();
        }

        private static bool SetByteHex(ref string line, string key, ref byte varToSet)
        {
            if (line.StartsWith(key))
            {
                var split = line.Split(':');
                if (split.Length >= 2)
                {
                    try
                    {
                        varToSet = byte.Parse(Utils.ReturnValidHexValue(split[1]), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                    }
                }
                return true;
            }
            return false;
        }

        private static bool SetFloatDec(ref string line, string key, ref float varToSet) 
        {
            if (line.StartsWith(key))
            {
                var split = line.Split(':');
                if (split.Length >= 2)
                {
                    try
                    {
                        varToSet = float.Parse(Utils.ReturnValidFloatValue(split[1]), NumberStyles.Float, CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                    }
                }
                return true;
            }
            return false;
        }

        private static bool SetUintHex(ref string line, string key, ref uint varToSet) 
        {
            if (line.StartsWith(key))
            {
                var split = line.Split(':');
                if (split.Length >= 2)
                {
                    try
                    {
                        varToSet = uint.Parse(Utils.ReturnValidHexValue(split[1]), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                    }
                }
                return true;
            }
            return false;

        }

        private static bool SetColorRGB(ref string line, string key, ref byte[] varToSet) 
        {
            if (line.StartsWith(key))
            {
                var split = line.Split(':');
                if (split.Length >= 2)
                {
                    try
                    {
                        string value = Utils.ReturnValidHexValue(split[1]);
                        value = value.PadRight(3 * 2, '0');

                        int cont = 0;
                        for (int ipros = 0; ipros < 3; ipros++)
                        {
                            string v = value[cont].ToString() + value[cont + 1].ToString();
                            varToSet[ipros] = byte.Parse(v, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            cont += 2;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return true;
            }
            return false;

        }

        private static bool SetLightSwitch(ref string line, string key, ref uint LightSwitch, uint mask) 
        {
            if (line.StartsWith(key))
            {
                var split = line.Split(':');
                if (split.Length >= 2)
                {
                    try
                    {
                        uint val = uint.Parse(Utils.ReturnValidDecValue(split[1]), NumberStyles.Integer, CultureInfo.InvariantCulture);
                        if (val != 0)
                        {
                            LightSwitch |= mask;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return true;
            }
            return false;

        }
    }
}
