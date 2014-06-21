using UnityEngine;
using System.Collections;

public class CameraTouchTranslation : MonoBehaviour {

	public float speed = 1.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	    if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (GameOptions.invertYAxis)
                touchDeltaPosition.y = -touchDeltaPosition.y;

			transform.Translate(touchDeltaPosition.x * speed * Time.deltaTime,touchDeltaPosition.y * speed * Time.deltaTime,0);
		}

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
	}
}
