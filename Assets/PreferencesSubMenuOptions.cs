using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesSubMenuOptions : MonoBehaviour, IItemListCollection
{
    #region Constants
    const string GameLanguage   = "Game Language";
    const string Audio          = "Audio";
    const string Graphics       = "Graphics";
    const string Theme          = "Theme";
    const string LeadCharacter  = "Character";
    const string GameMetrics    = "Game Metrics";
    #endregion

    public string Tag => "Preferences";

    public ListItem[] ListItems => new ListItem[]
    {
        new(){icon = null, header = GameLanguage,   content = string.Empty},
        new(){icon = null, header = Audio,          content = string.Empty},
        new(){icon = null, header = Graphics,       content = string.Empty},
        new(){icon = null, header = Theme,          content = string.Empty},
        new(){icon = null, header = LeadCharacter,  content = string.Empty},
        new(){icon = null, header = GameMetrics,    content = string.Empty}
    };

    public void ApplyList()
    {
        SubMenuSelectionHandler.DisplayList(this);
    }
}
