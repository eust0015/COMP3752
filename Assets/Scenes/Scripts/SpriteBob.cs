using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteBob : MonoBehaviour
{
    
    [SerializeField] private float heightVariance = 5f;
    private float speed = 0.5f;
    public bool randomize;
    [SerializeField] private float bobSpeed = 1f;

    private float currentHeightVariance;

    private Transform _t;
    // Start is called before the first frame update
    void Start()
    {
        _t = GetComponent<Transform>();

        if (randomize)
        {
            heightVariance += Random.Range(-1f, 1f);;
            bobSpeed += Random.Range(-0.1f, 0.1f);
        }
        currentHeightVariance = heightVariance;
        
        StartCoroutine(HeightBob());
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
        return (Mathf.Sin(x) / currentHeightVariance);
    }
}
