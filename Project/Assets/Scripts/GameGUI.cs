using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
	GameManager gameManager;

	void Start ()
	{
		gameManager = GetComponent<GameManager>();
	}

	void Update()
	{
		EasyFontTextMesh score = GameObject.Find("Score").GetComponent<EasyFontTextMesh>();
		score.Text = "" + gameManager.score;
	}
}