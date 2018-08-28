using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Knowledgebase
 *
 * This class serves as an in-code, queryable collection of the skill atoms
 * for your game.
 *
 */

public class Knowledgebase : MonoBehaviour {

    Dictionary<string, SkillAtom> skillAtoms = new Dictionary<string, SkillAtom>();
    Text visuzalizationText;
    public bool visualize = true;
    [SerializeField]
    Transform visualizerPrefab;

    void CheckForVisualization(Scene scene, LoadSceneMode mode) {
        if (!visuzalizationText) {
            CreateVisualizer();
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform);
		foreach (Transform child in transform)
		{
			SkillAtom skillAtom = child.GetComponent<SkillAtom>();
			if (skillAtom != null)
			{
				skillAtoms.Add(skillAtom.skillAtomName, skillAtom);
			}
		}
        if (!visuzalizationText && visualize) {
            CreateVisualizer();
        }
        SceneManager.sceneLoaded += CheckForVisualization;
    }



    void Update()
    {
        if (visuzalizationText)
        {
            string output = "";
            if (visualize)
            {
                foreach (SkillAtom skill in skillAtoms.Values)
                {
                    string color;
                    if (skill.decaying) {
                        color = ColorUtility.ToHtmlStringRGBA(Color.Lerp(Color.green, Color.red, skill.GetLastUse() / 2f)) ;
                    } else {
                        color = ColorUtility.ToHtmlStringRGBA(Color.Lerp(Color.green, Color.white, skill.GetLastUse() / 2f));
                    }
                    
                    output += string.Format("{0}: <color=#{2}>{1}</color>\n", skill.skillAtomName, skill.GetMastery().ToString("N3"), color);
                }
            }
            visuzalizationText.text = output;
        }
    }


    void CreateVisualizer() {
        Transform vizualizationTextCanvas = Instantiate(visualizerPrefab);
        Transform vizualizationTextUI = vizualizationTextCanvas.GetChild(0);
        visuzalizationText = vizualizationTextUI.GetComponent<Text>();

        CanvasScaler canvasScaler = vizualizationTextCanvas.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

	// --- Class Functions ---

	/* Function called to add a skill atom to the Knowledgebase
	 * Parameters:
	 *	name - a string corresponding to the name of the SkillAtom to be used
	 *		as the key for looking up the SkillAtom
	 *	SA - the SkillAtom to add to the Knowledgebase
	 */
	public void AddSkillAtom(string name, SkillAtom SA) {
        skillAtoms.Add(name, SA);
		// TODO: Maybe catch ArgumentException and do something in the case of
		//		an already existing name instance.
	}

	/* Function called to remove a skill atom to the Knowledgebase
	 * Parameters:
	 *	name - a string corresponding to the name of the SkillAtom and the key
	 *		for looking up that SkillAtom
	 * Description: Removes the skill atom with the provided "name" from the
	 * 		Knowledgebase. Returns a boolean representing whether or not a
	 *		SkillAtom with the given name was actually found and removed.
	 */
	public bool RemoveSkillAtom(string name)
	{
		return skillAtoms.Remove(name);
	}

	/* Function called to retrieve a SkillAtom from the Knowledgebase
	 * Parameters:
	 *	name - a string corresponding to the name of the SkillAtom and the key
	 *		for looking up that SkillAtom
	 * Description: Returns the SkillAtom in the Knowledgebase with the given
	 *		name.
	 */
	public SkillAtom GetSkillAtom(string name) {
        if (skillAtoms.ContainsKey(name))
            return skillAtoms[name];
        return null;
		// TODO: Maybe catch KeyNotFoundException and do something in the case
		//		of not finding a skill atom with requested name.
	}

	/* Function called to query the existance of a SkillAtom in the
	 * Knowledgebase
	 * Parameters:
	 *	name - a string corresponding to the name of the SkillAtom and the key
	 *		for looking up that SkillAtom
	 * Description: Returns true if the Knowledgebase contains a SkillAtom with
	 *		the provided name. Otherwise returns false.
	 */
	public bool ContainsName(string name) {
		return skillAtoms.ContainsKey(name);
	}
}
