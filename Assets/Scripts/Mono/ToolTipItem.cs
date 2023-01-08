using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ToolTipItem
{
    [SerializeField]
    internal TextMeshProUGUI textComponent;

    [TextArea(5,5), SerializeField, Header("Description")]
    internal string description;
}
