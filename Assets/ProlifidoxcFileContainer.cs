using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProlifidoxcFileContainer : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    [SerializeField, Header("DocxInfo")]
    TextMeshProUGUI tmp_DocxInfo;

    string documentName = "Untitled";
    int charactersLeft = MaxCharacterCount;
    private int wordCount;
    const int MaxCharacterCount = 7567;

    string doxcInfoFormat = "{0} | {1} Words | {2} Characters Left";
    private bool atMaxCharacterCount => inputField.text.Length >= MaxCharacterCount - 1;

    private void Start()
    {
        StartCoroutine(UpdateCycle());
    }

    IEnumerator UpdateCycle()
    {
        float time = 0f;
        const float maxDuration = 1f;
        char[] separators = new char[] { ' ', '\b', '\n', '\t' };

        while(true)
        {
            time += Time.deltaTime;

            if(time > maxDuration || atMaxCharacterCount)
            {
                time = 0f;
                yield return null;
                charactersLeft =  MaxCharacterCount - inputField.text.Length;
                inputField.text = inputField.text[..(MaxCharacterCount - 1)];
                wordCount = inputField.text.Split(separators, StringSplitOptions.RemoveEmptyEntries).Length;
                tmp_DocxInfo.text = string.Format(doxcInfoFormat, documentName, wordCount, charactersLeft);
                continue;
            }

            time = Input.anyKeyDown ? 0f : time;

            yield return null;
        }
    }

    void LoadData()
    {

    }

    void SaveData()
    {

    }
}
