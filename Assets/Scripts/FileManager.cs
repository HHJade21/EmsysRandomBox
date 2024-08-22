using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    private Dictionary<string, ImageFolder> imageFolders = new Dictionary<string, ImageFolder>();
    //이미지를 저장할 딕셔너리
    private Dictionary<string, Sprite> images = new Dictionary<string, Sprite>();
    private Sprite errorImg;

    private void Start()
    {
        errorImg = Resources.Load<Sprite>("Error/Image-Not-Found");
    }

    public Sprite GetImage(string name)
    {
        if (!images.ContainsKey(name))
        {
            Debug.LogError("Image not found: " + name);
            return errorImg;
        }
        return images[name];
    }

}

public class ImageFolder
{
    public string name;
    public Sprite image;
}