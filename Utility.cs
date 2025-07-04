using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utility : MonoBehaviour
{ 

    public static string API_KEY = "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";

     public static readonly string resolution_256 = "256x256";
    public static readonly string resolution_512 = "512x512";
    public static readonly string resolution_1024 = "1024x1024";
    public static readonly string maskTextureName = "_mergedTex.png";

    public static Texture2D CreateBigTranmsparentTexture(int height = 512 , int width = 512)
    {
        Texture2D newTexture = new Texture2D(height, width);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                newTexture.SetPixel(i, j, Color.clear);
            }
        }
        newTexture.Apply();
        return newTexture;
    }

    public static Texture2D CreateMaskImage(Texture2D bigTexture, Texture2D otherTexture)
    {
        for (int i = 0; i < otherTexture.height; i++)
        {
            for (int j = 0; j < otherTexture.width; j++)
            {
                bigTexture.SetPixel((bigTexture.width / 4 + i), (bigTexture.height / 4 + j), otherTexture.GetPixel(i, j));
            }
        }
        bigTexture.Apply();
        return bigTexture;
    }

    

	public static string WriteImageOnDisk(Texture2D texture, string fileName)
	{
		byte[] textureBytes = texture.EncodeToPNG();
		string path = GetBasePath() + fileName;
		File.WriteAllBytes(path, textureBytes);
		Debug.Log("File Written On Disk! " + path);
		return path;
	}

    public static Texture2D GetTextureFromFileName(string fileName)
    {
        string path = GetBasePath() + "/" + fileName;
        return GetTextureFromPath(path);
    }

    public static Texture2D GetTextureFromPath(string filePath)
    {
        var rawData = System.IO.File.ReadAllBytes(filePath);
        Texture2D tex = new Texture2D(2, 2); // Create an empty Texture; size doesn't matter (she said)
        tex.LoadImage(rawData);
        return tex;
    }

    public static Texture2D Resize(Texture2D texture, int width, int height)
    {
        return TextureScale.Bilinear(texture, width, height);
    }

    public static string GetImageName(int i)
    {
        return Time.time +  "_item_" + i + "_.png";
    }

    public static string GetBasePath()
    {
        return Application.dataPath + "/OpenAI Generated Assets/";// "Assets/OpenAiImages/";
    }

    
}
