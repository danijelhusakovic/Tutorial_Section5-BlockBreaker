using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Paddle paddle;
	
	private bool isResting = true;
	private Vector3 paddleToBallVector;
	
	// Use this for initialization
	void Start () {
	paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isResting){
		// Lock the ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			// Wait for mouse click to launch the ball
			if(Input.GetMouseButtonDown(0)){
				this.rigidbody2D.velocity = new Vector2 (2f, 10f);
				isResting = false;
			}
		}
		
	}
	
}
