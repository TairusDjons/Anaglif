using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerEvent();
public delegate void PlayerEventBool(bool value);
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;
    public Transform ceillingCheck;
    public Transform groundCheck;
    public Transform wallCheck;
    public Collider2D disableColliderOnCrouch;
    public float speed;
    public float jumpImpulse;
    public float slideImpulse;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public int amountOfJump = 2;
    public float pushBackOnWall = 300f;
    public event PlayerEvent OnLandedEvent;
    public event PlayerEvent OnDoubleJumpedEvent;
    public event PlayerEventBool OnCrouchEvent;
    public event PlayerEventBool OnWallEvent;

    const float groundRadius = .2f;
    const float ceilingRadius = .2f;
    const float wallRadiust = .2f;

    public bool l_grounded;
    private bool doubleJump = true;
    private bool l_crouch;
    private bool onWall;
    private bool faceRight = true;
    private bool canImpulse = true;

    private Vector2 l_velocity = Vector2.zero;
    void Start() {

    }

    private void FixedUpdate()
    {
        bool wasGrounded = l_grounded;
        l_grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                l_grounded = true;
                onWall = false;
                OnWallEvent(false);
                if (!wasGrounded && OnLandedEvent != null)
                {
                    OnLandedEvent();
                    doubleJump = true;
                }
            }
        }
        if (!l_grounded)
        {
            bool wasOnWall = onWall;
            onWall = false;
            Collider2D[] walls = Physics2D.OverlapCircleAll(wallCheck.position, wallRadiust, whatIsWall);
            if (walls.Length == 0)
                OnWallEvent(false);
            foreach (Collider2D col in walls)
            {
                if (col.gameObject != gameObject)
                {
                    onWall = true;
                    doubleJump = true;
                    if (!wasOnWall && OnWallEvent != null)
                        OnWallEvent(true);
                }

            }
            
        }
    }
    public void MovePlayer(float move, bool crouch, bool jump)
    {
        if (!crouch)
            if (Physics2D.OverlapCircle(ceillingCheck.position, ceilingRadius, whatIsGround))
                l_crouch = true;

        if (l_grounded)
            if (crouch)
            {
                if (!l_crouch)
                {
                    if (OnCrouchEvent != null)
                        OnCrouchEvent(true);
                    l_crouch = true;
                    disableColliderOnCrouch.enabled = false;
                }
                if (canImpulse)
                {
                    player.AddForce(new Vector2(slideImpulse * move, 0), ForceMode2D.Impulse);
                    StartCoroutine(impulseWait());
                }
                if (Mathf.Abs(player.velocity.x) > 0)
                    player.velocity = Vector2.Lerp(player.velocity, Vector2.zero, Time.deltaTime);
                
                return;
            }
            else
            {
                if (OnCrouchEvent != null)
                    OnCrouchEvent(false);
                disableColliderOnCrouch.enabled = true;
                l_crouch = false;
            }

        Vector2 velocity = new Vector2(move * speed, player.velocity.y);
        player.velocity = Vector2.SmoothDamp(player.velocity, velocity, ref l_velocity, 0.5f, Mathf.Infinity, Time.deltaTime * speed);
        if (jump && !onWall)
        {

            if (l_grounded)
            {
                player.velocity = new Vector2(player.velocity.x, jumpImpulse);
                l_grounded = false;
            }
            else if (doubleJump)
            {
                if (OnDoubleJumpedEvent != null)
                {
                    OnDoubleJumpedEvent();
                }
                player.velocity = new Vector2(player.velocity.x * 1.3f, jumpImpulse);
                l_grounded = false;
                doubleJump = false;
            }

        }
     
    
        if (move > 0 && !faceRight)
            Flip();
        else if (move < 0 && faceRight)
            Flip();

    }


    IEnumerator impulseWait()
    {
        canImpulse = false;
        while (l_crouch)
            yield return new WaitForSeconds(0.4f);
        canImpulse = true;
    }

    private void Flip()
    {
        faceRight = !faceRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
