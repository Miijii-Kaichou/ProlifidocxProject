using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModernItemListButton : MonoBehaviour
{
    [SerializeField]
    Image _thumbnail;

    [SerializeField]
    TextMeshProUGUI _header;

    [SerializeField]
    TextMeshProUGUI _content;
    private ListItem currentListItem;

    public void SetListItem(ListItem item)
    {
        if (item == null) return;
        currentListItem = item;
        ApplyListItem();
    }

    private void ApplyListItem()
    {
        // We'll be using a sprite atlas for this, so expect this line to change
        _thumbnail.sprite = currentListItem.icon;
        _header.text = currentListItem.header;
        _content.text = currentListItem.content;
    }
}
