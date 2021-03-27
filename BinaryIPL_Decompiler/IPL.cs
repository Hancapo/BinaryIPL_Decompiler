using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryIPL_Decompiler
{
    class IPL
    {
        public IPL(float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float rotW, int objectID, int interior, int lodIndex)
        {
            PosX = posX;
            PosY = posY;
            PosZ = posZ;
            RotX = rotX;
            RotY = rotY;
            RotZ = rotZ;
            RotW = rotW;
            ObjectID = objectID;
            Interior = interior;
            LodIndex = lodIndex;
        }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }
        public float RotW { get; set; }
        public int ObjectID { get; set; }
        public int Interior { get; set; }
        public int LodIndex { get; set; }



        

}
}
