using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject target;
    Vector3 offset;

    void Awake()
    {
        offset = new Vector3(0, 0, -7);
    }

    void LateUpdate()
    {
        if (target != null) 
        {
            FollowTarget();
        } 
    }

    public void AssignTarget(GameObject _target)
    {
        target = _target;
    }

    public void FollowTarget()
    {
        transform.position = new Vector3(target.transform.position.x, 15, target.transform.position.z) + offset;
    }
}