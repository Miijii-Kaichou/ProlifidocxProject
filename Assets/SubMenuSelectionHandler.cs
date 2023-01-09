using TMPro;
using UnityEngine;

public class SubMenuSelectionHandler : Singleton<SubMenuSelectionHandler>
{
    [SerializeField, Header("Sub Menu Header")]
    TextMeshProUGUI header;

    static IItemListCollection previousList, currentList;
    static ModernItemListButton[] items;

    private void OnEnable()
    {
        // Prep up and get all children objects.
        items ??= GetComponentsInChildren<ModernItemListButton>();
        DisableAllItems();
    }

    private void DisableAllItems()
    {
        if (items == null) return;
        foreach(var item in items)
        {
            item.gameObject.SetActive(false);
        }
    }

    static void SetHeader()
    {
        Instance.header.text = currentList.Tag;
    }

    public static void DisplayList(IItemListCollection list)
    {
        if (items == null) return;
        if (currentList == list) return;
        previousList = currentList;
        currentList = list;
        SetHeader();
        for(int i = 0; i < items.Length; i++)
        {
            if(i > currentList.ListItems.Length - 1)
            {
                items[i].gameObject.SetActive(false);
                continue;
            }
            items[i].gameObject.SetActive(true);
            items[i].SetListItem(currentList.ListItems[i]);
        }
    }
}
