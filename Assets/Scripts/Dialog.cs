using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text titleText;
    public Text contentText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialog(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    public void UpdateDialog(string title, string content)
    {
        if (titleText)
        {
            titleText.text = title;
        }

        if (contentText)
        {
            contentText.text = content;
        }
    }
}
