using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSize : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<UITexture>().SetScreenRect(0, 0, Screen.width, Screen.height);
    }

    private void Update()
    {
        this.GetComponent<UITexture>().SetScreenRect(0, 0, Screen.width, Screen.height);
    }
}
