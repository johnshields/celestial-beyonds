using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledCategory : PropertyAttribute
    {
        public string category;
        public int down;
        public int top;

        public StyledCategory(string category)
        {
            this.category = category;
            top = 10;
            down = 10;
        }

        public StyledCategory(string category, int spaceTop, int spaceBottom)
        {
            this.category = category;
            top = spaceTop;
            down = spaceBottom;
        }
    }
}