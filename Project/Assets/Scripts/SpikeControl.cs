using UnityEngine;
using System.Collections;

public class SpikeControl : MonoBehaviour
{
	public float rotateVariation;
	GameManager gameManager;

	void Start ()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;

		spriteRenderer.color = PlayerPrefsX.GetColor(name, new Color(1, 1, 1, 1));

		StartCoroutine(InfiniteRotation());
	}
	
	IEnumerator InfiniteRotation()
	{
		while (true)
		{
			if (!gameManager.isPaused) transform.Rotate(0,0,Time.deltaTime * rotateVariation);

			yield return 0;
		}
	}
}