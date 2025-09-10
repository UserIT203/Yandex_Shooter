using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    private Animator _animator;
    private CanvasGroup _canvasGroup;
    private AsyncOperation _sceneLoadingOperation;
    private bool _shouldPlayAnimationOpen = false;

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

    public static void SwitchScene(string sceneName)
    {
        Instance._animator.SetTrigger("onOpen");
        Instance._sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
        Instance._sceneLoadingOperation.allowSceneActivation = false;
    }


    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();

        if (_shouldPlayAnimationOpen == true) _animator.SetTrigger("onClose"); 
    }

    public void OnAnimationOver()
    {
        Debug.Log("Scene load");
        _shouldPlayAnimationOpen = true;
        _sceneLoadingOperation.allowSceneActivation = true;
    }
}
