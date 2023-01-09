using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopSubMenuOptions : MonoBehaviour, IItemListCollection
{
    public ListItem[] ListItems => new ListItem[0];

    public string Tag => "Co-Op Mode";

    public void ApplyList()
    {
        SubMenuSelectionHandler.DisplayList(this);
    }
}
