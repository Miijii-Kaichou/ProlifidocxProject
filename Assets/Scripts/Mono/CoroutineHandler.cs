using System.Collections;

/// <summary>
/// This sealed class assisted other classes that do not inherit from Monobehaviour
/// and need to run a coroutine. It's highly recommended to not use this class's methods unless you have to.
/// </summary>
public sealed class CoroutineHandler : Singleton<CoroutineHandler>
{
    public static void Execute(IEnumerator enumerator)
    {
        if (enumerator != null && !IsNull)
            Instance.StartCoroutine(enumerator);
    }

    public static void Halt(IEnumerator enumerator)
    {
        if (enumerator != null && !IsNull)
            Instance.StopCoroutine(enumerator);

        //Otherwise, there's no existing CoroutineEntry that we can stop, or that this object is null.
    }

    public static void ClearRoutines()
    {
        Instance.StopAllCoroutines();
    }
}