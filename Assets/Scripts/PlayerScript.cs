using System;
using Assets.Scripts.Extensions;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Properties

    public float MoveSpeed = 8f;
    public float JumpSpeed = 8f;

    private float _rayDistance = .6f;

    private bool isGrounded;
    private bool collisionRight;
    private bool collisionLeft;
    private bool wallCollision { get { return collisionLeft || collisionRight; } }

    private float ySpd;
    private float xSpd;

    #endregion

    void Update()
    {
        #region Movement

        //if not pressing button, slow player down
        if (Math.Abs(Input.GetAxisRaw("Horizontal")) < .05)
            xSpd = xSpd.IncrementTo(0, isGrounded ? MoveSpeed : .1f);

        //x movement (slower if not on ground)
        if (isGrounded)
            xSpd += Input.GetAxisRaw("Horizontal") * MoveSpeed;
        else
            xSpd += Input.GetAxisRaw("Horizontal") * MoveSpeed / 25;

        //limit x movement speed
        if (Math.Abs(xSpd) > MoveSpeed)
            xSpd = MoveSpeed * (xSpd < 0 ? -1 : 1);

        //gravity
        ySpd = isGrounded ? 0 : (ySpd - .25f);

        //jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || wallCollision)
            {
                ySpd = JumpSpeed;

                //push off from the wall if wall jumping
                if (collisionLeft) xSpd = MoveSpeed;
                else if (collisionRight) xSpd = MoveSpeed*-1;
            }
        }

        //collision with walls
        if (xSpd < 0 && collisionLeft) xSpd = 0;
        if (xSpd > 0 && collisionRight) xSpd = 0;

        //move the player based on the x and y computed above
        transform.Translate(xSpd * Time.deltaTime, ySpd * Time.deltaTime, 0, transform);

        #endregion

        #region Collisions

        RaycastHit down;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out down, _rayDistance);
        if (isGrounded)
        {
            Debug.DrawLine(down.point, down.point + Vector3.down * 5f, Color.red);
            transform.position = down.point + Vector3.up * .5f;
        }

        RaycastHit left;
        collisionLeft = Physics.Raycast(transform.position, Vector3.left, out left, _rayDistance);
        if (collisionLeft)
        {
            Debug.DrawLine(left.point, left.point + Vector3.left * 5f, Color.red);
            transform.position = left.point + Vector3.right * .51f;
        }

        RaycastHit right;
        collisionRight = Physics.Raycast(transform.position, Vector3.right, out right, _rayDistance);
        if (collisionRight)
        {
            Debug.DrawLine(right.point, right.point + Vector3.right * 5f, Color.red);
            transform.position = right.point + Vector3.left * .51f;
        }

        #endregion
    }
}

