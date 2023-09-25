using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform mover;
    public Animator anim;
    public float moveSpeed;
    public float jumpHeight;
    public float interactRange = 5;
    private float XAxis = 0;
    private bool jump = false;
    private Rigidbody _rb;
    private bool isRight = true;
    private bool canInteract = false;
    private GameObject interactableObject;
    private Collider[] interactCols;

    private void Start()
    {
        _rb = mover.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
        CheckInteractions();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void ProcessInput()
    {
        XAxis = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }

    private void CheckInteractions()
    {
        interactCols = Physics.OverlapSphere(mover.position, interactRange);

        if (interactCols.Length > 0)
        {
            foreach (Collider c in interactCols)
            {
                if (c.CompareTag("Interactable"))
                {
                    canInteract = true;
                    SetInteractableObject(c);
                    break;
                }
            }
        }
        else
        {
            canInteract = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            interactableObject.GetComponent<InteractableObject>().Interact();
        }
    }

    private void Move()
    {
        float runAnimMultiplier = 1.2f;
        anim.SetFloat("moveSpeed", _rb.velocity.normalized.magnitude * runAnimMultiplier);
        anim.SetFloat("jumpHeight", _rb.velocity.y);
        _rb.AddForce(new Vector3(0, 0, XAxis * moveSpeed));

        if (XAxis > 0.1f)
        {
            isRight = true;
        }
        else if (XAxis < -0.1f)
        {
            isRight = false;
        }

        anim.gameObject.transform.rotation = Quaternion.Euler(0, isRight ? 0 : 180, 0);
    }

    private void Jump()
    {
        if (jump && IsGrounded())
        {
            Debug.Log("Jump");
            _rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }

    public void Teleport(Transform newPosition)
    {
        UIManager.instance.BlackOut();
        mover.position = newPosition.position;

    }

    private bool IsGrounded()
    {
        Vector3 offset = new Vector3(0, 0.1f, 0);
        return Physics.Raycast(mover.position + offset, -Vector3.up, 0.2f);
    }

    public void SetInteractableObject(Collider c)
    {
        interactableObject = c.gameObject;
    }
}
