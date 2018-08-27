using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetector : Detector {



    [Header("----Specific to Input Detector----")]
    [Header("Input Detector Watches for Keyboard Input")]
    public KeyCode key;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key)) {
            Trigger();
        }
        DisplayHints();
    }
}
