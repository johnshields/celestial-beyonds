// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledButton : PropertyAttribute
    {
        public float Down;
        public string Text = "";
        public float Top;

        public StyledButton(string Text)
        {
            this.Text = Text;
            Top = 0;
            Down = 0;
        }

        public StyledButton(string Text, float Top, float Down)
        {
            this.Text = Text;
            this.Top = Top;
            this.Down = Down;
        }
    }
}