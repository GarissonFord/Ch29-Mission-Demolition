using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour 
{
	//This is a "Singleton," a single intended copy of the object 
	static public FollowCam S;
	public float easing = 0.05f;
	public Vector2 minXY;

	//fields set in Inspector
	public bool ____________________________;

	//set dynamically
	public GameObject poi; //Point of Interest
	public float camZ; //Camera's z position

	void Awake()
	{
		S = this;
		camZ = this.transform.position.z;
	}

	void FixedUpdate()
	{
		Vector3 destination;
		if (poi == null) 
		{
			destination = Vector3.zero;
		} 
		else 
		{
			//Gets poi's position
			destination = poi.transform.position;
			if (poi.CompareTag ("Projectile")) 
			{
				//if it is sleeping
				if (poi.GetComponent<Rigidbody> ().IsSleeping ()) 
				{
					//Set poi to null and return camera to its origin
					poi = null;
					return;
				}
			}
		}
		//Prevents the camera from sinking below the ground
		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);
		/* transitions between a view of the main camera to that of the poi with
         * easing representing the rate at which it moves
		 */ 

		destination = Vector3.Lerp(transform.position, destination, easing);
		destination.z = camZ;
		transform.position = destination;
		//Always keeps ground in view by changing orthographic size
		this.GetComponent<Camera>().orthographicSize = destination.y + 10;
	}
}
