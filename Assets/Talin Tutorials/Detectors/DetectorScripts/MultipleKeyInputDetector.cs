using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleKeyInputDetector : Detector {



    [Header("----Specific to Input Detector----")]
    [Header("Input Detector Watches for Keyboard Input")]
    public KeyCode[] keys;

	// Update is called once per frame
	void Update () {
        bool pressed = true;
        foreach(KeyCode key in keys) {
            if (!Input.GetKey(key)) {
                pressed = false;
            }
        }
        if (pressed) {
            Trigger();
        }
        DisplayHints();
    }
}
