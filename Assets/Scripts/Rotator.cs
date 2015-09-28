using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public int direction;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(direction * 15,direction * 30,direction * 45) * Time.deltaTime);
	}
}
