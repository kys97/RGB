using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform shootingPos;
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
	[SerializeField]
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	public GameObject bullet;
	public GameObject s_bullet;
	public GameObject bombEffect;
	Queue<GameObject> bullets = new Queue<GameObject>();
	Queue<GameObject> s_bullets = new Queue<GameObject>();

	public int maxBullets;
	public float dashForce;
	[HideInInspector] public bool isCrouching = false;
	[HideInInspector] public bool isMoving = false;
	[HideInInspector] public bool isAttacking = false;
	[HideInInspector] public bool isDashing = false;
	Collider2D[] hit;
	public LayerMask bulletLayer;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		for (int i = 0; i < maxBullets; i++)
		{
			CreateBullet();
		}
		for(int i = 0; i < 4; i++)
        {
			CreateS_Bullet();
        }

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool crouch, bool jump, bool dash)
	{
		if (move == 0)
			isMoving = false;
		else
			isMoving = true;
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}
		isCrouching = crouch;

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (dash)
			{
				Dash();
			}
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
        
	}
	//ÃÑ¾Ë ¹ß»ç
	public void Fire()
    {
		isAttacking = true;
		if (bullets.Count == 0)
			CreateBullet();
		GameObject bullet = bullets.Dequeue();
		bullet.SetActive(true);
        if (!m_FacingRight)
        {
			bullet.GetComponent<Bullet>().isLookingRight = false;
		}
		bullet.transform.position = shootingPos.position;
	}
	void Dash()
    {
		if(PlayerStat.instance.GetGage() > 0)
        {
			isDashing = true;
			if (m_FacingRight)
				m_Rigidbody2D.AddForce(new Vector2(1, 0) * dashForce, ForceMode2D.Impulse);
			else
				m_Rigidbody2D.AddForce(new Vector2(-1, 0) * dashForce, ForceMode2D.Impulse);
			PlayerStat.instance.ChangeGage(-1);
		}
	}
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void CreateBullet()
	{
		GameObject newBullet = Instantiate(bullet);
		bullets.Enqueue(newBullet);
		newBullet.SetActive(false);
	}
	void CreateS_Bullet()
    {
		GameObject newBullet = Instantiate(s_bullet);
		s_bullets.Enqueue(newBullet);
		newBullet.SetActive(false);
	}
	public void ReturnBullet(GameObject bullet)
    {
		if(bullet.gameObject.tag == "Bullet")
        {
			bullets.Enqueue(bullet);
			bullet.SetActive(false);
		}
        else
        {
			s_bullets.Enqueue(bullet);
			bullet.SetActive(false);
		}
    }
	public void StrongAttack()
    {
		if (PlayerStat.instance.GetGage() < 3)
			return;
		PlayerStat.instance.ChangeGage(-3);
		if (s_bullets.Count == 0)
			CreateS_Bullet();
		GameObject s_bullet = s_bullets.Dequeue();
		s_bullet.SetActive(true);
		if (!m_FacingRight)
		{
			s_bullet.GetComponent<Bullet>().isLookingRight = false;
		}
		s_bullet.transform.position = shootingPos.position;
	}

	public void SpecialAttack(float bombRadius)
    {
		StartCoroutine(EnableBomb());
		hit = Physics2D.OverlapCircleAll(transform.position, bombRadius, bulletLayer);
		if (hit!=null)
        {
			for(int i = 0; i < hit.Length; i++)
            {
				hit[i].gameObject.SetActive(false);
            }
        }
    }
	IEnumerator EnableBomb()
    {
		bombEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		bombEffect.SetActive(false);
    }
}
