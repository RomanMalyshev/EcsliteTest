using Level_Data;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ButtonsConfig")]
    public class ButtonsConfig:ScriptableObject
    {
        public ButtonData[] Buttons;
    }
}