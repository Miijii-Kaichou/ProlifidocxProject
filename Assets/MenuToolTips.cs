using System;
using TMPro;
using UnityEngine;

public class MenuToolTips : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI header;

    [SerializeField]
    TextMeshProUGUI information;

    ToolTipItem currentItem;

    public void SetDescription(ToolTipItem item)
    {
        currentItem = item;
        if(information.text == null)
        {
            Debug.LogError($"Can't set description without \"{nameof(information)}\" component");
            return;
        }

        if(currentItem == null)
        {
            header.text = string.Empty;
            information.text = string.Empty;
            return;
        }

        header.text = currentItem?.textComponent.text;
        information.text = currentItem?.description;
    }
}
