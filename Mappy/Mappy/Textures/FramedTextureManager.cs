﻿using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Mappy.Textures
{
    public static class FramedTextureManager
    {
        const int NAME = 0;
        const int TILESET = 1;
        const int X = 2;
        const int Y = 3;
        const int WIDTH = 4;
        const int HEIGHT = 5;

        static Dictionary<string, FramedTexture> textures = new Dictionary<string, FramedTexture>();

        public static Dictionary<string, FramedTexture> Textures { get { return textures; } }

        public static void AddTexture(string name, string tileset, Frame frame)
        {
            ExceptionHelper.AssertIsNotInDictionary<FramedTexture>(textures, name, "FramedTextureManager.AddTexture()");
            textures.Add(name, new FramedTexture(tileset, frame));
        }

        public static FramedTexture GetTexture(string name)
        {
            ExceptionHelper.AssertIsInDictionary<FramedTexture>(textures, name, "FramedTextureManager.GetTexture()");
            return textures[name];
        }

        public static void LoadTextures(string textureFile)
        {
            string[] lines = StringHelper.GetCleanLines(textureFile);
            GetTextures(lines);
        }

        private static void GetTextures(string[] lines)
        {
            foreach (string line in lines)
                if (line.StartsWith("ftexture:"))
                    LoadTexture(line);
        }

        private static void LoadTexture(string line)
        {
            string source = "FTM.LoadTexture()";
            string[] parts = line.Replace("ftexture:", "").Split(',');

            int x = ExceptionHelper.AssertIsInteger(parts[X], source);
            int y = ExceptionHelper.AssertIsInteger(parts[Y], source);
            int width = ExceptionHelper.AssertIsInteger(parts[HEIGHT], source);
            int height = ExceptionHelper.AssertIsInteger(parts[WIDTH], source);
            string name = parts[NAME];
            string tileset = parts[TILESET];

            Frame frame = new Frame(-1, -1, new SFML.Graphics.IntRect(x, y, width, height));

            AddTexture(name, tileset, frame);
        }
    }
}
