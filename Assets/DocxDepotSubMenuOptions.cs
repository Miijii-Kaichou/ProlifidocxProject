using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocxDepotSubMenuOptions : MonoBehaviour, IItemListCollection
{
    #region Constants
    const string Avatars    = "Avatars";
    const string Icons      = "Icons";
    const string Themes     = "Themes";
    const string DocxToken  = "DocxToken";
    const string FreeWrite  = "Free Write";
    #endregion

    public string Tag => "DocxDepot™";

    public ListItem[] ListItems => new ListItem[]
    {
        new(){icon = null, header = Avatars,    content = string.Empty },
        new(){icon = null, header = Icons,      content = string.Empty },
        new(){icon = null, header = Themes,     content = string.Empty },
        new(){icon = null, header = DocxToken,  content = string.Empty },
        new(){icon = null, header = FreeWrite,  content = string.Empty }
    };

    public void ApplyList()
    {
        SubMenuSelectionHandler.DisplayList(this);
    }
}
