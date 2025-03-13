using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureArrayImporter
{
    public static Texture2DArray CreateTexture2DArray(Texture2D[] spritesheets)
    {
        if (spritesheets.Length == 0)
        {
            Debug.LogError("Aucun spritesheet fourni.");
            return null;
        }

        int width = spritesheets[0].width;
        int height = spritesheets[0].height;
        TextureFormat format = spritesheets[0].format;

        Texture2DArray textureArray = new Texture2DArray(width, height, spritesheets.Length, format, true);
        textureArray.filterMode = FilterMode.Point;

        for (int i = 0; i < spritesheets.Length; i++)
        {
            if (spritesheets[i].width != width || spritesheets[i].height != height)
            {
                Debug.LogError($"Le spritesheet {spritesheets[i].name} a une taille différente.");
                return null;
            }

            Graphics.CopyTexture(spritesheets[i], 0, 0, textureArray, i, 0);
        }

        textureArray.Apply();
        return textureArray;
    }
}