using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class ModernToggleButton : MonoBehaviour, IGUIElement
{
    [Header("Primitve Data")]
    [SerializeField]
    private bool _isToggledOn = false;

    [SerializeField]
    private int _slideDistance = 40;

    [Header("Graphics")]
    [SerializeField]
    private Button _button;

    [SerializeField]
    private Image _buttonBackground;

    [SerializeField]
    private AnimaticIcon icon;

    [SerializeField, Header("Background Graphic Color")]
    private Color selected, unselected;

    [Header("Tool Tip Info")]
    [SerializeField]
    MenuToolTips menuToolTipControl;

    [SerializeField]
    ToolTipItem toolTipInfo;

    private Vector2? _originalPosition;
    private Vector2 velocity;
    private UISelectionStateManager localUISelectionManager;

    public bool IsSelected
    {
        get
        {
            return localUISelectionManager.IsSelected(this);
        }
        set
        {
            localUISelectionManager.Select(this);
            localUISelectionManager.cachedToolTip = toolTipInfo;
        }
    }

    private void Start()
    {
        localUISelectionManager ??= GetComponentInParent<UISelectionStateManager>();
        Physics.queriesHitTriggers = true;
        localUISelectionManager.Include(this);
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

            if (_isToggledOn == true || IsSelected)
            {
                _button.transform.localPosition = Vector2.SmoothDamp(position, targetVector, ref velocity, 0.1f);
                yield return null;
                continue;
            }
            _button.transform.localPosition = Vector2.SmoothDamp(position, _originalPosition.Value, ref velocity, 0.1f);
            yield return null;
        }
    }

    public void OnSelect()
    {
        IsSelected = true;
    }

    void OnMouseExit()
    {
        _isToggledOn = false;
        menuToolTipControl.SetDescription(localUISelectionManager.cachedToolTip);
    }

    private void OnMouseEnter()
    {
        menuToolTipControl.SetDescription(toolTipInfo);
    }

    void OnMouseOver()
    {
        _isToggledOn = true;
    }
}
