using Level_Data;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "DoorConfig")]
    public class DoorsConfig : ScriptableObject
    {
        public DoorData[] Doors;
    }
}