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

    // Update is called once per frame
    void Update()
    {
        #region Movement

        if (isGrounded)
            xSpd = 0;

        xSpd = Input.GetAxisRaw("Horizontal") * MoveSpeed;

        ySpd = isGrounded ? 0 : (ySpd - .25f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || wallCollision)
            {
                ySpd = JumpSpeed;
            }
        }

        if (xSpd < 0 && collisionLeft) xSpd = 0;
        if (xSpd > 0 && collisionRight) xSpd = 0;

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
