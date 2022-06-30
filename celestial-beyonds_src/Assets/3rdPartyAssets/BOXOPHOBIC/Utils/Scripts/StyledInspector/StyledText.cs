// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledText : PropertyAttribute
    {
        public TextAnchor alignment = TextAnchor.MiddleCenter;
        public bool disabled;
        public float down;
        public string text = "";
        public float top;

        public StyledText()
        {
        }

        public StyledText(TextAnchor alignment, bool disabled)
        {
            this.alignment = alignment;
            this.disabled = disabled;
        }

        public StyledText(TextAnchor alignment, bool disabled, float top, float down)
        {
            this.alignment = alignment;
            this.disabled = disabled;
            this.top = top;
            this.down = down;
        }
    }
}