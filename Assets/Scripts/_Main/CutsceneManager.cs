using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CutsceneManager : MonoBehaviour
{
    [Inject] private GameTimeManager _timeManager;

    [SerializeField] private string _openScene;
    [SerializeField] private List<CutsceneStruct> _cutscenes = new List<CutsceneStruct>();

    private Dictionary<string, GameObject> _cutsceneDataBase = new Dictionary<string, GameObject>();

    private GameObject _currentCutscene;

    private void OnEnable()
    {
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.onSceneLoad += PlayOpenScene;
    }

    private void OnDisable()
    {
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.onSceneLoad -= PlayOpenScene;
    }


    private void Awake()
    {
        InitializedDataBase();

        foreach (var cutscene in _cutsceneDataBase)
        {
            cutscene.Value.SetActive(false);
        }
    }

    private void PlayOpenScene()
    {
        StartCutscene(_openScene);
    }

    private void InitializedDataBase()
    {
        if (_cutscenes.Count <= 0) return;

        foreach (CutsceneStruct cutscene in _cutscenes)
        {
            _cutsceneDataBase.Add(cutscene.Key, cutscene.CutsceneObject);
        }
    }

    public void StartCutscene(string cutsceneKey)
    {
        if(_cutsceneDataBase.ContainsKey(cutsceneKey) == false)
        {
            Debug.LogError("Катсцены нет");
            return;
        }

        if(_currentCutscene != null && _currentCutscene == _cutsceneDataBase[cutsceneKey]) return;

        _timeManager.Pause();

        _currentCutscene = _cutsceneDataBase[cutsceneKey];

        foreach (var cutscene in _cutsceneDataBase)
        {
            cutscene.Value.SetActive(false);
        }

        _currentCutscene.SetActive(true);
    }

    public void EndCutscene()
    {
        if(_currentCutscene != null)
        {
            _currentCutscene.SetActive(false);
            _currentCutscene = null;
        }

        _timeManager.Resume();
        Debug.Log("End Cutscene");
    }
}

[System.Serializable]
public struct CutsceneStruct
{
    public string Key;
    public GameObject CutsceneObject;
}
