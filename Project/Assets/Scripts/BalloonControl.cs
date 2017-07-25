using UnityEngine;
using System.Collections;

public class BalloonControl : MonoBehaviour
{
    public float upSpeed;
    public float sideSpeed;
    public Sprite splash;
    public Sprite perforated;
    public GameObject balloonLight;
    public AudioClip[] balloonPops;
    public AudioClip balloonBigPop;

    public int point;
    GameManager gameManager;

    bool canTouch = true;
    bool splashed = false;

	void Start ()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;

		spriteRenderer.color = new Color(Random.Range(0, 10) * 0.1f, Random.Range(0, 10) * 0.1f, Random.Range(0, 10) * 0.1f, 1);

		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		while (true)
		{
			if (!gameManager.isPaused)
			{
				if (canTouch) transform.position += (Vector3.up * upSpeed) + (Vector3.right * sideSpeed);
				else if (!canTouch && !splashed) transform.position += Vector3.down * upSpeed * 1.5f;
			}
			yield return 0;
		}
	}

	void Update ()
	{
		if ( !gameManager.isPaused )
		{
			if ( (Application.platform != RuntimePlatform.IPhonePlayer) && (canTouch && Input.anyKeyDown) )
			{
				if ( gameManager.canTouch && GetComponent<Renderer>().bounds.Contains(gameManager.touchPosition) ) Perforated();
			}
			else if ( (Application.platform == RuntimePlatform.IPhonePlayer) && (canTouch && Input.GetTouch(0).phase == TouchPhase.Began) )
			{
				if ( gameManager.canTouch && GetComponent<Renderer>().bounds.Contains(gameManager.touchPosition) ) Perforated();
			}
		}
	}

	void Perforated()
	{
		GetComponent<AudioSource>().PlayOneShot(balloonPops[Random.Range(0, balloonPops.Length)], 0.8F);

		canTouch = false;

		GameObject.Find("Game Manager").GetComponent<GameManager>().Score(point);

		SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		spriteRenderer.sprite = perforated;

		StartCoroutine(RemoveFromGame());

		balloonLight.transform.localScale = new Vector3(balloonLight.transform.localScale.x * 0.5f, balloonLight.transform.localScale.y * 0.6f, balloonLight.transform.localScale.z);
		balloonLight.transform.position += Vector3.right * 0.1f;

		gameManager.canTouch = false;
	}

	IEnumerator RemoveFromGame()
	{
		for ( int i = 0; i < 480; i++ ) yield return 0;

		Object.Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ( canTouch )
		{
			canTouch = false;

			SpriteRenderer spikeRenderer = col.gameObject.GetComponent<Renderer>() as SpriteRenderer;
			SpriteRenderer balloonRenderer = GetComponent<Renderer>() as SpriteRenderer;

			spikeRenderer.color = balloonRenderer.color;

			StartCoroutine(MakePop());

			#if UNITY_IPHONE
				Handheld.Vibrate();
			#endif

			PlayerPrefsX.SetColor(col.gameObject.name, spikeRenderer.color);
		}
    }

    IEnumerator MakePop()
    {
    	GetComponent<AudioSource>().PlayOneShot(balloonBigPop, 0.8F);

    	splashed = true;
    	transform.localScale = new Vector3(0.01f, 0.01f, 1);

    	SpriteRenderer balloonRenderer = balloonLight.GetComponent<Renderer>() as SpriteRenderer;
    	balloonRenderer.enabled = false;

    	SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		spriteRenderer.sprite = splash;

    	bool maxScale = true;
    	while (maxScale)
    	{
    		transform.localScale += new Vector3(0.08f, 0.08f, 1);

    		if (transform.localScale.x > 1.6f)
    		{
    			transform.localScale = new Vector3(1.6f, 1.6f, 1);
    			maxScale = false;
    		}

    		yield return 0;
    	}

    	spriteRenderer.enabled = false;

    	StartCoroutine(FinishGame());
    }

    IEnumerator FinishGame()
	{
		for ( int i = 0; i < 30; i++ ) yield return 0;

		GameObject.Find("Game Manager").GetComponent<GameManager>().GameLoose();
		Object.Destroy(gameObject);
	}
}