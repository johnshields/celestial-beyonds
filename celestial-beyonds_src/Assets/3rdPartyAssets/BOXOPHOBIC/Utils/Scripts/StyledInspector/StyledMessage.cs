// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledMessage : PropertyAttribute
    {
        public float Down;
        public string Message;
        public float Top;
        public string Type;

        public StyledMessage(string Type, string Message)
        {
            this.Type = Type;
            this.Message = Message;
            Top = 0;
            Down = 0;
        }

        public StyledMessage(string Type, string Message, float Top, float Down)
        {
            this.Type = Type;
            this.Message = Message;
            this.Top = Top;
            this.Down = Down;
        }
    }
}