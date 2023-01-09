using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PageHandler : MonoBehaviour
{

    // The buffer will run based on this
    // input field
    [SerializeField]
    TMP_InputField wordPadField;

    PageSlot currentSlot;
    PageSlot[] availablePageSlots;
    int pageNumber = 0;

    bool isLoadingFromSavedData = false;

    const int MaxLineCount = 44;
    const int MaxPages = 32;

    private void OnEnable()
    {
        Initialize();
    }

    IEnumerable ListenerCycle()
    {
        while (true)
        {
            if (wordPadField.textComponent.textInfo.lineCount > MaxLineCount)
            {
                NextPage();
            }
            yield return null;
        }
    }

    void Initialize()
    {
        availablePageSlots= new PageSlot[MaxPages];
        currentSlot = availablePageSlots[pageNumber];
    }

    void NextPage()
    {
        pageNumber++;
        JumpToPage(pageNumber);
    }

    void PreviousPage()
    {
        pageNumber--;
        JumpToPage(pageNumber);
    }

    void JumpToPage(int pageNumber)
    {
        currentSlot = availablePageSlots[pageNumber];
    }
}