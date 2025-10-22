using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public Data Data;
    }

    [System.Serializable]
    public class Data
    {
        public int CharacterID;
        public int Coins;
        public bool ShowTutorial;
    }
}