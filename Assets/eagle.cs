using UnityEngine;
using System.Collections;

public class eagle : MonoBehaviour {



	int currentFurthest;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0,0,1);

		if (transform.position.x < GameManager.instance.player.transform.position.x)
			GameManager.instance.player.transform.position = transform.position;

	}
}
