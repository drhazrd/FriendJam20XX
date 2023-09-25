using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform mover;
    public Animator anim;
    public float moveSpeed;
    public float jumpHeight;
    private float XAxis = 0;
    private bool jump = false;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = mover.GetComponent<Rigidbody>();
    }

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
        float runAnimMultiplier = 1.2f;
        anim.SetFloat("moveSpeed", Mathf.Abs(XAxis) * runAnimMultiplier);
        anim.SetFloat("jumpHeight", _rb.velocity.y);
        _rb.AddForce(new Vector3(0, 0, XAxis * moveSpeed));
        anim.gameObject.transform.rotation = Quaternion.Euler(0, XAxis > 0 ? 0 : 180, 0);
    }

    private void Jump()
    {

    }
}
