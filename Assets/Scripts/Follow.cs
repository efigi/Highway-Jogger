using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform targetTransform;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = targetTransform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targetTransform)
        {
            transform.position = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z - offset.z);
        }
    }
}
