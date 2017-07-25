using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public int score = 0;
	public Vector3 touchPosition;
	public bool canTouch = true;
	public bool isPaused = false;
	public AudioClip sawbladeSpinningAudio;

	void Start()
	{
		PlayerPrefsX.SetLong("CurrentScore", 0);
		GetComponent<AudioSource>().PlayOneShot(sawbladeSpinningAudio, 0.4F);
	}

	void Update()
	{
		canTouch = true;
		if ( (Application.platform != RuntimePlatform.IPhonePlayer) )
		{
			touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
		}
		else if ( (Application.platform == RuntimePlatform.IPhonePlayer) )
		{
			touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.transform.position.z * -1));
		}
	}

	public void GameWin()
	{
		Debug.Log("jogo terminou, voce ganhou");
	}

	public void GameLoose()
	{
		SMSceneManager sceneManager = new SMSceneManager(SMSceneConfigurationLoader.LoadActiveConfiguration("Config"));
       	sceneManager.TransitionPrefab = "Config/Custom Fade Transition";

		sceneManager.LoadScreen("GameOver");

		PlayerPrefsX.SetLong("CurrentScore", score);
		Debug.Log(PlayerPrefsX.GetLong("CurrentScore", 0));
	}

	public void Score(int point)
	{
		if ( point > 0 ) PositiveScore();
		else NegativeScore();
	}

	void PositiveScore()
	{
		score++;
	}

	void NegativeScore()
	{
		
	}
}