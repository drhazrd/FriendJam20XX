using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform mover;
    public Animator anim;
    public InputActionAsset inputActions;
    public float moveSpeed;
    public float jumpHeight;
    public float interactRange = 5;
    private float XAxis = 0;
    public Vector3 moveDirection = Vector3.zero;
    private bool jump = false;
    private Rigidbody _rb;
    private bool isRight = true;
    private bool canUse = false;
    private GameObject interactableObject;
    private Collider[] interactCols;

    //actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction useAction;

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        _rb = mover.GetComponent<Rigidbody>();

        moveAction = inputActions.FindActionMap("Gameplay").FindAction("Move");

        inputActions.FindActionMap("Gameplay").FindAction("Jump").performed += ctx => jump = ctx.ReadValue<float>() > 0.1f;
        inputActions.FindActionMap("Gameplay").FindAction("Use").performed += ctx => Use();
    }

    void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
        CheckInteractions();
    }

    private void FixedUpdate()
    {
        Move();
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
                    canUse = true;
                }
            }

            // if there is more than one interactableobject, set the closest one
            if (canUse)
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

                if (closestCol == null)
                {
                    return;
                }

                SetInteractableObject(closestCol);
            }
        }
        else
        {
            canUse = false;
        }
    }

    private void Move()
    {
        float runAnimMultiplier = 1.2f;
        anim.SetFloat("moveSpeed", _rb.velocity.normalized.magnitude * runAnimMultiplier);
        _rb.AddForce(new Vector3(-moveDirection.y * moveSpeed, 0, moveDirection.x * moveSpeed));

        if (moveDirection.x > 0.1f)
        {
            isRight = true;
        }
        else if (moveDirection.x < -0.1f)
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

    private void Use()
    {
        if (canUse)
        {
            anim.SetTrigger("interact");

            InteractableObject io = interactableObject.GetComponent<InteractableObject>();

            if (io)
            {
                io.Interact();
            }
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
        if (!c) return;
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
