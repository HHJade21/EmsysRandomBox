using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class Banner : MonoBehaviour
{
    [Header("Banner Settings")]
    [SerializeField] private float bannertimer = 1.0f;

    [Header("Banner Images")]
    [SerializeField]
    private List<Sprite> images = new List<Sprite>();

    private int index = 0;
    private Image image;
    private void Start()
    {
        InvokeRepeating("BannerTimer", 0, bannertimer);
    }

    private void BannerTimer()
    {
        if (images.Count == 0) return;
        index++;
        if (index >= images.Count)
        {
            index = 0;
        }
    }

    private void Update()
    {
        this.image.sprite = images[index];
    }
}