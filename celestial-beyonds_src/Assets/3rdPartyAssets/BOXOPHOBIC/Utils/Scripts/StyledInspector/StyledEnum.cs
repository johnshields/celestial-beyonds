using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledEnum : PropertyAttribute
    {
        public int down;
        public string options = "";

        public int top;

        public StyledEnum(string options)
        {
            this.options = options;

            top = 0;
            down = 0;
        }

        public StyledEnum(string options, int top, int down)
        {
            this.options = options;

            this.top = top;
            this.down = down;
        }
    }
}