using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    private void Reset()
    {
        speed = 30.0f;
        direction = Vector3.back;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (direction.z * speed * Time.deltaTime));
    }
}
