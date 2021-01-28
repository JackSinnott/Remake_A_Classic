using UnityEngine;

public partial class APIBasicWebCall
{
    [System.Serializable]
    public class GameState
    {
        public int version;
        public System.DateTime timestamp;
        public string position;
        public Vector2 position_value;
    }

}
   
