using System.Collections.Generic;

using Mappy.Texture;
using Mappy.Texture.Animation;

namespace Mappy.Maps
{
    internal static class RightTileCreator
    {
        public static Tile GetRightTile(string textureName, Vector2D position,
            bool isHollow)
        {
            if (GeneralTextureManager.ContainsTexture(textureName))
                return new Tile(isHollow, textureName, position);
            else if (CollectionHelper<AnimationGroup>.DictionnaryContains(AnimationGroupManager.AnimationGroups, textureName))
                return new AnimatedTile(isHollow, textureName, new Vector2D(position.X, position.Y));

            throw new KeyNotFoundException("Symbol '" + textureName + "' is neither " +
                "a texture or an animation. source:RightTileCreator.GetRightTile()");
        }
    }
}
