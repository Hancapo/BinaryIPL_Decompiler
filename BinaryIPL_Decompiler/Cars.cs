using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryIPL_Decompiler
{
    class Cars
    {
        public Cars(float posX, float posY, float posZ, float angle, int objectId, int primaryColor, int secondaryColor, int forceSpawn, int alarmProb, int lockedProb, int uNK1, int uNK2)
        {
            PosX = posX;
            PosY = posY;
            PosZ = posZ;
            Angle = angle;
            ObjectId = objectId;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            ForceSpawn = forceSpawn;
            AlarmProb = alarmProb;
            LockedProb = lockedProb;
            UNK1 = uNK1;
            UNK2 = uNK2;
        }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float Angle { get; set; }
        public int ObjectId { get; set; }
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
        public int ForceSpawn { get; set; }
        public int AlarmProb { get; set; }
        public int LockedProb { get; set; }
        public int UNK1 { get; set; }
        public int UNK2 { get; set; }
    }
}
