using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetector : Detector {
    [Header("----Specific to Proximity Detector----")]
    [Header("Proximity Detector triggers when target is within a radius.")]

    public GameObject target;

    public float activationRadius = 5f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }

    void Update() {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= activationRadius) {
            Trigger();
        }
    }
}
