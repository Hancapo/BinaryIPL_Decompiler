using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;

namespace BinaryIPL_Decompiler
{

    class Program
    {

        public static List<IDE> ItemList { get; set; } = new List<IDE>();
        public static List<KeyValuePair<int, string>> IdeKeys { get; set; } = new List<KeyValuePair<int, string>>();

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            int ItemCount;
            int CarCount;
            string[] idefiles = Directory.GetFiles("ide/", "*.ide");
            string[] binaryiplfiles = Directory.GetFiles("bipl/", "*.ipl");
            StringBuilder sb = new StringBuilder();
            Directory.CreateDirectory("ipl");

            foreach (var ide in idefiles)
            {
                string[] idelines = File.ReadAllLines(ide);

                foreach (var ideline in idelines)
                {
                    if (ideline.StartsWith("tobj")) { continue; }
                    if (ideline.StartsWith("objs")) { continue; }
                    if (ideline.StartsWith("end"))  { continue; }
                    if (ideline.StartsWith("path")) { continue; }
                    if (ideline.StartsWith("2dfx")) { continue; }
                    if (ideline.StartsWith("anim")) { continue; }
                    if (ideline.StartsWith("txdp")) { continue; }
                    if (string.IsNullOrEmpty(ideline)){ continue; }
                    if (ideline.StartsWith("#")){ continue; }

                    IDE iden = new IDE
                    {
                        Id = (int)long.Parse(ideline.Split(',')[0].Trim()),
                        Modelname = ideline.Split(',')[1].Trim()
                    };

                    IdeKeys.Add(new KeyValuePair<int, string>(iden.Id, iden.Modelname));

                    


                }




            }

            foreach (var ipl in binaryiplfiles)
            {

                FileStream fs = File.OpenRead(ipl);
                Console.WriteLine("Processing " + Path.GetFileName(ipl) + "...");
                BinaryReader br = new BinaryReader(fs);
                List<IPL> _ListIPL = new List<IPL>();
                List<Cars> _ListCars = new List<Cars>();
                if (br.ReadInt32() == 2037542498)
                {
                    //Console.WriteLine("Valid BinaryIPL");
                    ItemCount =  br.ReadInt32(); //reading item instances count
                    br.ReadBytes(4 * 3); // CULL, GRGE and ENEX skip
                    CarCount = br.ReadInt32(); //Reading parked cars count
                    br.ReadBytes(4 * 13); //Skipping useless sectors

                    sb.AppendLine("# IPL generated with Vicho's BinaryIPL Decompiler");


                    for (int z = 0; z < ItemCount; z++)
                    {
                        IPL _ipl = new IPL(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                        _ListIPL.Add(_ipl);
                    }


                    if (_ListIPL.Count > 0)
                    {
                        sb.AppendLine("inst");
                        foreach (var entity in _ListIPL)
                        {
                            sb.AppendLine(entity.ObjectID + ", "
                                + GetValuefromKey(entity.ObjectID) + ", "
                                + entity.Interior + ", "
                                + entity.PosX + ", "
                                + entity.PosY + ", "
                                + entity.PosZ + ", "
                                + entity.RotX + ", "
                                + entity.RotY + ", "
                                + entity.RotZ + ", "
                                + entity.RotW + ", "
                                + entity.LodIndex);
                        }
                        sb.AppendLine("end");
                    }


                    if (CarCount > 0)
                    {
                        for (int k = 0; k < CarCount; k++)
                        {
                            Cars _cars = new Cars(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                            _ListCars.Add(_cars);
                        }
                    }

                    if (_ListCars.Count > 0)
                    {
                        sb.AppendLine("cars");
                        foreach (var car in _ListCars)
                        {
                            sb.AppendLine(car.PosX + ", "
                                + car.PosY + ", "
                                + car.PosZ + ", "
                                + car.Angle + ", "
                                + car.ObjectId + ", "
                                + car.PrimaryColor + ", "
                                + car.SecondaryColor + ", "
                                + car.ForceSpawn + ", "
                                + car.AlarmProb + ", "
                                + car.LockedProb + ", "
                                + car.UNK1 + ", "
                                + car.UNK2);
                        }
                        sb.AppendLine("end");
                    }

                    Console.WriteLine("Writing " + Path.GetFileName(ipl) + " decompiled IPL");
                    

                    File.WriteAllText("ipl/" + Path.GetFileName(ipl), sb.ToString().Replace("NaN", "0"));
                    sb.Clear();

                }
                else
                {
                    Console.WriteLine(Path.GetFileNameWithoutExtension(ipl) + " isn't a valid Binary IPL");
                }

            }
            StringBuilder sb2 = new StringBuilder();
            foreach (var item in IdeKeys)
            {
                sb2.AppendLine(item.Value);
            }

            File.WriteAllText("strings.txt", sb2.ToString());
            Console.ReadKey();

        }   

        public static string GetValuefromKey(int key)
        {
            foreach (var IdeKey in IdeKeys)
            {
                if (IdeKey.Key == key)
                {
                    return IdeKey.Value;
                }
            }

            return "null";
        }

    }

}
