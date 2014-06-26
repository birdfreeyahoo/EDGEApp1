using UnityEngine;
using System.Collections;

public class TextureStretcher : MonoBehaviour 
{
    void Start()
    {
        Rect pixelInset = guiTexture.pixelInset;
        pixelInset.width = Screen.width;
        pixelInset.height = Screen.height;
		pixelInset.x = -Screen.width/2;
		pixelInset.y = -Screen.height/2;

        guiTexture.pixelInset = pixelInset;
    }
}
