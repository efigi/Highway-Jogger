using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public enum States { Running, Jumping, Sliding, Hurt, Dead };
    public States state;

    private void Reset()
    {
        state = States.Running;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
