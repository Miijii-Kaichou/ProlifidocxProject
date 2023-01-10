using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineSubMenuOptions : MonoBehaviour, IItemListCollection
{
    #region Constants
    const string FreeWrite = "Free Write";
    const string TypingTest = "Typing Test";

    const string FreeWriteDescription = "Inspired by a story? Let loose and do some creative writing." +
        " No time limit.";
    const string TypingTestDescription = "Test how fast you can type with randomly generated sentences, or documents" +
        "that are saved in your vault.";
    #endregion

    public ListItem[] ListItems => new ListItem[] {
            new(){icon = null, header = FreeWrite, content = FreeWriteDescription},
            new(){icon = null, header = TypingTest, content = TypingTestDescription}
    };

    public string Tag => "Offline Mode";

    public void ApplyList()
    {
        SubMenuSelectionHandler.DisplayList(this);
    }
}
