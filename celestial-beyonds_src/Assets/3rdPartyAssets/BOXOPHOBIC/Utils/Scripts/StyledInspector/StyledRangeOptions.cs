// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledRangeOptions : PropertyAttribute
    {
        public string displayLabel;
        public float max;
        public float min;
        public string[] options;

        public StyledRangeOptions(float min, float max, string displayLabel, string[] options)
        {
            this.min = min;
            this.max = max;
            this.displayLabel = displayLabel;

            this.options = options;
        }
    }
}