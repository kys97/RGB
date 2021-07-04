using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	float horizontalMove = 0f;
	bool jump = false;
	[SerializeField]
	bool crouch = false;
	bool dash = false;
	public bool fire = false;
    
    void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * PlayerStat.instance.runSpeed;

        if (Input.GetButtonUp("Dash"))
        {
			dash = true;
        }

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if (Input.GetButtonDown("Fire1"))
		{
			controller.Fire();
		}

        if (Input.GetButtonDown("s_Attack"))
        {
			controller.StrongAttack();
        }
	}

	void FixedUpdate ()
	{
        if (fire)
        {
			fire = false;
        }
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
		dash = false;
		jump = false;
	}
}
