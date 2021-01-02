using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    Vector3 offset;

    void Awake()
    {
        offset = new Vector3(0, 0, -7);
    }

    void LateUpdate()
    {
        if (target != null) 
        {
            FollowPlayer(target);
        } 
    }

    public void FollowPlayer(GameObject target)
    {
        transform.position = new Vector3(target.transform.position.x, 15, target.transform.position.z) + offset;
    }

}
