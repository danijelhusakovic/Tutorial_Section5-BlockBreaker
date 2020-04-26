using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	private int _numScenes;
	private int _currentIndex;

	private void Start()
	{
		_numScenes = SceneManager.sceneCountInBuildSettings;
		_currentIndex = SceneManager.GetActiveScene().buildIndex;
	}

	void Update ()
	{
		if (_currentIndex >= _numScenes - 3) { return; }

		if(Input.GetKeyDown(KeyCode.RightArrow)){
			LoadNextLevel();
		}
	}

	public void LoadLevel(string name){
		Debug.Log("LoadLevel method entered for " + name);
		Application.LoadLevel(name);
	}
	
	public void LoadNextLevel()
	{
        if (_currentIndex >= _numScenes - 1) { return; }
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	public void QuitRequest(){
		Debug.Log("Quit requested!");
		Application.Quit();
	}
	
	public void BrickDestroyed(){
		if (Brick.breakableCount <= 0) {
			LoadNextLevel();
		}
	}
}
