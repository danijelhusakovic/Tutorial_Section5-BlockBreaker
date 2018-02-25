using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;
	private bool isResting = true;
	private Vector2 speedModificationVector, nullVector2D;
	private Vector3 paddleToBallVector;
	private float speedModification = 150f;
	
	// Use this for initialization
	void Start () {
	paddle = GameObject.FindObjectOfType<Paddle>();
	paddleToBallVector = this.transform.position - paddle.transform.position;
	speedModificationVector = new Vector2(speedModification, speedModification);
	}
	
	// Update is called once per frame
	void Update () {
	if((Input.GetKeyDown(KeyCode.KeypadPlus))||(Input.GetKeyDown(KeyCode.Plus))){
		gameObject.GetComponent<Rigidbody2D>().velocity += speedModificationVector;
		Debug.Log (gameObject.GetComponent<Rigidbody2D>().velocity);
	} else if ((Input.GetKeyDown(KeyCode.KeypadMinus))||(Input.GetKeyDown(KeyCode.Minus))){
		gameObject.GetComponent<Rigidbody2D>().velocity -= speedModificationVector;	
		}
		
		
		
		if(isResting){
		// Lock the ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			// Wait for mouse click to launch the ball
			if(Input.GetMouseButtonDown(0)){
				this.GetComponent<Rigidbody2D>().velocity = new Vector2 (2f, 10f);
				isResting = false;
			}
		}
		
	}
	
	void OnCollisionEnter2D (Collision2D collision){
		
		Vector2 tweak = new Vector2 (Random.Range (0f, 0.2f), Random.Range (0f, 0.2f));
		
		if(!isResting){
			GetComponent<AudioSource>().Play();
			GetComponent<Rigidbody2D>().velocity += tweak;
		}
	}
	
}
