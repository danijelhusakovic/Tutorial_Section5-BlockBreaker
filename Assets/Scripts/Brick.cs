using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public static int breakableCount = 0;
	
	public AudioClip crack;
	public Sprite[] hitSprites;
	
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
	
	void OnCollisionEnter2D (Collision2D collision){
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
	
	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		if(hitSprites[spriteIndex]){
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
	
	
	void SimulateWin(){
		levelManager.LoadNextLevel();
	}
	
}
