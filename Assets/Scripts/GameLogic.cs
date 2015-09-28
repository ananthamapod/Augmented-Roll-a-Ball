using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour {
	public Text winText;

	private GameObject[] pickups;
	private int numPickups = 0;

    private GameObject[] players;
	// Use this for initialization
	void Start () {
		winText.text = "";
	}
	
    void FixedUpdate()
    {
        //Reset level
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
    }

    // Update is called once per frame
    void LateUpdate () {
		int deadCount = 0;

		if (numPickups == 0) {
			pickups = GameObject.FindGameObjectsWithTag ("PickUp");
            numPickups = pickups.Length;
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        foreach (GameObject pickup in pickups) {
			if( !pickup.activeSelf) {
				deadCount++;
			}
		}
        if (deadCount == numPickups) {
			int winner = 0;
            int points = 0;
			for(int i = 0; i < 2; i++) {
				PlayerController controllerScript = players[i].GetComponent<PlayerController>();
                if (controllerScript.count > points) {
                    points = controllerScript.count;
                    winner = controllerScript.firstPlayer? 1 : 2;
                }
			}
			winText.text = winner == 0? "Draw" : (winner == 1? "Player 1 Wins!" : "Player 2 Wins!");
            Time.timeScale = 0;

            //Hack to reset level even after time has stopped
            if (Input.GetKey(KeyCode.R))
            {
                Application.LoadLevel(0);
                Time.timeScale = 1;
            }
        }
    }
}
