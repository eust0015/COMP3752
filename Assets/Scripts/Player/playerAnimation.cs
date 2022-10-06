using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class playerAnimation : MonoBehaviour
{
    [SerializeField] private float playbackSpeed;
    [SerializeField] private float playbackSpeedRun;
    private Animator _a;
    private playerMovement _m;

    [SerializeField] private float heightVariance;
    [SerializeField] private float heightVarianceRun; 
    private float speed = 0.5f;
    [SerializeField] private float bobSpeed;

    private float currentHeightVariance;

    private RectTransform _t;
    private Animator _s;

    private void Start()
    {
        _t = GetComponent<RectTransform>();
        _a = GetComponent<Animator>();
        _m = transform.parent.GetComponent<playerMovement>();
        _s = transform.parent.GetChild(1).GetComponent<Animator>();
        currentHeightVariance = heightVariance;
        
        StartCoroutine(HeightBob());
    }

    private void Update()
    {
        SpeedCheck();
    }

    private void SpeedCheck()
    {
        if (_m.momentum == Vector2.zero)
        {
            _a.speed = playbackSpeed;
            currentHeightVariance = Mathf.Lerp(currentHeightVariance, heightVariance, 2f * Time.deltaTime);
            return;
        }
        currentHeightVariance = Mathf.Lerp(currentHeightVariance, heightVarianceRun, 2f * Time.deltaTime);
        _a.speed = playbackSpeedRun;
    }

    private IEnumerator HeightBob()
    {
        while (true)
        {
            _t.localPosition = Vector3.MoveTowards(_t.localPosition, new Vector3(0, SinWavePos(Time.time * bobSpeed), 0), speed * Time.deltaTime);
            yield return null;
        }
    }

    private float SinWavePos(float x)
    {
        return (Mathf.Sin(x * 1) / currentHeightVariance);
    }

    public IEnumerator AttackAnim()
    {
        _a.Play("PlayerAttack");
        _s.Play("Slash");
        yield return new WaitForSeconds(.375f);
        _s.Play("Empty");
        yield return new WaitForSeconds(.625f);
        _a.Play("Player");
    }
}
