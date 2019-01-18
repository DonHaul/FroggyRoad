using UnityEngine;
using System.Collections;

public class goForwards : MonoBehaviour {

	public float speed= 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, transform.position -Vector3.forward, speed * Time.deltaTime);

	}
}
