using System;

namespace Mappy.Texture
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
            ExceptionHelper<int>.AssertFileExists(textureFile, "GeneralTextureManager.LoadTextures()");

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
            type = TextureType.SFML; //if in collections, not dummy, if not dummy
                                    //to respect 'out'

            if (CollectionHelper<SFML.Graphics.Texture>.DictionnaryContains(TextureManager.Textures, name))
                return true;
            else if (CollectionHelper<FramedTexture>.DictionnaryContains(FramedTextureManager.Textures, name))
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
