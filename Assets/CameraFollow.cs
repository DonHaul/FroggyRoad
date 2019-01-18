using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public static CameraFollow instance;

	Vector3 offset;

	public GameObject player;

	float maxPos;
	// Use this for initialization
	void Start () {
		instance = this;
		player = GameObject.FindGameObjectWithTag ("Player");

		offset = -player.transform.position+transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if(player!=null)
		if (player.transform.position.x > maxPos)
			maxPos = player.transform.position.x;

		transform.position =offset+ Vector3.right * maxPos;
	}
}
