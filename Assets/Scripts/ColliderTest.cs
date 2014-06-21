using UnityEngine;

public class ColliderTest : MonoBehaviour 
{
	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
	}
}
