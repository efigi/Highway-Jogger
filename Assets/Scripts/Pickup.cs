﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ParticleSystem pickupParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pickupParticle)
            {
                //TODO add particle shiz
                ParticleSystem newParticle = Instantiate(pickupParticle, transform.position, Quaternion.identity);
                newParticle.transform.parent = other.gameObject.transform;
            }
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}
