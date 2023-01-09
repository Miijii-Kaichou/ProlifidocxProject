using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleModeSubMenuOptions : MonoBehaviour, IItemListCollection
{
    #region Constants
    const string QuickPlay = "Quick Play";
    const string SoloPlay = "Solo Play";
    const string FreeWrite = "Free Write";
    const string TypingTest = "Typing Test";

    const string QuickPlayDescription = "Short on time? Do a quick 2-minute session" +
        "of any randomly generated prompt. Choose between pre-built ones, or one's you've created.";
    const string SoloPlayDescription = "Set up a game with a set time limit and prompt. Test how well you can" +
        "put out content with a set deadline.";
    const string FreeWriteDescription = "Inspired by a story? Let loose and do some creative writing." +
        " No time limit.";
    const string TypingTestDescription = "Test how fast you can type with randomly generated sentences, or documents" +
        "that are saved in your vault.";

    const int Capacity = 4; 
    #endregion

    public ListItem[] ListItems => new ListItem[Capacity] {
            new(){icon = null, header = QuickPlay, content = QuickPlayDescription},
            new(){icon = null, header = SoloPlay, content = SoloPlayDescription },
            new(){icon = null, header = FreeWrite, content = FreeWriteDescription},
            new(){icon = null, header = TypingTest, content = TypingTestDescription}
     };

    public string Tag => "Single Mode";

    public void ApplyList()
    {
        SubMenuSelectionHandler.DisplayList(this);
    }
}
