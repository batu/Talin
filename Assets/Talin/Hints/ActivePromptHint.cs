using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePromptHint : Hint {

    [Header("Activate Prompt Hint specific variables.")]
    public GameObject to_be_activated_UI;


    public override bool CheckPreconditions() {


        if (skillAtom.initialMastery < hintActivationMasteryTreshold) {
            return true; 
        }
        to_be_activated_UI.SetActive(false);
        GameObject UI = GameObject.FindGameObjectWithTag("UI_Blur");
        if(UI != null) {
            UI.SetActive(false);
        }
        return false;
    }

    public override void GiveHint() {
        to_be_activated_UI.SetActive(true);
    }
}
