using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    private Animator _animator;
    private CanvasGroup _canvasGroup;
    private AsyncOperation _sceneLoadingOperation;
    private bool _shouldPlayAnimationOpen = false;
    private bool _hasLoadScene = false;

    public event Action onSceneLoad;

    #region Singleton
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private void OnLevelWasLoaded(int level)
    {
        if (_shouldPlayAnimationOpen == true) _animator.SetTrigger("onClose");
    }

    public static void SwitchScene(string sceneName)
    {
        if (Instance._hasLoadScene == true) return;

        Instance._animator.SetTrigger("onOpen");
        Instance._sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
        Instance._sceneLoadingOperation.allowSceneActivation = false;
        Instance._hasLoadScene = true;
    }

    public static void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SwitchScene(currentSceneName);
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
    }

    public void OnAnimationOver()
    {
        Debug.Log("Scene load");
        _shouldPlayAnimationOpen = true;
        _sceneLoadingOperation.allowSceneActivation = true;
        _hasLoadScene = false;
    }

    public void OnOpenNewScene() => onSceneLoad?.Invoke();
}
