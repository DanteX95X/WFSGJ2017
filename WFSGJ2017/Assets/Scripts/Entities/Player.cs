using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region variables
    bool isJumping;
	Vector3 jumpDirection;
	Camera mainCamera;

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
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

		jumpDirection = Input.mousePosition;

			jumpDirection.z = transform.position.z - mainCamera.transform.position.z;
			jumpDirection = mainCamera.ScreenToWorldPoint(jumpDirection);
			jumpDirection = (jumpDirection - transform.position).normalized * jumpForceY;
			if (jumpDirection.y > 0)
			{
				isJumping = true;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpDirection.x, jumpDirection.y));
			}

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
