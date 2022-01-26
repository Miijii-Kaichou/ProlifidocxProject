using UnityEngine;

using static Extensions.Convenience;

public class NavigationRoute : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Object[] targetSceneIndices = new UnityEngine.Object[5];

    [SerializeField, Tooltip("This scene will be the scene you want to load when navigation backwards.")]
    UnityEngine.Object fallbackScene;

    public void LoadSceneByIndex(int index)
    {
        targetSceneIndices[index].Load();
    }

    public void LoadFallBackScene()
    {
        fallbackScene.Load();
    }
}