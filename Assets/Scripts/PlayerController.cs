using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform mover;
    public float moveSpeed;
    public float jumpHeight;
    private float XAxis = 0;
    private bool jump = false;

    void Update()
    {
        ProcessInput();
        Move();
        Jump();
    }

    private void ProcessInput()
    {
        XAxis = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        mover.transform.Translate(new Vector3(0, 0, XAxis * moveSpeed * Time.deltaTime));
    }

    private void Jump()
    {

    }
}
