using UnityEngine;

namespace Config
{  
    [CreateAssetMenu(fileName = "PlayerConfig")]
    public class PlayerConfig: ScriptableObject
    {
        public float Speed;
    }
}