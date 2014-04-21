using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MoveSpeed = 15f;
    public float JumpSpeed = 250f;

    private float _rayDistance = .6f;

    private bool isGrounded;
    private bool collisionRight;
    private bool collisionLeft;

    private bool wallCollision { get { return collisionLeft || collisionRight; } }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Movement

        float spd = Input.GetAxisRaw("Horizontal") * MoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(0, JumpSpeed, 0);
        }

        if (spd < 0 && collisionLeft) spd = 0;
        if (spd > 0 && collisionRight) spd = 0;

        transform.Translate(spd*Time.deltaTime, 0, 0, transform);

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
            transform.position = left.point + Vector3.right * .5f;
        }

        RaycastHit right;
        collisionRight = Physics.Raycast(transform.position, Vector3.right, out right, _rayDistance);
        if (collisionRight)
        {
            Debug.DrawLine(right.point, right.point + Vector3.right * 5f, Color.red);
            transform.position = right.point + Vector3.left * .5f;
        }

        #endregion
    }
}
