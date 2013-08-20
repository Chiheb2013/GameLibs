﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mappy.Texture;

namespace Mappy
{
    internal static class ExceptionHelper<T>
    {
        public static void AssertIsNotInDictionnary(IDictionary<string,T> dict, 
            string name, string source)
        {
            if (CollectionHelper<T>.DictionnaryContains(dict, name))
                ThrowExistanceException(name, source);
        }

        public static void AssertIsInDictionnary(IDictionary<string, T> dict,
            string name, string source)
        {
            if (!CollectionHelper<T>.DictionnaryContains(dict, name))
                ThrowExistanceException(name, source, "doesn't exist.");
        }

        public static int AssertIsInteger(string str, string source)
        {
            int i = 0;

            if (!int.TryParse(str, out i))
                throw new FormatException("'" + str + "' is not an integer. " +
                    "source:" + source);

            return i;
        }

        public static void AssertTextureExists(string name, string source)
        {
            if (!GeneralTextureManager.ContainsTexture(name))
                ThrowExistanceException(name, source, "is not loaded");
        }

        public static void AssertFileExists(string path, string source)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File '" + path + "' was not found. " +
                    "source:" + source);
        }

        private static void ThrowExistanceException(string name, string source,
            string existance = "already exists")
        {
            throw new ArgumentException("Symbol '" + name + "' " + existance + " " +
                "source: " + source);
        }
    }
}
