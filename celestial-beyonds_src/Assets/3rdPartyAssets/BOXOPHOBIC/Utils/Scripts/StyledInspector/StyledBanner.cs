using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledBanner : PropertyAttribute
    {
        public float colorB;
        public float colorG;
        public float colorR;
        public string helpURL;
        public string title;

        public StyledBanner(string title)
        {
            colorR = -1;
            this.title = title;
            helpURL = "";
        }

        public StyledBanner(string title, string helpURL)
        {
            colorR = -1;
            this.title = title;
            this.helpURL = helpURL;
        }

        public StyledBanner(float colorR, float colorG, float colorB, string title, string helpURL)
        {
            this.colorR = colorR;
            this.colorG = colorG;
            this.colorB = colorB;
            this.title = title;
            this.helpURL = helpURL;
        }

        // Legacy
        public StyledBanner(string title, string subtitle, string helpURL)
        {
            colorR = -1;
            this.title = title;
            this.helpURL = helpURL;
        }

        public StyledBanner(float colorR, float colorG, float colorB, string title, string subtitle, string helpURL)
        {
            this.colorR = colorR;
            this.colorG = colorG;
            this.colorB = colorB;
            this.title = title;
            this.helpURL = helpURL;
        }
    }
}