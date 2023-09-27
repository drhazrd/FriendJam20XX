using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithOffset : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public bool localSpace = false;

    void Update()
    {
        if (localSpace)
        {
            transform.localPosition = followTarget.localPosition + offset;
        }
        else
        {
            transform.position = followTarget.position + offset;
        }
    }
}
