using UnityEngine;
using TMPro;

public class GetCursorPosition : MonoBehaviour
{
    public TMP_SelectionCaret selectionCaret;
    public TMP_InputField inputField;
    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.ActivateInputField();
        selectionCaret = GetComponentInChildren<TMP_SelectionCaret>();
        selectionCaret.rectTransform.ForceUpdateRectTransforms();
    }

}
