using UnityEngine;
using System.Collections;

public class ColorWheelSelector : MonoBehaviour {

    public UIController uiController;
    public int[] wheelSelections;
    public Transform[] colorWheelObjects;
    public ColorSelection[] selectedColors;

    public ColorSelection selectedColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < colorWheelObjects.Length; ++i)
	    {
	        bool isSelected = i == wheelSelections[uiController.index];
	        colorWheelObjects[i].GetComponent<Animator>().SetBool("Selected", isSelected);
	        if (isSelected)
            {
	            selectedColor = selectedColors[wheelSelections[uiController.index]];
                ColorSelectionWorldController.SelectedColor = selectedColor;
	        }
	    }
	}
}
