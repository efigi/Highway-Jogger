using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed;
    public Vector3 rotateAxis;

    private void Reset()
    {
        rotateAxis = Vector3.up;
        rotateSpeed = 90.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(rotateAxis, rotateSpeed * Time.deltaTime);
    }
}
