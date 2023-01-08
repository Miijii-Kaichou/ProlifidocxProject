using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnimation : MonoBehaviour
{
    [SerializeField]
    Image cursorSprite;

    Alarm blinkAlarm = new Alarm(1);

    bool isVisible = true;
    const float TRANSPARENT = 0f;
    const float BlinkRate = 0.25f;

    Color initColor;

    private void Start()
    {
        initColor = cursorSprite.color;
        
        blinkAlarm.SetFor(BlinkRate, 0, false, timerEvent: () =>
        {
            isVisible = !isVisible;
            cursorSprite.color = isVisible ? initColor : new Color(initColor.r, initColor.g, initColor.b, TRANSPARENT);
        });
    }
}
