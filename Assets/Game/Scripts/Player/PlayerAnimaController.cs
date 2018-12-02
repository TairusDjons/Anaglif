using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimaController : MonoBehaviour {

    public PlayerController player;
    public Animator anima;
    private bool doubleJump;
    void Start() {
        player.OnDoubleJumpedEvent += JumpAnimation;
        player.OnLandedEvent += Landed;
        player.OnWallEvent += OnWall;
        player.OnCrouchEvent += Crouch;
        

    }

    // Update is called once per frame
    void Update() {
        float speed = player.player.velocity.x;
        float vSpeed = player.player.velocity.y;
        anima.SetFloat("Speed", Mathf.Abs(speed));
        anima.SetFloat("vSpeed", vSpeed);
        anima.SetBool("Grounded", player.l_grounded);
    }

    void Grounded()
    {
        anima.SetBool("Grounded", false);
    }


    void Crouch(bool value)
    {
        anima.SetBool("Crouch", value);
    }

    void OnWall(bool value)
    {
        anima.SetBool("Wall", value);
    }

    void JumpAnimation()
    {
        anima.SetTrigger("DoubleJump");
    }

    void Landed()
    {
        anima.SetBool("Grounded", true);
    }

}
