using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledTexturePreview : PropertyAttribute
    {
        public string displayName = "";

        public StyledTexturePreview()
        {
            displayName = "";
        }

        public StyledTexturePreview(string displayName)
        {
            this.displayName = displayName;
        }
    }
}