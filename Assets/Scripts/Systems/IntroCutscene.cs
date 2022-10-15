using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class IntroCutscene : MonoBehaviour
{
    private VideoPlayer _v;

    private void Awake()
    {
        _v = GetComponent<VideoPlayer>();
        _v.loopPointReached += OnFinish;
    }

    private void OnFinish(VideoPlayer source)
    {
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("beeHive");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
