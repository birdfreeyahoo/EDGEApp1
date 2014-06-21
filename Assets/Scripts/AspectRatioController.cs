using UnityEngine;
using System.Collections;

public class AspectRatioController : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        float aspect = camera.aspect;
        Debug.Log("Aspect: " + aspect);

        Debug.Log("Resizing background plane...");

        Transform lTransform = GetComponentInChildren<Transform>();
        Vector3 scale = new Vector3(lTransform.localScale.z * aspect, 1, lTransform.localScale.z);
        lTransform.localScale = scale;
	}
	
	
}
