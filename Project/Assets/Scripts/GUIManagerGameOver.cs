using UnityEngine;
using System.Collections;

public class GUIManagerGameOver : MonoBehaviour
{
	int counterScore = 0;
	int maxScore = 0;

	void Start ()
	{
		EasyFontTextMesh score = GameObject.Find("Score").GetComponent<EasyFontTextMesh>();
		score.Text = "" + PlayerPrefsX.GetLong("HighScore", 0);

		maxScore = (int)PlayerPrefsX.GetLong("CurrentScore", 0);

		#if UNITY_IPHONE
			LeaderboardIntegration leaderboardIntegration = GameObject.Find("Game Manager").GetComponent<LeaderboardIntegration>();
			leaderboardIntegration.SubmitNewScore(maxScore);
		#endif
		
		StartCoroutine(Counter());
	}

	IEnumerator Counter()
	{
		EasyFontTextMesh highScore = GameObject.Find("HighScore").GetComponent<EasyFontTextMesh>();
		highScore.Text = "" + PlayerPrefsX.GetLong("HighScore", 0);

		EasyFontTextMesh score = GameObject.Find("Score").GetComponent<EasyFontTextMesh>();

		while (counterScore < maxScore)
		{
			counterScore += 1;
			if ( counterScore > maxScore ) counterScore = maxScore;
			score.Text = "" + counterScore;

			yield return 0;
		}
		score.Text = "" + maxScore;

		if ( maxScore > PlayerPrefsX.GetLong("HighScore", 0) )
		{
			highScore.Text = "" + maxScore;
			PlayerPrefsX.SetLong("HighScore", maxScore);
		}
	}

	void Update()
	{
		if ( (Application.platform != RuntimePlatform.IPhonePlayer) && (Input.anyKeyDown) ) counterScore = maxScore;
		else if ( (Application.platform == RuntimePlatform.IPhonePlayer) && (Input.GetTouch(0).phase == TouchPhase.Began) ) counterScore = maxScore;
	}
}