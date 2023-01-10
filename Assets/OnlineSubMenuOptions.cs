using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineSubMenuOptions : MonoBehaviour, IItemListCollection
{
    #region Constants
    const string DocxNews       = "DocxNews";
    const string QuickSession   = "Quick Session";
    const string GroupSession   = "Group Session";
    const string TypingTest     = "Typing Test";
    const string DocxHub        = "Docx Hub";
    #endregion

    public ListItem[] ListItems => new ListItem[]
    {
        new(){icon = null, header = DocxNews, content = string.Empty},
        new(){icon = null, header = QuickSession, content = string.Empty},
        new(){icon = null, header = GroupSession, content = string.Empty},
        new(){icon = null, header = TypingTest, content = string.Empty},
        new(){icon = null, header = DocxHub, content = string.Empty}
    };

    public string Tag => "Online Mode";

    public void ApplyList()
    {
        SubMenuSelectionHandler.DisplayList(this);
    }
}
