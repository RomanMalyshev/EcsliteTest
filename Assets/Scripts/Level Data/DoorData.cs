using System;
using UnityEngine;

namespace Level_Data
{
    [Serializable]
    public class DoorData
    {
        public Vector3 Position;
        public float MoveSpeed;
        public Vector3 OpenOffsetPosition;
    }
}