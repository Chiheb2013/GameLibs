using System;
using System.IO;
using System.Collections.Generic;

using Mappy.Textures;

namespace Mappy
{
    internal static class ExceptionHelper
    {
        public static void AssertIsNotInDictionary<T>(IDictionary<string,T> dict, 
            string name, string source)
        {
            if (CollectionHelper.DictionaryContains<T>(dict, name))
                ThrowExistanceException(name, source);
        }

        public static void AssertIsInDictionary<T>(IDictionary<string, T> dict,
            string name, string source)
        {
            if (!CollectionHelper.DictionaryContains<T>(dict, name))
                ThrowExistanceException(name, source, "doesn't exist.");
        }

        public static void AssertIsInDictionary<T1, T2>(IDictionary<T1, T2> dict, T1 element, string source)
        {
            if (!CollectionHelper.DictionaryContains<T1, T2>(dict, element))
                ThrowExistanceException("Element '" + typeof(T1).ToString(), source, "' doesn't exist.");
        }

        public static void AssertIsNotInDictionary<T1, T2>(IDictionary<T1, T2> dict, T1 element, string source)
        {
            if (!CollectionHelper.DictionaryContains<T1, T2>(dict, element))
                ThrowExistanceException("Element '" + typeof(T1).ToString(), source, "' already exists");
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

        public static T AssertIsTAndReturnCasted<T>(object second, string source)
        {
            if (second is T)
                return (T)second;
            throw new InvalidCastException("Object is not of type '" + typeof(T).ToString() + "'. " +
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
