using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public bool dead;

	public int currentFurthest;
	public float maxStopTime=6f;
	public float counter;


	public Text scoreText;
	public Text bestText;
	public Text coinsText;
	public Text restartText;

	public enum LaneType
	{
		Grass, RiverStale, TrainTracks, Road, RiverMoving
	};

	public int coins=0;

	public static GameManager instance;

	public GameObject coinFab;

	public GameObject bestLine;

	public GameObject player;


	public GameObject eagle;



	// Use this for initialization

	void Awake() {
		
	}

	void Start() {

		instance = this;

		player = GameObject.FindGameObjectWithTag ("Player");

		bestLine.transform.position+=Vector3.right*(PlayerPrefs.GetInt ("Best")+0.5f);

		bestText.text = PlayerPrefs.GetInt ("Best").ToString ();

		coinsText.text = PlayerPrefs.GetInt ("Coins").ToString ();
		currentFurthest = RandomGeneration.instance.curPosition;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.R)) {

			End ();
		
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		if (player.transform.position.y < -1)
			Death ();


		if (counter > maxStopTime) {
				
			Instantiate (eagle, player.transform.position + Vector3.right * 20,eagle.transform.rotation);
				Death();
		}
		if(currentFurthest!=RandomGeneration.instance.curPosition )
		{counter = 0;

				currentFurthest = RandomGeneration.instance.curPosition;
		}
		if(dead==false &&  player.transform.position.x>.1f)
		counter += Time.deltaTime;


		//score text
		scoreText.text=Mathf.RoundToInt(player.transform.position.x).ToString();


		//best stuff

		if (player.transform.position.x > PlayerPrefs.GetInt ("Best")) {
			Debug.Log ("WOAH");
				PlayerPrefs.SetInt ("Best", Mathf.RoundToInt (player.transform.position.x));
			bestText.text = PlayerPrefs.GetInt ("Best").ToString ();
			Debug.Log ("NEW BEST IT:" + Mathf.RoundToInt (player.transform.position.x));
		}
			


	}
	void End()
	{

	}

	public void Death ()
	{
		//this line prevents this from happening several times
		counter = 0;
		dead = true;
		Debug.Log ("I'm Dreaead");
		restartText.enabled = true;
		//SHOW SCORE BEST SCOORE AND PRESS R TO RESTART
		player.GetComponent<Animator>().SetBool("Dead",true);

	}

	public GameObject CoinDropper(Vector3 coinPos, float coinChance,GameObject parentGO)
	{

		if (Random.value < coinChance) {

			GameObject coin = (GameObject)Instantiate (coinFab, coinPos, Quaternion.identity);

			coin.transform.SetParent (parentGO.transform);

			return coin;
		}


		return null;
	}
}
