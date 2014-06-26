using UnityEngine;
using System.Collections;

public class TextureStretcher : MonoBehaviour 
{
    void Start()
    {
        Rect pixelInset = guiTexture.pixelInset;
        pixelInset.width = Screen.height * (16.0f / 9.0f);
        pixelInset.height = Screen.height;
		pixelInset.x = -pixelInset.width/2;
		pixelInset.y = -Screen.height/2;

        guiTexture.pixelInset = pixelInset;
    }
}
