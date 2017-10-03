using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	// Use this for initialization
	void Start ()
	{
		score = 0;
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		
	}


	IEnumerator SpawnWaves()
	{
			yield return new WaitForSeconds (startWait);
		while (!gameOver) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
		restartText.text = "Press 'R' to restart";
		restart = true;
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	void Update(){
		if(restart){
			if(Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}
	}
}
