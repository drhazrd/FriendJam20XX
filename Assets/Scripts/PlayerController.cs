using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform mover;
    public CharacterController controller;
    public Animator anim;
    public InputActionAsset inputActions;
    public float moveSpeed;
    public float jumpHeight;
    public float interactRange = 5;
    public float runAnimationMultiplier;

    private float XAxis = 0;
    private Vector3 moveDirection = Vector3.zero;
    private bool jump = false;
    // private Rigidbody _rb;
    private bool isRight = true;
    private bool canUse = false;
    private GameObject interactableObject;
    private Collider[] interactCols;

    //actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction useAction;
    private UIManager uiManager;

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
        // _rb = mover.GetComponent<Rigidbody>();

        moveAction = inputActions.FindActionMap("Gameplay").FindAction("Move");

        inputActions.FindActionMap("Gameplay").FindAction("Jump").performed += ctx => jump = ctx.ReadValue<float>() > 0.1f;
        inputActions.FindActionMap("Gameplay").FindAction("Use").performed += ctx => Use();

        uiManager = UIManager.instance;
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
                    uiManager.pickupUI.SetActive(false);
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
        anim.SetFloat("moveSpeed", controller.velocity.normalized.magnitude * runAnimationMultiplier);

        Vector3 moveDir = new Vector3(-moveDirection.y, 0, moveDirection.x);

        // rotate mover to face direction of movement
        Vector3 mappedDirection = Vector3.ProjectOnPlane(moveDir, Vector3.up);

        // if movedir is zero, don't rotate
        if (mappedDirection != Vector3.zero)
        {
            Quaternion q1 = mover.rotation;
            Quaternion q2 = Quaternion.LookRotation(mappedDirection);
            mover.rotation = Quaternion.Lerp(q1, q2, 0.1f);
        }

        Vector3 prevPos = controller.transform.position;

        controller.Move(controller.transform.forward * moveSpeed * mappedDirection.magnitude * Time.deltaTime);

        bool isOnGround = MapToFloor();

        // if there is no ground underneath controller, go back to prevPos
        if (!isOnGround)
        {
            controller.transform.position = prevPos;
        }
    }

    private void Jump()
    {
        if (jump && IsGrounded())
        {
            anim.SetTrigger("jump");
            // _rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }

    private bool MapToFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(mover.position, -Vector3.up, out hit, 1.5f))
        {
            mover.position = hit.point;
            return true;
        }
        return false;
    }

    private void Use()
    {
        if (canUse)
        {
            // anim.SetFloat("randomPickupAnim", Mathf.Round(Random.Range(0, 1)));
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
        if (!c)
        {
            uiManager.pickupUI.SetActive(false);
            return;
        }
        interactableObject = c.gameObject;

        Item item = c.GetComponent<Item>();
        InteractableObject io = c.GetComponent<InteractableObject>();

        if (item)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(c.transform.position);
            uiManager.SetPickupUI(item, screenPosition);
            return;
        }
        else if (io)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(c.transform.position);
            uiManager.SetPickupUI(io, screenPosition);
            return;
        }
        else
        {
            uiManager.pickupUI.SetActive(false);
        }
    }
}
