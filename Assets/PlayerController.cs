using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {


	public LayerMask obstacles;


	public float logTimer=0.2f;
	public float logOffset;



	public Vector3 nextPos;
	public Vector3 currentWorldPos;
	public float jumpForce=100f;
	public float speed= 0.05f;
	public float speedRot=0.05f;

	public float rotationOffset=90;

	public bool canPlat = true;
	public Vector3 facingDir;

	public bool onPlatform;
	public GameObject pivotPoint;



	Rigidbody rb;
	// Use this for initialization


	void Start () {
		currentWorldPos = transform.position;
		rb = GetComponent<Rigidbody> ();
		facingDir = Vector3.right;
	}




	
	// Update is called once per frame
	void Update () {



		if (GameManager.instance.dead)
			return;
	
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation (Quaternion.Euler(0,rotationOffset,0)*facingDir), speedRot*Time.deltaTime);



		if ( transform.position != new Vector3 (currentWorldPos.x + nextPos.x, transform.position.y, currentWorldPos.z + nextPos.z)) {
			
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (currentWorldPos.x + nextPos.x, transform.position.y, currentWorldPos.z + nextPos.z), speed*Time.deltaTime);

		


		}
		else {

			/*if (onPlatform && transform.position.x == currentWorldPos.x) {
				transform.position = pivotPoint.transform.position;
				Debug.Log ("Working");
			}*/

			//resets position
			nextPos = Vector3.zero;

			if (Input.GetAxisRaw ("Horizontal") != 0) {
				nextPos.z = -Input.GetAxisRaw ("Horizontal");

			} else if (Input.GetAxisRaw ("Vertical") != 0) {
				nextPos.x = Input.GetAxisRaw ("Vertical");
				//THIS SHOULD STAY M8!


				/*
				if(onPlatform)
					StartCoroutine(LogTime());
				onPlatform=false;*/


				//logOffset is zero when it gets out of the log
				logOffset = 0;
				onPlatform=false;
			}

			//sets curposition
			currentWorldPos = transform.position;




			//COLLISION AND MOVEMENT  if it moved
			if(nextPos.x!=0 || nextPos.z!=0)
			{
				
				facingDir = nextPos;


				RaycastHit hit;
				Physics.Raycast (transform.position, nextPos, out hit, 1,obstacles);

				//NO OBJECT IN FRONT
				if (hit.collider == null) {

						rb.AddForce (Vector3.up * jumpForce, ForceMode.Acceleration);
				

					//if is on platform, changes offset within log according to movement
					if (onPlatform) {
						OnPlatformStuff ();

					}


				} 
				else{ //if it hits something then it shoudnt move

					nextPos = Vector3.zero;

				}		
		}

			//if on platform set the current world pos to this
			if (onPlatform) {
				currentWorldPos = pivotPoint.transform.position + Vector3.forward*logOffset;
			} else {
				//if not on platform then normalize it


				currentWorldPos.x = Mathf.Round (currentWorldPos.x);
				currentWorldPos.z = Mathf.Round (currentWorldPos.z);
			}


		}


	}


	IEnumerator LogTime()
	{
		canPlat = false;
		yield return new WaitForSeconds ((1/(speed*10)));
		canPlat = true;
	}

	void OnPlatformStuff()
	{
		if(nextPos.z!=0)
		{logOffset += nextPos.z;
				nextPos=Vector3.zero;
		}

	}

	//sets the pivot when it collides with the log
	void DoLogStuff(GameObject log)
	{
		float closestPosition=100;


		foreach (Transform t in log.transform) {
			float distance = (t.position - transform.position).magnitude;

			if (distance < closestPosition) {
				closestPosition = distance;
				pivotPoint = t.gameObject;
			}
		
		}
		currentWorldPos = pivotPoint.transform.position;


	}



	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position,transform.position+nextPos);
	}


	void Die()
	{
		Debug.Log ("I'm Dead");
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Log" && !onPlatform) {
			onPlatform = true;
			Debug.Log ("Entered Log");
			DoLogStuff (col.gameObject);
			nextPos = Vector2.zero;
		} else if (col.tag == "Car")
			GameManager.instance.Death ();

	}



}
