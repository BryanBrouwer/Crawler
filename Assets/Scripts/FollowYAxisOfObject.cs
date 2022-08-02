using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowYAxisOfObject : MonoBehaviour
{
    [SerializeField]
    private Transform ObjectToFollow;

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, ObjectToFollow.localPosition.y, transform.localPosition.z);
    }
}
