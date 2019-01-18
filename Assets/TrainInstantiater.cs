using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class TrainInstantiater : MonoBehaviour {

	public GameObject[] trainSigns;

	public GameObject trainSign;

	public float intervalMin=1;
	public float intervalMax = 3;
	public float speed=5;

	public GameObject[] objects;

	bool rotated;




	public bool normalizeInstantiation;


	GameObject pickedObj;

	// Use this for initialization
	void Start () {

		if (Random.value > 0.5f) {
			transform.GetChild(0).position=Vector3.Scale(transform.GetChild(0).position , new Vector3 (1, 1, -1));
			rotated = true;

		}
		pickedObj = objects [Random.Range (0, objects.Length)];



		StartCoroutine (InstantiateStuff());
	}

	// Update is called once per frame
	IEnumerator InstantiateStuff () {
		float interval;
		while (true) {
			interval=Random.Range(intervalMin,intervalMax);

			if (normalizeInstantiation)
				interval = (Mathf.RoundToInt(interval * speed))*(1/ speed);

			yield return new WaitForSeconds (interval);



			if (trainSign != null)
				StartCoroutine (WarningLights ());
			else
				CreateObject ();


		}

	}



	IEnumerator WarningLights()
	{
		
		Debug.Log ("Happening?");

		Switch (trainSigns [1]);
		yield return new WaitForSeconds (0.2f);
		Switch (trainSigns [2]);
		yield return new WaitForSeconds (0.2f);
		Switch (trainSigns [1]);
		yield return new WaitForSeconds (0.2f);
		Switch (trainSigns [2]);
		yield return new WaitForSeconds (0.2f);


		Switch (trainSigns [0]);

		CreateObject ();


	}

	void Switch(GameObject sign)
	{
		GameObject aux;
		aux = trainSign;
		trainSign=(GameObject)Instantiate (sign, trainSign.transform.position, trainSign.transform.rotation);
		trainSign.transform.SetParent (transform);

		Destroy (aux);
	}


	void CreateObject()
	{
		GameObject	go = (GameObject)Instantiate (pickedObj, transform.GetChild (0).position, pickedObj.transform.rotation);
		go.GetComponent<goForwards> ().speed = speed;
		if (rotated) {
			go.transform.Rotate (0, 180, 0);
			go.GetComponent<goForwards> ().speed = -go.GetComponent<goForwards> ().speed;
		}
	}
}
