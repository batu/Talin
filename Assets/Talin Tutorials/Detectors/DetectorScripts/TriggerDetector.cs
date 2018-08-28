using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : Detector {
    [Header("----Specific to Proximity Detector----")]
    [Header("Collision Detector triggers when the target collides with the collider.")]
    public GameObject target;

    
    Collider2D parentCollider;
    Collider2D col;
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == target)
        {
            Trigger();
        }
    }

}
