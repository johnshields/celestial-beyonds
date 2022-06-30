//  Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.Constants
{
    public static class CONSTANT
    {
        public static Texture2D LogoImage => Resources.Load("Boxophobic - Logo") as Texture2D;

        public static Texture2D BannerImageBegin => Resources.Load("Boxophobic - BannerBegin") as Texture2D;

        public static Texture2D BannerImageMiddle => Resources.Load("Boxophobic - BannerMiddle") as Texture2D;

        public static Texture2D BannerImageEnd => Resources.Load("Boxophobic - BannerEnd") as Texture2D;

        public static Texture2D CategoryImageBegin => Resources.Load("Boxophobic - CategoryBegin") as Texture2D;

        public static Texture2D CategoryImageMiddle => Resources.Load("Boxophobic - CategoryMiddle") as Texture2D;

        public static Texture2D CategoryImageEnd => Resources.Load("Boxophobic - CategoryEnd") as Texture2D;

        public static Texture2D IconEdit => Resources.Load("Boxophobic - IconEdit") as Texture2D;

        public static Texture2D IconHelp => Resources.Load("Boxophobic - IconHelp") as Texture2D;

        public static Color ColorDarkGray => new Color(0.27f, 0.27f, 0.27f);

        public static Color ColorLightGray => new Color(0.83f, 0.83f, 0.83f);

        public static GUIStyle TitleStyle
        {
            get
            {
                var guiStyle = new GUIStyle
                {
                    richText = true,
                    alignment = TextAnchor.MiddleCenter
                };

                return guiStyle;
            }
        }

        public static GUIStyle BoldTextStyle
        {
            get
            {
                var guiStyle = new GUIStyle();

                Color color;

                if (EditorGUIUtility.isProSkin)
                    color = new Color(0.87f, 0.87f, 0.87f);
                else
                    color = new Color(0.27f, 0.27f, 0.27f);

                guiStyle.normal.textColor = color;
                guiStyle.alignment = TextAnchor.MiddleCenter;
                guiStyle.fontStyle = FontStyle.Bold;

                return guiStyle;
            }
        }
    }
}