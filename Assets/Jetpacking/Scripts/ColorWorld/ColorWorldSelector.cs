using UnityEngine;
using System.Collections;

public class ColorWorldSelector : MonoBehaviour {

    public ColorSelection color;

	// Use this for initialization
	void Start () {
        ColorSelectionWorldController.OnSelectedColorChange += UpdateWorldObjectForColorChange;
	}

    private void UpdateWorldObjectForColorChange(object sender, ColorSelectionWorldController.ColorSelectionChangeEventArgs e)
    {
        bool isSelected = color == e.Color;
        foreach (var collider in gameObject.GetComponentsInChildren<Collider>())
        {
            collider.enabled = isSelected;
        }
        var localCollider = gameObject.GetComponent<Collider>();
        localCollider.enabled = isSelected;
    }
}
