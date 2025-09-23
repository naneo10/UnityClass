using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsRotate : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 18.0f * Time.deltaTime);
        transform.Rotate(transform.up * 30.0f * Time.deltaTime);
    }
}
