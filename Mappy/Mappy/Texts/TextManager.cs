using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Mappy.Fonts
{
    public static class TextManager
    {
        static Dictionary<string, Text> texts = new Dictionary<string, Text>();

        public static Dictionary<string, Text> Texts { get { return texts; } }

        public static void AddText(string name, string fontPath)
        {
            AddText(name, fontPath, 12);
        }

        public static void AddText(string name, string fontPath, uint size)
        {
            AddText(name, fontPath, size, Text.Styles.Regular);
        }

        public static void AddText(string name, string fontPath, uint size, Text.Styles style)
        {
            if (!texts.Keys.Contains(name))
            {
                Font font = new Font(fontPath);
                Text text = new Text("", font);
                text.CharacterSize = size;
                text.Style = style;

                texts.Add(name, text);
            }
            else throw new ArgumentException("Already used text name '" + name + "'. source:TextManager.AddText()");
        }

        public static Font GetFont(string name)
        {
            return texts[name].Font;
        }

        public static void SetText(string name, string text)
        {
            texts[name].DisplayedString = text;
        }

        public static void SetSize(string name, uint size)
        {
            texts[name].CharacterSize = size;
        }
    }
}
