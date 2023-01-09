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
    const int MaxCharacterCount = 30270;

    string doxcInfoFormat = "{0} | {1} Words | {2} Characters Left";

    private void Start()
    {
        StartCoroutine(UpdateCycle());
    }

    IEnumerator UpdateCycle()
    {
        float time = 0f;
        const float maxDuration = 1f;

        while(true)
        {
            time += Time.deltaTime;

            if(time > maxDuration)
            {
                time = 0f;
                yield return null;
                charactersLeft =  MaxCharacterCount - inputField.text.Length;
                wordCount = inputField.text.Split(new char[] {' ','\b','\n','\t'}, StringSplitOptions.RemoveEmptyEntries).Length;
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
