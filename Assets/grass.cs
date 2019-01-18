using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class grass : MonoBehaviour {


	public float coinChance=1f;

	//deleteafter
	public GameObject coinFab;

	//1 is obstructed
	public int[] obstructed;


	public bool isGrass;

	public bool randomchance;

	[Range(0,1)]
	public float chance;

	public GameObject previousLane;
	public GameObject [] obstacles;

	public GameObject[]  obsts= new GameObject[9];








	void Start () {

		if (randomchance)
			chance = Random.value;


		//random Instantiation
		for (int i = -4; i <=4; i++) {
			if (Random.value < chance) {
				GameObject go =(GameObject)	Instantiate (obstacles [Random.Range (0, obstacles.Length)], new Vector3 (transform.position.x, transform.position.y, i), Quaternion.identity);
				go.transform.SetParent (transform);

				if(isGrass)
					obstructed [i + 4] = 1;
				else
					obstructed [i + 4] = 0;


				obsts [i+4] = go;
			}else
				obsts [i+4] = null;
			}

		//TODO IS THIS CONDITION REALLY NEEDED
		if (previousLane!=null)
			FixGrass2 ();
	


		//coinInstantiated
			for (int i = 0; i < obstructed.Length; i++) {

			if (obstructed [i] == 0)
				//CoinDropper (new Vector3 (transform.position.x, 0.1f, i-4), 1f,gameObject);
			
			if(GameManager.instance!=null)
				GameManager.instance.CoinDropper (new Vector3 (transform.position.x, 0.1f, i-4), coinChance,gameObject);
		
		}





		

		}


	public void CoinDropper(Vector3 position, float coinChance,GameObject parentGO)
	{

		if (Random.value < coinChance) {


		GameObject coin = (GameObject)
				Instantiate (GameManager.instance.coinFab, Vector3.zero, Quaternion.identity);

			coin.transform.SetParent (parentGO.transform);
		}
	}

	//MUST ENSURE PASSABILITY
	//MUST OCCUPIE SOME NULL SPACES WHERE YOU CANT GO
	void FixGrass2()
	{

		//TODO IS THIS LINE NEEDED
		if (previousLane.GetComponent<grass> () == null)
			return;

		//OPEN UP OR PUT DOWN LILYPADIN CASE THERES NO SOLUTION
		bool passable1=false;

		for (int i = 0; i < obstructed.Length; i++) {
			if (obstructed [i] == 0 && previousLane.GetComponent<grass> ().obstructed [i] == 0) {
				passable1 = true;
				break;
			}
		}


		int toDelete=0;


		//openPassageWay find in previous lane an open space
		if (passable1 == false) {

			do {
				toDelete = Random.Range (0, obsts.Length);
			} while (previousLane.GetComponent<grass> ().obstructed [toDelete] == 1);

			//delete it
			if (isGrass) {

				//Debug.Log ("Destroyed at:" + toDelete);
				Destroy (obsts [toDelete]);
				obsts [toDelete] = null;
				obstructed [toDelete] = 0;
			} else {

				//Debug.Log ("Generated at:" + toDelete);
				GameObject go =(GameObject)	Instantiate (obstacles [Random.Range (0, obstacles.Length)], new Vector3 (transform.position.x, transform.position.y, toDelete-4), Quaternion.identity);
				go.transform.SetParent (transform);
				obstructed [toDelete] = 0;

				obsts [toDelete] = go;

			}
			//Debug.Log ("Deleted: " + toDelete);
		}

		//OCCUPIE SPACES U CANT GO
		for (int i = 0; i < obstructed.Length; i++) {

			//Debug.Log ("Checking:" + i);

			//if has a tree behind it
			if (obstructed [i] == 0 && previousLane.GetComponent<grass> ().obstructed [i] == 1) {
				//should there be connected? 
				bool passable=false;

				//check left
				if (i != obstructed.Length - 1) {
					for (int j = i + 1; j < obstructed.Length; j++) {
						//no passages this way
						if (obstructed [j] == 1) {
							//Debug.Log ("TreeFound");
							break;
						}
						if (obstructed [j] == 0 && previousLane.GetComponent<grass> ().obstructed [j] == 0) {
							{
								passable = true;
								break;
							}
					
						}
					}
				}
				//check righy
				if (i > 0) {
					for (int j = i - 1; j >= 0; j--) {

						if(obstructed [j] == 1)
							break;

						if (obstructed [j] == 0 && previousLane.GetComponent<grass> ().obstructed [j] == 0) {
							{
								passable = true;
								break;
							}

						}
					}
				}
				if (passable == false) {
					//Debug.Log ("Obstructed in: " + i);
					obstructed [i] = 1;

				}
				

			}
		}

	

		}



	void FixGrass()
	{
		if (previousLane== null)
			return;


		//FindPassageWays
		bool passable=false;

		for (int i = 0; i < obsts.Length; i++) {
			if (obsts [i] == null && previousLane.GetComponent<grass> ().obsts [i] == null) {
				passable = true;
				break;
			}
		}

		int toDelete=0;
		//openPassageWay
		if (passable == false) {

			for (int i = 0; i < obsts.Length; i++) {
				if (previousLane.GetComponent<grass> ().obsts [i] == null) {

					toDelete = i;
					break;
				}
			}

			Destroy (obsts [toDelete]);
			obsts [toDelete] = null;


			//ADAPTS OBSTS WITH VIRTUAL TREES
			for (int i = toDelete; i < obsts.Length; i++) {
				if (obsts [i] != null) {
					for (int j = i; j < obsts.Length; j++) {
						obsts [j] = new GameObject();
					}
					break;
				}
			}

			for (int i = toDelete; i >=0; i--) {
				if (obsts [i] != null) {
					for (int j = i; j >=0; j--) {
						obsts [j] = gameObject;
					}
					break;
				}
			}
		}



	}

	void FixRiver()
	{

		bool passable=false;

		for (int i = 0; i < obsts.Length; i++) {
			if (obsts [i] != null && previousLane.GetComponent<grass> ().obsts [i] != null) {
				passable = true;
				break;
			}
		}

		int toCreate=0;
		//openPassageWay
		if (passable == false) {

			for (int i = 0; i < obsts.Length; i++) {
				if (previousLane.GetComponent<grass>()!=null &&previousLane.GetComponent<grass> ().obsts [i] != null) {

					toCreate = i;
					break;
				}
			}

			GameObject go =(GameObject)	Instantiate (obstacles [Random.Range (0, obstacles.Length)], new Vector3 (transform.position.x, transform.position.y, toCreate), Quaternion.identity);
			go.transform.SetParent (transform);

			obsts [toCreate] = go;
		}


	}
}
