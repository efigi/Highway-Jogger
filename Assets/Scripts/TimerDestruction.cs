using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestruction : MonoBehaviour
{

    public float timeTillDestruction;

    private void Reset()
    {
        timeTillDestruction = 10.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTillDestruction -= Time.deltaTime;
        if (timeTillDestruction <= 0)
        {
            Destroy(gameObject);
        }
    }
}
