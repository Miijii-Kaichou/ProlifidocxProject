using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class ModernToggleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private Image _buttonBackground;

    [SerializeField]
    private bool _isToggledOn = false;

    [SerializeField]
    private int _slideDistance = 40;

    [SerializeField, Header("Background Graphic Color")]
    private Color selected, unselected;

    private Vector2? _originalPosition;
    private Vector2 velocity;

    private void Start()
    {
        StartCoroutine(SlideAnimation());
    }

    IEnumerator SlideAnimation()
    {
        _originalPosition ??= _button.transform.localPosition;
        _buttonBackground.color= unselected;
        while(true)
        {
            var targetVector = new Vector2(_originalPosition.Value.x + _slideDistance, _originalPosition.Value.y);
            var position = (Vector2)_button.transform.localPosition;
            float percentage = (position.x - targetVector.x) / (_originalPosition.Value.x - targetVector.x);
            _buttonBackground.color = Color.Lerp(selected, unselected, percentage);
           
            if (_isToggledOn == true)
            {
                _button.transform.localPosition = Vector2.SmoothDamp(position, targetVector, ref velocity, 0.1f);
                yield return null;
                continue;
            }

            _button.transform.localPosition = Vector2.SmoothDamp(position, _originalPosition.Value, ref velocity, 0.1f);
            yield return null;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isToggledOn = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isToggledOn = true;
    }
}
