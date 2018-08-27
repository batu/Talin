using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* SkillAtom
 *
 * This class is used to represent the mastery level of a certain
 * skill by the player. Its class properties are:
 *
 * name_of_skill - a string corresponding to the name of the skill.
 *		Example names would be "walk", "jump", "shoot".
 * mastery - a float value which should be between 0 and 1. 0 Represents
 *		no experience with the skill, while 1 represents complete mastery
 *		of the skill.
 * ever_used - a boolean value which represents whether the player has ever
 *		used the given skill. It is set to True the first time Exercise is
 *		called on the SkillAtom.
 * time_since_last_use - a float representing the amount of time that has
 * 		passed since the last time that the skill was Exercised.
 *
 */
public class SkillAtom : MonoBehaviour {

    float lastMastery = 0f;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform.parent);
        lastMastery = initialMastery;
    }

    [HideInInspector]
    public bool decaying;

    // Update is called once per frame
    void Update () {

        if(lastMastery > initialMastery) {
            decaying = true;
        } else {
            decaying = false;
        }
        lastMastery = initialMastery;
    }

    [Range(0,1)]
    public float initialMastery = 0.5f;
    public string skillAtomName = "Null";




    public float GetMastery()
    {
        return initialMastery;
    }

    [HideInInspector]
    public bool everUsed = false;
    float lastUsedTime = 0f;
    float lastChangedTime = 0f; // only need to update this when it actually changes, not every frame

    public float GetLastUse()
    {
        return Time.time - lastUsedTime;
    }


    //public System.Collections.ArrayList hints = new System.Collections.ArrayList();

    /* Call this when the player uses the skill.
	 * Parameter: val - the amount by which the player's mastery of the
	 *					skill increases from use.
	 * This function increases the mastery of the skill by the "val" provided
	 * 		to a mximum of 1.
	 */
    public void Excercise(float val)
    {
        everUsed = true;
        //time_since_last_use = 0f;
        lastUsedTime = Time.time;
        lastChangedTime = Time.time;
        initialMastery = Mathf.Max(Mathf.Min(val + initialMastery, 1.0f), 0.0f);
    }

	/* Call this to lower a player's mastery of the skill.
	 * Parameter: val - the amount by which the player's mastery of the
	 *					skill decreases from lack of use.
	 * This function decreases the mastery of the skill by the "val" provided
	 * 		to a minimum of 0.
	 */
    public void Decay(float val)
    {
        lastChangedTime = Time.time;
        initialMastery = Mathf.Max(Mathf.Min(initialMastery - val * Time.deltaTime, 1.0f), 0.0f);
    }
}
