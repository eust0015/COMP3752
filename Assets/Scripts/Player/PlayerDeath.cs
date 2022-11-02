using Player;
using System.Collections;
using System.Collections.Generic;
using UI.Statistics;
using UnityEngine;
using Audio;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private FMODAudioSource deathSound;
    private void Start()
    {
        health.Health.OnValueZero += CheckDeath;
    }

    private void CheckDeath()
    {
        sceneChanger.current.FadeToBlack("beeHive");
        health.Health.OnValueZero -= CheckDeath;
        deathSound.PlaySound();
    }

}
