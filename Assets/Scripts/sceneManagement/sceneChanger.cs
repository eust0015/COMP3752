using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneChanger : MonoBehaviour
{
    private AsyncOperation asyncLoad;
    private Image screenCover;
    [SerializeField] private float fadeSpeed;
    
    public static sceneChanger current { get; private set; }

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            current = this;
        }
        
        DontDestroyOnLoad(gameObject);
        screenCover = transform.GetChild(0).GetComponent<Image>();
    }
    
    public void FadeToBlack(string scene)
    {
        //Debug.Log("fade");
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeOut(string scene)
    {
        while (screenCover.color.a < 0.99999f)
        {
            screenCover.color = new Color(screenCover.color.r, screenCover.color.g, screenCover.color.b,
                Mathf.Lerp(screenCover.color.a, 1, fadeSpeed * Time.deltaTime));
            yield return null;
        }
        //Debug.Log("finished in");
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (screenCover.color.a > 0.00001f)
        {
            screenCover.color = new Color(screenCover.color.r, screenCover.color.g, screenCover.color.b,
                Mathf.Lerp(screenCover.color.a, 0, fadeSpeed * Time.deltaTime));
            yield return null;
        }
    }
}
