using System.Collections;
using System.Collections.Generic;
using UI.Statistics;
using UnityEngine;

public class PollenMovement : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject); var pollenHUD = FindObjectOfType<CurrencyHUD>();
            pollenHUD.Currency.IncreaseValue(1);
        }
    }
}
