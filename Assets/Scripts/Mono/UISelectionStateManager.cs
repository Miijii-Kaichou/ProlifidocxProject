using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISelectionStateManager : MonoBehaviour
{
    private Dictionary<IGUIElement, bool> _ElementStateCollection = new();
    private IGUIElement _PreviousElement, _CurrentElement;

    [SerializeField]
    int elementsCount = 0;
    internal ToolTipItem cachedToolTip;

    internal bool IsSelected(IGUIElement uiElement)
    {
        if (_ElementStateCollection.ContainsKey(uiElement) == false) return false;
        return _ElementStateCollection[uiElement] == true;
    }

    internal void Select(IGUIElement uiElement)
    {
        _PreviousElement = uiElement == _CurrentElement ? _PreviousElement : _CurrentElement;
        _CurrentElement = uiElement;
        _ElementStateCollection[_CurrentElement] = true;

        if (_PreviousElement == null) return;
        
        _ElementStateCollection[_PreviousElement] = false;
    }

    internal void Include(IGUIElement uiElement)
    {
        _ElementStateCollection.Add(uiElement, false);
        elementsCount = _ElementStateCollection.Count;
    }

    public void InvokeGameQuit()
    {
        Application.Quit(0);
    }
}
