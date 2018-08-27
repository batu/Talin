using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is use to represent a Hint that is given to a player in order to
 * 		help them learn to play the game. It is meant to be subclassed and have
 *		its key methods overwritten for different ytpes of hints.
 *
 *	Hints have a skill_atom property used to reference the skill with which
 *		the hint is meant to help. This value is set by default to the value
 *		of the Detector instance attached to the same game object. It can
 *		be changed manually.
 */

// [RequireComponent(typeof(Detector))]
public abstract class Hint : MonoBehaviour {

	// Use this for initialization
	// TODO: Look into ways of making this safe if a subclass overrides Start().
	void Awake () {
		if(skillAtom == null) {
			Detector detector = GetComponent<Detector>();
			if (detector == null) {
				Debug.LogWarning("No detector found for Hint.", transform);
			} else {
				skillAtom = detector.skillAtom;
			}
		}
	}

    [HideInInspector]
	public SkillAtom skillAtom;
	[Range(0,1)]
	public float hintActivationMasteryTreshold = 0.5f;

	/* Function called to see if the preconditions for a the hint is met.
	 * Return Value: True if the conditions to use the hint have been met.
	 *				 False otherwise.
	 *
	 * This function should be overriden in a subclass to check whatever
	 * preconditions need checking for the particular type of Hint.
	 */
	public abstract bool CheckPreconditions();

	/* Function called to have the hint perform whatever operation it does
	 * 		to give the player a hint.
	 *
	 * This function should be overriden in a subclass to give a hint to the
	 * 		player.
	 */
	public abstract void GiveHint();
}
