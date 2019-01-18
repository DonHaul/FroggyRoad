using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	public float interval =10f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, interval);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
