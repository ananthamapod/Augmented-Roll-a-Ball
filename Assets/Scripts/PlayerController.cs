using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public bool firstPlayer;

	public Text countText;

	private Rigidbody rb;
	public int count;

	private float speed;
	private float height;

	private bool startedJump;
	private bool isJumping;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		speed = 10;
		count = 0;
		height = 0;
		startedJump = false;
		isJumping = false;
		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis (firstPlayer? "Horizontal1" : "Horizontal2");
		float moveVertical = Input.GetAxis (firstPlayer? "Vertical1" : "Vertical2");
		float moveUp = 0.0f;

		//If ball is jumping, pressing space has no effect
		//Ball goes up for 10 frames
		//Once ball is at top at 10 frames
		//Ball comes down
		if (isJumping) {
			//Have to cancel out the jumping force after 10 frames
			if (startedJump && height >= 5000) {
				moveUp = -25.0f;
				startedJump = false;
			}
			if (transform.position.y <= 0.5f) {
				isJumping = false;
			}
			height++;
		} else {
			if (Input.GetKey (firstPlayer? KeyCode.Space: KeyCode.Return)) {
				height = 0;
				moveUp = 25.0f;
				startedJump = true;
				isJumping = true;
			}
		}

		Vector3 movement = new Vector3 (moveHorizontal, moveUp, moveVertical);

		rb.AddForce (movement * speed);

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag ("Player")) {
			if(other.gameObject.transform.position.y > transform.position.y) {
				count--;
                SetCountText();
            }
        } else if (other.gameObject.CompareTag("Wall")) {
            count--;
            SetCountText();
        }
    }

	void SetCountText () {
		countText.text = "Count: " + count.ToString ();
	}
}
