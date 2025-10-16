using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGController : Singleton<BGController>
{
    public Sprite[] Img;

    public SpriteRenderer ImgRenderer;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        ChangeBackground();
    }

    public void ChangeBackground()
    {
        if(ImgRenderer != null && Img != null && Img.Length > 0)
        {
            int RandomIndex = Random.Range(0, Img.Length);

            if (Img[RandomIndex] != null)
            {
                ImgRenderer.sprite = Img[RandomIndex];
            }
        }
    }
}
