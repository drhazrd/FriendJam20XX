using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteractEvent;
    public string promptText;

    public void Interact()
    {
        OnInteractEvent.Invoke();
    }
}
