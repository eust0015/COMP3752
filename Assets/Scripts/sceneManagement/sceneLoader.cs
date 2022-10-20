using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class sceneLoader : MonoBehaviour
{
    public string scene;
    void OnTriggerEnter2D(Collider2D other)
    {
        sceneChanger.current.FadeToBlack(scene);
    }
}
