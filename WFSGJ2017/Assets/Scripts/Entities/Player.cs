using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region variables

    bool isJumping;

    #endregion

    #region properties

    #endregion


    #region methods

    void Start ()
    {
        Debug.Log("Player created");
        isJumping = true;
	}

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
	}

    void Jump()
    {
        isJumping = true;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "obstacle")
        {
            Debug.Log("On ground");
            isJumping = false;
        }
    }

    #endregion methods
}
