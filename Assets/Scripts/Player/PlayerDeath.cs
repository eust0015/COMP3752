using Player;
using System.Collections;
using System.Collections.Generic;
using UI.Statistics;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    private void Start()
    {
        health.Health.OnValueZero += CheckDeath;
    }

    private void CheckDeath()
    {
        sceneChanger.current.FadeToBlack("beeHive");
        health.Health.OnValueZero -= CheckDeath;
    }

}
