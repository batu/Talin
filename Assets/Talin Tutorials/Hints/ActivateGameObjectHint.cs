using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObjectHint : Hint {

    [Header("SetActive hint specific variables.")]
    public GameObject gameObjectToBeActivated;

    public override bool CheckPreconditions() {
        return (skillAtom.initialMastery <= hintActivationMasteryTreshold);
    }

    public override void GiveHint() {
        gameObjectToBeActivated.SetActive(true);
    }
}
