using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region variables
    bool isJumping;

    [SerializeField]
    float jumpForceY;

    #endregion

    #region properties

    #endregion


    #region methods

    void Start ()
    {
        isJumping = true;
        jumpForceY = 300;
	}

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
	}

    void Jump()
    {
        if (isJumping)
            return;

        isJumping = true;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForceY));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		GameObject collider = collision.collider.gameObject;
        if (collider.tag == "obstacle" 
			&& (collider.transform.position.y - collider.transform.localScale.y) < (transform.position.y - transform.localScale.y))
        {
            isJumping = false;
		}
    }

    #endregion methods
}
