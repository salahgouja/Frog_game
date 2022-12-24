using UnityEngine;
public class Goal : MonoBehaviour
{
	public AudioClip m_goalClip;
    public GameObject panel;

    private bool m_isGoal;

	private void OnTriggerEnter2D( Collider2D other )
	{
		if ( !m_isGoal )
		{
			if ( other.name.Contains( "Player" ) )
			{
				var cameraShake = FindObjectOfType<CameraShaker>();

				cameraShake.Shake();

				m_isGoal = true;

				var animator = GetComponent<Animator>();
				animator.Play( "Pressed" );

                panel.SetActive(true);

                var audioSource = FindObjectOfType<AudioSource>();
				audioSource.PlayOneShot( m_goalClip );
				panel.SetActive(true);
			}
		}
	}
}