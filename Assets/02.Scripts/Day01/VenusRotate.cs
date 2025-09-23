using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusRotate : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, 43.0f * Time.deltaTime);
        transform.Rotate(transform.up * 40 * Time.deltaTime);
    }
}
