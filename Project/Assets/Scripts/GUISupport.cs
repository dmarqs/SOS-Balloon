using UnityEngine;
using System.Collections.Generic;

public class GUISupport : MonoBehaviour
{
	bool buttonHold = false;
	public GameObject menuBackground;
	public AudioClip buttonClick;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if ( (Application.platform != RuntimePlatform.IPhonePlayer) && !buttonHold && (Input.GetMouseButtonDown(0)) )
		{
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));

			if ( GetComponent<Renderer>().bounds.Contains(touchPosition) ) ButtonHold();
		}
		else if ( (Application.platform == RuntimePlatform.IPhonePlayer) && !buttonHold && (Input.GetTouch(0).phase == TouchPhase.Began) )
		{
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.transform.position.z * -1));

			if ( GetComponent<Renderer>().bounds.Contains(touchPosition) ) ButtonHold();
		}
		
		if ( (Application.platform != RuntimePlatform.IPhonePlayer) && (Input.GetMouseButtonUp(0)) )
		{
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));

			if ( GetComponent<Renderer>().bounds.Contains(touchPosition) && buttonHold ) OpenScene();
			else ButtonUp();
		}
		else if ( (Application.platform == RuntimePlatform.IPhonePlayer) && (Input.GetTouch(0).phase == TouchPhase.Ended) )
		{
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.transform.position.z * -1));

			if ( GetComponent<Renderer>().bounds.Contains(touchPosition) && buttonHold ) OpenScene();
			else ButtonUp();
		}
	}

	void OpenScene()
	{
		ButtonUp();

		if ( name == "PlayButton" )
		{
			SMSceneManager sceneManager = new SMSceneManager(SMSceneConfigurationLoader.LoadActiveConfiguration("Config"));
           	sceneManager.TransitionPrefab = "Config/Custom Fade Transition";

			sceneManager.LoadScreen("MainGame");
		}
		else if ( name == "RankingButtonStartScene")
		{
			#if UNITY_IPHONE
				LeaderboardIntegration leaderboard = GameObject.Find("Game Manager").GetComponent<LeaderboardIntegration>();
				leaderboard.OpenLeaderboardView();
			#endif
		}
		else if ( name == "PauseButton" )
		{
			GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
			gameManager.gameShouldGoOn = false;
			menuBackground.SetActive(true);
		}
		else if ( name == "ResumeButton" )
		{
			GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
			gameManager.gameShouldGoOn = true;
			menuBackground.SetActive(false);
		}
	}

	void ButtonHold()
	{
		GetComponent<AudioSource>().PlayOneShot(buttonClick, 0.8F);
		
		buttonHold = true;

		SpriteRenderer sprite = GetComponent<Renderer>() as SpriteRenderer;
		sprite.color = new Color(0.6f, 0.6f, 0.6f, 1);
	}

	void ButtonUp()
	{
		buttonHold = false;

		SpriteRenderer sprite = GetComponent<Renderer>() as SpriteRenderer;
		sprite.color = new Color(1, 1, 1, 1);
	}
}