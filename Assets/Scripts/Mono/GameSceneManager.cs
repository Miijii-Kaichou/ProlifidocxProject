using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public static string StagePrepped { get; private set; } = null;
    static Navigation<string> sceneNavigation = new Navigation<string>();
    public static void LoadScene(string name, bool additive = false, bool extendNavigation = false)
    {
        if (extendNavigation)
            sceneNavigation.Stretch(SceneManager.GetActiveScene().name);

        Instance.StartCoroutine(LoadSceneAsync(name, additive));
    }

    static IEnumerator LoadSceneAsync(string name, bool additive)
    {
        AsyncOperation loadingOperation = null;
        try
        {
            loadingOperation = SceneManager.LoadSceneAsync(name, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            loadingOperation.allowSceneActivation = false;
        }
        catch
        {
            LoadPrevious();
            yield break;
        }
        yield return (loadingOperation.progress > 0.99f);
        loadingOperation.allowSceneActivation = true;

    }

    public static void LoadPrevious(int distance = 1)
    {
        string previousStage = sceneNavigation.Condense(distance);
        Instance.StartCoroutine(LoadSceneAsync(previousStage, false));
    }

    internal static void PrepareToLoad(string v)
    {
        StagePrepped = v;
    }

    internal static void Deploy()
    {
        LoadScene(StagePrepped ?? "TITLE");
    }

    public static void ReloadScene()
    {
        LoadScene(sceneNavigation.Current);
    }
}
