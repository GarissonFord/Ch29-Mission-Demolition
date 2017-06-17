using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
	idle,
	playing,
	levelEnd
}

public class MissionDemolition : MonoBehaviour 
{
	static public MissionDemolition S;

	//Set in inspector
	public GameObject[] castles;
	public Text  levelText;
	public Text scoreText;
	public Vector3 castlePos;

	public bool _______________________;

	//Set dynamically
	public int level;
	public int levelMax;
	public int shotsTaken;
	public GameObject castle;
	public GameMode mode = GameMode.idle;
	public string showing = "Slingshot"; //FollowCam mode

}
