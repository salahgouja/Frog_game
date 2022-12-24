using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public GameObject m_playerHitPrefab;
	public GameObject playBtn;
	public GameObject panel;
	public AudioClip m_jumpClip;
	public AudioClip m_hitClip;
	public bool IsSkipJumpSe;
    public int score;
    public TextMeshProUGUI scoreText;
    // when player died

    public void Start()
	{

        score = 0;
	}


           private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Apple"))
        {
            score++;
            scoreText.text = score.ToString();

        }
    }

	public void restart()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
	public void quit()
	{
        SceneManager.LoadScene(0);


    }
    public void OnRestart()
	{
        Invoke("restart", 2);


    }




    public void play()
	{
        SceneManager.LoadScene(0);

    }
    public void Dead()
	{
		gameObject.SetActive( false );

		var cameraShake = FindObjectOfType<CameraShaker>();

		cameraShake.Shake();

		Invoke("showDeathScreen", 2 );

		Instantiate
		(
			m_playerHitPrefab,
			transform.position,
			Quaternion.identity
		);

		var audioSource = FindObjectOfType<AudioSource>();
		audioSource.PlayOneShot( m_hitClip );

    }


    private void OnRetry()
	{
		// reload scene
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
	}
    private void showDeathScreen()
	{
        panel.SetActive(true);

    }

    private void Awake()
	{
		var motor = GetComponent<PlatformerMotor2D>();

		motor.onJump += OnJump;
	}

	private void OnJump()
	{
		// if can jump play
		if ( IsSkipJumpSe )
		{
			IsSkipJumpSe = false;
		}
		else
		{
			var audioSource = FindObjectOfType<AudioSource>();
			audioSource.PlayOneShot( m_jumpClip );
		}
	}
}