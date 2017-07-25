using UnityEngine;
using System.Collections;

public class GUIManagerStart : MonoBehaviour
{
	void Awake()
	{
		Debug.Log("passou");
		Application.targetFrameRate = 60;
	}

	void Start ()
	{
		EasyFontTextMesh score = GameObject.Find("Score").GetComponent<EasyFontTextMesh>();
		score.Text = "" + PlayerPrefsX.GetLong("HighScore", 0);
	}
}