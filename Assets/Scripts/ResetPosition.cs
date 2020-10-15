using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public float edgeDistance;

    private void Reset()
    {
        edgeDistance = 200.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= edgeDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - edgeDistance);
        }
    }
}
