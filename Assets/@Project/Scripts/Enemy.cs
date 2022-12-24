using PC2D;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject m_hitPrefab;

	public AudioClip m_hitClip;

	private PlatformerMotor2D m_motor;

	private SpriteRenderer m_renderer;

	private BoxCollider2D m_collider;

	private void Awake()
	{
		m_motor = GetComponent<PlatformerMotor2D>();

		m_motor.normalizedXMovement = -1;

		m_renderer = GetComponent<SpriteRenderer>();

		m_renderer.flipX = false;

		m_collider = GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
		var dir = 0 < m_motor.normalizedXMovement
			? Vector3.right
			: Vector3.left;

		var offset = m_collider.size.y * 0.5f;
		var hit = Physics2D.Raycast
		(
			transform.position - new Vector3( 0, offset, 0 ),
			dir,
			m_collider.size.x * 0.55f,
			Globals.ENV_MASK
		);

		if ( hit.collider != null )
		{
			m_motor.normalizedXMovement = -m_motor.normalizedXMovement;

			m_renderer.flipX = !m_renderer.flipX;
		}
	}

	private void OnTriggerEnter2D( Collider2D other )
	{
		if ( other.name.Contains( "Player" ) )
		{
			var motor = other.GetComponent<PlatformerMotor2D>();

			if ( motor.IsFalling() )
			{
				Destroy( gameObject );

				motor.ForceJump();

				var cameraShake = FindObjectOfType<CameraShaker>();

				cameraShake.Shake();

				Instantiate( m_hitPrefab, transform.position, Quaternion.identity );

				var audioSource = FindObjectOfType<AudioSource>();
				audioSource.PlayOneShot( m_hitClip );

				var player = other.GetComponent<Player>();
				player.IsSkipJumpSe = true;
			}
			else
			{
				var player = other.GetComponent<Player>();
				player.Dead();
			}
		}
	}
}