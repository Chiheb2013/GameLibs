using System;

namespace Mappy.Textures
{
    public static class GeneralTextureManager
    {
        public static SFML.Graphics.Texture GetTexture(string name)
        {
            TextureType type = TextureType.SFML;

            if (ContainsTexture(name, out type))
                return GetRightTexture(name, type);

            throw new ArgumentException("Texture '" + name + "' is not loaded. " +
                                        "source:GTM.GetTexture()");
        }

        public static void LoadTextures(string textureFile)
        {
            ExceptionHelper.AssertFileExists(textureFile, "GeneralTextureManager.LoadTextures()");

            TextureManager.LoadTextures(textureFile);
            FramedTextureManager.LoadTextures(textureFile);
        }

        public static bool ContainsTexture(string name)
        {
            TextureType dummy = TextureType.SFML;
            return ContainsTexture(name, out dummy);
        }

        public static bool ContainsTexture(string name, out TextureType type)
        {
            type = TextureType.SFML; //if in collections, type's not dummy
                                    //if not in collection : type's dummy to respect 'out'

            if (CollectionHelper.DictionaryContains<SFML.Graphics.Texture>(TextureManager.Textures, name))
                return true;
            else if (CollectionHelper.DictionaryContains<FramedTexture>(FramedTextureManager.Textures, name))
            {
                type = TextureType.Framed;
                return true;
            }
            return false;
        }

        private static SFML.Graphics.Texture GetRightTexture(string name, TextureType type)
        {
            if (type == TextureType.SFML)
                return TextureManager.GetTexture(name);
            else
                return FramedTextureManager.GetTexture(name);
        }
    }
}
