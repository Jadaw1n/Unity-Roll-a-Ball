using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float jumpHeight;
	public Text countText;
	public Text winText;

	private int count = 0;
	private int winCondition;
	private Rigidbody rb;
	private DateTime gameStart;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		print("Hello World!");
		updateText ();
		winText.enabled = false;
		winCondition = GameObject.FindGameObjectsWithTag ("Pickup").Length;
		gameStart = DateTime.Now;
	}

	void updateText() {
		countText.text = "Count: " + count;

		if (count == winCondition) {
			var diff = DateTime.Now.Subtract (gameStart);
			winText.enabled = true;
			winText.text = string.Format("You have won the game!\nTime: {0}:{1}.{2}", diff.Minutes, diff.Seconds, diff.Milliseconds);
		}
	}
	// physics code
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce (movement * speed);

		if (Input.GetKeyDown ("space") && rb.transform.position.y <= 0.55){
			
			rb.AddForce (Vector3.up * jumpHeight);
		}


	}

	private GameObject lastCollided;

	void OnTriggerEnter(Collider other) {
		print (other.name);


		if (other.gameObject.CompareTag ("Pickup")) {
			// deactivate the object:
			other.gameObject.SetActive(false);

			count += 1;
			updateText ();

//			if (lastCollided != null && Random.Range(0, 10) <= 5) {
//				lastCollided.SetActive (true);
//			}
//
//			lastCollided = other.gameObject;
		}

		// remove from scene:
		//Destroy(other.gameObject);

	}
}

