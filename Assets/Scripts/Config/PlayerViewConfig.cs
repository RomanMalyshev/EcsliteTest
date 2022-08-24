using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PlayerViewConfig")]
    public class PlayerViewConfig : ScriptableObject
    {
        public GameObject PlayerPrefab;
    }
}

