using System;
using UnityEngine;
using System.Collections;

public static class ColorSelectionWorldController {

    private static ColorSelection _selectedColor;
    public static ColorSelection SelectedColor
    {
        get { return _selectedColor; }
        set {
            if (_selectedColor != value)
            {
                _selectedColor = value;
                UpdateSelectedColor();
            }
        }
    }

    private static void UpdateSelectedColor()
    {
        if (OnSelectedColorChange != null)
        {
            OnSelectedColorChange(null, new ColorSelectionChangeEventArgs(SelectedColor));
        }
    }

    public static event EventHandler<ColorSelectionChangeEventArgs> OnSelectedColorChange;

    public class ColorSelectionChangeEventArgs : EventArgs
    {
        public ColorSelectionChangeEventArgs(ColorSelection color)
        {
            Color = color;
        }

        public ColorSelection Color { private set; get; }
    }
}
