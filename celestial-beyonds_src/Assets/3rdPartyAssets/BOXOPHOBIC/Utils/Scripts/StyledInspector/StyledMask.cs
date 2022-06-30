using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledMask : PropertyAttribute
    {
        public int down;
        public string options = "";

        public int top;

        public StyledMask(string options)
        {
            this.options = options;

            top = 0;
            down = 0;
        }

        public StyledMask(string options, int top, int down)
        {
            this.options = options;

            this.top = top;
            this.down = down;
        }
    }
}