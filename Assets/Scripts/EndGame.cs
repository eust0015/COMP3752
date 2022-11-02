using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class EndGame : MonoBehaviour
{
    void Update()
    {
        sceneChanger.current.FadeToBlack("beeHive");
    }
}
