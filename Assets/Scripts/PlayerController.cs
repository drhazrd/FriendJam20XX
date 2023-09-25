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
                }
            }

            // if there is more than one interactableobject, set the closest one
            if (canInteract)
            {
                float closestDistance = Mathf.Infinity;
                Collider closestCol = null;

                foreach (Collider c in interactCols)
                {
                    if (c.CompareTag("Interactable"))
                    {
                        float distance = Vector3.Distance(mover.position, c.transform.position);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestCol = c;
                        }
                    }
                }

                SetInteractableObject(closestCol);
            }
        }
        else
        {
            canInteract = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            anim.SetTrigger("interact");

            InteractableObject io = interactableObject.GetComponent<InteractableObject>();

            if (io)
            {
                io.Interact();
            }
        }
    }

    private void Move()
    {
        float runAnimMultiplier = 1.2f;
        anim.SetFloat("moveSpeed", _rb.velocity.normalized.magnitude * runAnimMultiplier);
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
            anim.SetTrigger("jump");
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
        bool isAirborn = Physics.Raycast(mover.position + offset, -Vector3.up, 0.2f);

        anim.SetBool("isAirborn", isAirborn);

        return isAirborn;
    }

    public void SetInteractableObject(Collider c)
    {
        interactableObject = c.gameObject;

        Item item = c.GetComponent<Item>();

        if (item)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(c.transform.position);
            FindObjectOfType<UIManager>().SetPickupUI(item, screenPosition);
        }
        else
        {
            FindObjectOfType<UIManager>().pickupUI.SetActive(false);
        }
    }
}
