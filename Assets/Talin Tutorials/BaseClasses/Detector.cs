using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO: Create comprehensive comment explanation of this class. */
/*
 *
 */

public class Detector : MonoBehaviour {

    public enum Reactions {excercise, decay};

    public GameObject _KnowledgeBase;
    [HideInInspector]
    public Knowledgebase knowledgebase;

    public SkillAtom skillAtom;

    [Header("What should happen when the detector is triggered?")]
    public Reactions onReaction;

    [Header("Decay or Exercise rate of the mastery when triggered.")]
    public float weight = 0.1f;

    ArrayList hints = new ArrayList();


    // Use this for initialization
    void Awake () {
        knowledgebase = FindObjectOfType<Knowledgebase>() as Knowledgebase;
        if (knowledgebase == null) {
            AddKnowledgeBaseIfDoesntExist();
            Debug.LogWarning("No skill atom knowledgebase found by detector, tried to automatically add the requirements. Please add a knowledgebase to your scene to ensure there is no unexpected behavior.", transform);
        }
        // TODO: Create some way of having these be ordered by priority.
        hints.AddRange(transform.GetComponents<Hint>());

        if (!skillAtom.gameObject.activeInHierarchy) {
            SkillAtom[] allSkillAtoms;
            allSkillAtoms = FindObjectsOfType<SkillAtom>();
            foreach(SkillAtom skill in allSkillAtoms) {
                if(skill.name == skillAtom.name) {
                    skillAtom = skill;
                }
            }
        }
    }

    void AddKnowledgeBaseIfDoesntExist() {
        GameObject knowledgebaseGameObject = Instantiate(_KnowledgeBase) as GameObject;
        knowledgebase = knowledgebaseGameObject.GetComponent<Knowledgebase>();

        GameObject skillAtomGO = new GameObject();
        skillAtomGO.transform.SetParent(knowledgebaseGameObject.transform);
        skillAtomGO.name = skillAtom.name;

        SkillAtom newSA = skillAtomGO.AddComponent<SkillAtom>();
        newSA.skillAtomName = skillAtom.name;
        newSA.initialMastery = skillAtom.GetMastery();

        skillAtom = newSA;
    }




    /* Function called to make the detector check through hints and display
     * The first one with precondition met.
     */
    protected void DisplayHints()
    {
        foreach (Hint hint in hints)
        {
            if (hint.CheckPreconditions())
            {
                hint.GiveHint();
            }
        }
    }



    /* Function called to make the detecor alter any values in database or
     *      do anything else it needs to when the condition is is looking
     *      for is detected.
     *
     *      Can and often should be overwritten in subclasses to do more *          specific things.
     */
    public virtual void Trigger()
    {
        if (Reactions.excercise == onReaction)
        {
            skillAtom.Excercise(weight);
        }
        if (Reactions.decay == onReaction)
        {
            skillAtom.Decay(weight);
        }

        DisplayHints();
    }
}
