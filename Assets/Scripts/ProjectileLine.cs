using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour 
{
	//Singleton
	static public ProjectileLine S;

	//Set in inspector
	public float minDist = 0.1f;
	public bool __________________;
	//Set dynamically
	public LineRenderer line;
	private GameObject _poi;
	public List<Vector3> points;

	void Awake()
	{
		S = this;
		line = GetComponent <LineRenderer> ();
		//Disable until needed
		line.enabled = false;
		points = new List<Vector3> ();
	}

	//A property (a method portrayed as a field)
	public GameObject poi
	{
		get
		{
			return(_poi);
		}
		set
		{
			_poi = value;
			if (_poi != null) 
			{
				//When poi is given a new value, resets everything
				line.enabled = false;
				points = new List<Vector3> ();
				AddPoint();
			}
		}
	}

	//Clears the line
	public void Clear()
	{
		_poi = null;
		line.enabled = false;
		points = new List <Vector3> ();
	}

	public void AddPoint()
	{
		//Gets the poi's current position
		Vector3 pt = _poi.transform.position;
		//if the pt isn't far enough from the last point
		if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) 
		{
			return;
		}
		//If this is the launch point
		if (points.Count == 0) 
		{
			Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
			Vector3 launchPosDiff = pt - launchPos;
			//More line is added to assist aiming
			points.Add(pt + launchPosDiff);
			points.Add (pt);
			line.SetVertexCount (2);
			//Sets first two points
			line.SetPosition (0, points[0]);
			line.SetPosition (1, points [1]);
			line.enabled = true;
		}
		else 
		{
			points.Add (pt);
			line.SetVertexCount (points.Count);
			line.SetPosition (points.Count - 1, lastPoint);
			line.enabled = true;
		}
	}

	//The most recently added point
	public Vector3 lastPoint
	{
		get 
		{
			if (points == null) 
			{
				return(Vector3.zero);
			}
			return (points[points.Count - 1]);
		}
	}

	void FixedUpdate()
	{
		if (poi == null) 
		{
			if (FollowCam.S.poi != null) 
			{
				if (FollowCam.S.poi.CompareTag ("Projectile")) 
				{
					poi = FollowCam.S.poi;
				} 
				else 
				{
					return; //if we didn't find a poi
				}
			} 
			else 
			{
				return; //if we didn't find a poi
			}
		} 

		//If there is a poi, its location is added in each FixedUpdate
		AddPoint();
		if(poi.GetComponent<Rigidbody>().IsSleeping())
		{
			poi = null;
		}
	}
}
