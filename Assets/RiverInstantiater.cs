using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class RiverInstantiater : MonoBehaviour {

	public float intervalMin=1;
	public float intervalMax = 3;

	public float speedMax = 5;
	public float speedMin = 2;

	public float speed=5;




	public GameObject[] objects;

	bool rotated;

	//NO NEED FOR THIS SHIT
	public bool normalizeInstantiation;


	GameObject pickedObj;

	// Use this for initialization
	void Start () {

		if (Random.value > 0.5f) {
			transform.GetChild(0).position=Vector3.Scale(transform.GetChild(0).position , new Vector3 (1, 1, -1));
			rotated = true;

		}


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

			pickedObj = objects [Random.Range (0, objects.Length)];


			/*Quaternion rotation;
			if (rotated) {
				rotation = Quaternion.Euler (0, 180, 0);
			}else 
				rotation=Quaternion.identity;
*/
			GameObject	go = (GameObject)Instantiate (pickedObj, transform.GetChild (0).position, pickedObj.transform.rotation);
			go.GetComponent<goForwards> ().speed = speed;
			if (rotated) {
				go.transform.Rotate (0, 180, 0);
				go.GetComponent<goForwards> ().speed = -go.GetComponent<goForwards> ().speed;
			}

			//go.GetComponent<Rigidbody> ().isKinematic = true;

			yield return new WaitForSeconds (interval);
		}

	}




}
