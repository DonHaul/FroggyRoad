using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomGeneration : MonoBehaviour {

	public static RandomGeneration instance;

	public GameObject[] LanePrefabs;

	public GameObject roadLines;

	public int curPosition=1;



	public List<GameObject> Lanes = new List<GameObject> ();

	public GameObject player;
	

	// Use this for initialization
	void Start () {
		instance = this;

		player = GameObject.FindGameObjectWithTag ("Player");


	}
	
	// Update is called once per frame
	void Update () {
	
		//creation
		while (player.transform.position.x + 10 > curPosition) {

			int pick = Random.Range (0, LanePrefabs.Length);

			GameObject lane=(GameObject)Instantiate (LanePrefabs[pick],transform.position+Vector3.right*curPosition,LanePrefabs[pick].transform.rotation);


			//grass / riverstale
			grass grassScript = lane.GetComponent<grass> ();

			if (grassScript != null) {
				grassScript.previousLane = Lanes[Lanes.Count-1];
			}


			//Road Line
			if (Lanes [Lanes.Count - 1].tag == "Road" && lane.tag=="Road") {
				Instantiate (roadLines, Lanes [Lanes.Count - 1].transform.position+Vector3.up*0.07f + Vector3.right * 0.5f, roadLines.transform.rotation);
			}



			/*if (lane.tag != "RiverMoving") {


				//GameManager.instance.CoinDropper (curPosition,1f,gameObject);
			}*/

			Lanes.Add (lane);

			curPosition++;

		}


		//deletion
		if(player.transform.position.x  > Lanes[0].transform.position.x+10) {

			Destroy (Lanes [0]);

			Lanes.RemoveAt (0);



		}
	}
}
