using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class Instantiater : MonoBehaviour {

	[System.Serializable]
	public struct TrainStuff{

		public GameObject[] trainSignsprefabs;
		public GameObject trainSign;
	}

	public TrainStuff trainStuff;


	public bool isRiver;

	public float intervalMin=1;
	public float intervalMax = 3;

	public float speedMax = 5;
	public float speedMin = 2;

	public float coinChance=1f;


	float speed=5;

	public bool differentObjects;


	public GameObject[] objects;

	bool rotated;


	public bool normalizeInstantiation;


	GameObject pickedObj;

	// Use this for initialization
	void Start () {


		//coinInstantiated
		for (int i = 0; i < 10; i++) {

			float height = 0.05f;
			if (trainStuff.trainSign != null)
				height += .15f;

			if(GameManager.instance!=null && isRiver==false)
				GameManager.instance.CoinDropper (new Vector3 (transform.position.x, height, i-4), coinChance,gameObject);

		}
		if (Random.value > 0.5f) {
			transform.GetChild(0).position=Vector3.Scale(transform.GetChild(0).position , new Vector3 (1, 1, -1));
			rotated = true;
		
		}
		pickedObj = objects [Random.Range (0, objects.Length)];


		speed = Random.Range (speedMin, speedMax);

		StartCoroutine (InstantiateStuff());
	}

	// Update is called once per frame
	IEnumerator InstantiateStuff () {
		float interval;
		while (true) {
			interval=Random.Range(intervalMin,intervalMax);
			if (normalizeInstantiation)
				interval = (Mathf.RoundToInt(interval * speed))*(1/ speed);
		
			if(differentObjects)
				pickedObj = objects [Random.Range (0, objects.Length)];

	
			if (trainStuff.trainSign != null)
				StartCoroutine (WarningLights ());
			else
				CreateObject ();
		

			yield return new WaitForSeconds (interval);


		}

	}


	void CreateObject()
	{
		GameObject	go = (GameObject)Instantiate (pickedObj, transform.GetChild (0).position, pickedObj.transform.rotation);


		//CREATE COINS
		if (isRiver) {
			foreach (Transform item in go.transform) {
//				Debug.Log ("Coin Created");
			
				GameObject coin = GameManager.instance.CoinDropper (Vector3.down * 10, coinChance, item.gameObject);
				if(coin!=null)
					coin.GetComponent<coin> ().pivot = item;
			}
		}


		go.GetComponent<goForwards> ().speed = speed;
		if (rotated) {
			go.transform.Rotate (0, 180, 0);
			go.GetComponent<goForwards> ().speed = -go.GetComponent<goForwards> ().speed;
		}
	}



	IEnumerator WarningLights()
	{
		

	

		Switch (trainStuff.trainSignsprefabs [1]);
		yield return new WaitForSeconds (0.2f);
		Switch (trainStuff.trainSignsprefabs [2]);
		yield return new WaitForSeconds (0.2f);
		Switch (trainStuff.trainSignsprefabs [1]);
		yield return new WaitForSeconds (0.2f);
		Switch (trainStuff.trainSignsprefabs [2]);
		yield return new WaitForSeconds (0.2f);


		Switch (trainStuff.trainSignsprefabs [0]);

		CreateObject ();


	}

	void Switch(GameObject sign)
	{
		GameObject aux;
		aux = trainStuff.trainSign;
		trainStuff.trainSign=(GameObject)Instantiate (sign, trainStuff.trainSign.transform.position, trainStuff.trainSign.transform.rotation);
		trainStuff.trainSign.transform.SetParent (transform);

		Destroy (aux);
	}

}
