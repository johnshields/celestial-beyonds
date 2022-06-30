// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledInteractive : PropertyAttribute
    {
        public string keyword;
        public int type;
        public int value;

        public StyledInteractive(int v)
        {
            type = 0;
            value = v;
        }

        public StyledInteractive(string k)
        {
            type = 1;
            keyword = k;
        }
    }
}