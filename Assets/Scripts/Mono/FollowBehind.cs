using UnityEngine;

public class FollowBehind : MonoBehaviour
{
    private static FollowBehind Instance;

    //An object will follow it's target smoothly
    //Mainly for Raven and Maple's Emitters

    [SerializeField]
    float dampTime = 0.15f;

    [SerializeField]
    Vector3 _offset;

    [SerializeField]
    Transform targetTranform;

    [SerializeField]
    Transform _parent;

    Vector3 velocity = Vector3.zero;

    Transform originParent;

    static Transform TargetTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (targetTranform && gameObject.activeInHierarchy)
        {
            Vector3 point = (Vector3)transform.position - new Vector3(_offset.x * Mathf.Sign(targetTranform.localScale.x), _offset.y, _offset.z);
            Vector3 delta = (Vector3)targetTranform.position - new Vector3(point.x, point.y, point.z);
            Vector3 destination = (Vector3)transform.position + delta;

            
            //Z will be changed
            var pos = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

            if(float.IsNaN(pos.x) || float.IsNaN(pos.y) || float.IsNaN(pos.z))
                    return;

            transform.position = pos;
        }
    }

    private void OnEnable()
    {
        originParent = transform.parent;
        transform.SetParent(_parent);
    }

    private void OnDisable()
    {
        if (gameObject.activeInHierarchy)
            transform.SetParent(originParent);
    }

    public void Set(Transform target)
    {
        targetTranform = target;
    }

    public static void SetTarget(Transform target)
    {
        TargetTransform = target;
        Instance.Set(TargetTransform);
    }

    public void UpdateOffset(Vector3 offset)
    {
        _offset = offset;
    }
}
