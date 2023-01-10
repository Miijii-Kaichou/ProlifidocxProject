using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSubMenuOptions : MonoBehaviour, IItemListCollection
{
    #region Constants
    const string Avatar         = "Avater";
    const string UserID         = "UserID";
    const string PlayerIcon     = "Player Icon";
    const string FriendsList    = "Friends List";
    const string PromptCreator  = "Prompt Creator";
    const string Records        = "Records";
    const string ClearData      = "Clear Data";
    #endregion

    public string Tag => "Player Data";

    public ListItem[] ListItems => new ListItem[]
    {
        new(){icon = null, header = UserID, content = string.Empty},
        new(){icon = null, header = PlayerIcon, content = string.Empty},
        new(){icon = null, header = FriendsList, content = string.Empty},
        new(){icon = null, header = PromptCreator, content = string.Empty},
        new(){icon = null, header = Records, content = string.Empty},
        new(){icon = null, header = ClearData, content = string.Empty},
    };

    public void ApplyList()
    {
        SubMenuSelectionHandler.DisplayList(this);
    }
}
