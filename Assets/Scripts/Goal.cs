using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	static public bool goalMet = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Projectile")) 
		{
			Goal.goalMet = true;
			//Sets the color to a higher opcaity
			Color c = GetComponent<Renderer>().material.color;
			c.a = 1;
			GetComponent<Renderer>().material.color = c;
		}
	}
}
