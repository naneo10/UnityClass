using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercuryRotate : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 140.0f * Time.deltaTime);
        transform.Rotate(transform.up * 60.0f * Time.deltaTime);
    }
}
