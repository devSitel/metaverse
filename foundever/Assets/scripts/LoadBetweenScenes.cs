using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBetweenScenes : MonoBehaviour
{
    private static string TransitionSceneName = "LoadScene";
    public static LoadBetweenScenes Instance { get; private set; }
    public float CurrentLoadProgress { get => currentLoadProgress; set => currentLoadProgress = value; }

    private float currentLoadProgress = 0;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    private static void CheckForInstance()
    {
        if (!Instance)
        {
            GameObject Loader = new GameObject("SceneLoader");
            Instance = Loader.AddComponent<LoadBetweenScenes>();
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    public static void LoadScene(string SceneName)
    {
        CheckForInstance();
        Instance.StartCoroutine(LoadSceneAsync(SceneName));
    }

    private static IEnumerator LoadSceneAsync(string SceneName)
    {
        Instance.CurrentLoadProgress = 0;
        SceneManager.LoadScene(TransitionSceneName);
        yield return new WaitForSeconds(1f);

        AsyncOperation AsyncOp = SceneManager.LoadSceneAsync(SceneName);
        AsyncOp.allowSceneActivation = false;
        while (!AsyncOp.isDone)
        {
            Instance.CurrentLoadProgress = AsyncOp.progress;
            if (AsyncOp.progress >= 0.9f)
            {
                AsyncOp.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
