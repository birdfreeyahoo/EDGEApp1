using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour 
{
	public Vector3 velocity;

	void Start()
	{

	}

	// Update is called once per frame
	void Update () 
	{
		transform.Translate(velocity * Time.deltaTime);
	}
}
