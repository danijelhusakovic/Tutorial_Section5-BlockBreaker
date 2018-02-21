using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public static int breakableCount = 0;
	
	public AudioClip crack;
	public Sprite[] hitSprites;
	public GameObject smoke;
	
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		isBreakable = (this.tag == "Breakable");
		timesHit = 0;
		if(isBreakable){
			breakableCount++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		print(breakableCount);
	}
	
	void OnCollisionExit2D (Collision2D collision){
		AudioSource.PlayClipAtPoint(crack, transform.position);
		if (isBreakable){
			HandleHits();
		}
	}
	
	void HandleHits() {
		this.timesHit++;
		int maxHits = hitSprites.Length + 1;
		if(timesHit >= maxHits){
			breakableCount--;
			PuffSmoke();
			// messaging the level manager each time a brick is destroyed
			// levelmanager asks itself "was that the last one?"
			// if so, loads next level :)
			// simple as that
			levelManager.BrickDestroyed();
			Destroy(gameObject);
		} else {
			//if the brick still lives, change its appearance in addition to timesHit++
			LoadSprites();
		}
	}
	
	void PuffSmoke(){
		GameObject smokePuff = Instantiate (smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		if(hitSprites[spriteIndex] != null){ // this could be just if(hitSprites[spriteIndex]), but this is clearer
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		} else {
			Debug.LogError ("Brick sprite missing");
		}
	}
	
	
	void SimulateWin(){
		levelManager.LoadNextLevel();
	}
	
}
