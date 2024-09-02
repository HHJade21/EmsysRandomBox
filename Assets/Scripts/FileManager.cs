using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    public static FileManager Instance
    {
        get
        {
            if (Instance == null)
            {
                
            }
            return Instance;
        }
    }
}

public class ImageFolder
{
    public string name;
    public Sprite image;
}