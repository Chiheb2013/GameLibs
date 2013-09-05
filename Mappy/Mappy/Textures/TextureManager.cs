using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Mappy.Textures
{
    public static class TextureManager
    {
        static Vector2D textureSize = new Vector2D(16, 16);
        static Dictionary<string, SFML.Graphics.Texture> textures = new Dictionary<string, SFML.Graphics.Texture>();

        public static Vector2D TextureSize { get { return textureSize; } set { textureSize = value; } }
        public static Dictionary<string, SFML.Graphics.Texture> Textures { get { return textures; } }

        public static void LoadTextures(string texturesPath)
        {
            string[] lines = StringHelper.GetCleanLines(texturesPath);
            GetTextures(lines);
        }

        public static void AddTexture(string name, string imagePath)
        {
            ExceptionHelper.AssertFileExists(imagePath, "TextureManager.AddTexture()");
            ExceptionHelper.AssertIsNotInDictionary<SFML.Graphics.Texture>(textures, name, "TextureManager.AddTexture()");
            
            SFML.Graphics.Texture texture = new SFML.Graphics.Texture(imagePath);
            textures.Add(name, texture);
        }

        public static SFML.Graphics.Texture GetTexture(string name)
        {
            ExceptionHelper.AssertIsInDictionary<SFML.Graphics.Texture>(textures, name, "TextureManager.GetTexture()");
            return textures[name];
        }

        private static void GetTextures(string[] lines)
        {
            foreach (string line in lines)
                if (line.StartsWith("texture:"))
                    LoadTexture(line);
        }

        private static void LoadTexture(string line)
        {
            string[] parts = line.Replace("texture:", "").Split(',');
            AddTexture(parts[0], parts[1]);
        }
    }
}
