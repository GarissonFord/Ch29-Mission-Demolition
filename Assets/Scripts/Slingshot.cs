using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

	static public Slingshot S;

	//Set in the Inspector
	public GameObject prefabProjectile;
	//So...this is literally just meant to just divide the Inspector and dynamic fields?
	public float velocityMult = 4f;
	public bool ________________;

	//Set dynamically
	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;

	void Awake()
	{
		S = this;
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	}

	void Update()
	{
		if (!aimingMode)
			return;

		//Gets the 2D coordinates of the current mouse position
		Vector3 mousePos2D = Input.mousePosition;
		//Uses the camera's negative zed coordinate to make it appear in the camera view
		mousePos2D.z = -Camera.main.transform.position.z;
		//Gives 3D world coordinates to the current mouse position
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);

		/* Creates the Vector connecting the projectile
		 * with the cursor even when it leaves the launchPos
		 */
		Vector3 mouseDelta = mousePos3D - launchPos;
		/* The furthest distance from the launchPos' origin that 
		 * the projectile can travel while being aimed 
		 */
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		//If the mouseDelta is greater than the Collider's radius...
		if (mouseDelta.magnitude > maxMagnitude) 
		{
			//Set mouseDelta's length to 1 while pointing in the same direction
			mouseDelta.Normalize ();
			//Multiply mouseDelta by the furthest distance, this creates the "clamp"
			mouseDelta *= maxMagnitude;
		}

		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		//If mouse button is released
		if (Input.GetMouseButtonUp (0)) 
		{
			aimingMode = false;
			projectile.GetComponent<Rigidbody> ().isKinematic = false;
			//Take the reverse magnitude of mouseDelta and multiply it by the velocity
			projectile.GetComponent<Rigidbody> ().velocity = -mouseDelta * velocityMult;
			//Sets the projectile to be followed by the camera
			FollowCam.S.poi = projectile;
			/* Doesn't eliminate the actual game object, just allows another instance 
			 * to occupy the variable so we can make multiple shots 
			 */
			projectile = null;
		}
	}

	void OnMouseEnter()
	{
		print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);
	}

	void OnMouseExit()
	{
		print ("Slingshot:OnMouseExit()");
		launchPoint.SetActive (false);
	}

	void OnMouseDown()
	{
		aimingMode = true;
		//Instantiate projectile
		projectile = Instantiate(prefabProjectile) as GameObject;
		//Set it at launchPoint
		projectile.transform.position = launchPos;
		projectile.GetComponent<Rigidbody>().isKinematic = true;
	}
}
