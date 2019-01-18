using UnityEngine;
using System.Collections;

public class coin : MonoBehaviour {

	public Transform pivot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (pivot != null)
			transform.position = pivot.position;
	}

	void OnTriggerEnter(Collider other)
	{
		


	
		if (other.tag == "Player") {
			
			GameManager.instance.coins++;

			PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins")+1);

			GameManager.instance.coinsText.text = PlayerPrefs.GetInt ("Coins").ToString();

			Destroy (this.gameObject);
		}
	}
}
