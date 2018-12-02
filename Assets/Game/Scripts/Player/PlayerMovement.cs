using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    public PlayerController player;

    private float horizontalMove;
    private bool jump;
    private bool crouch;

	void Update ()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
            jump = true;
        if (Input.GetKeyDown(KeyCode.LeftControl))
            crouch = true;
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            crouch = false;
	}
    private void FixedUpdate()
    {
        player.MovePlayer(horizontalMove, crouch, jump);
        jump = false;
    }
}
