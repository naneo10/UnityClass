using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeptuneRotate : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 0.2f * Time.deltaTime);
        transform.Rotate(transform.up * 17.0f * Time.deltaTime);
    }
}
