using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseNumber : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textToIncrease;
    public float currentValue;
    public float increment;
    public float setTime;
    private float counter;

    private void Reset()
    {
        setTime = 1.0f;
        currentValue = 0.0f;
    }
    void Start()
    {
        counter = setTime;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = setTime;
            currentValue += increment;
            textToIncrease.text = currentValue.ToString();
        }
    }
}
