using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerItemManager : MonoBehaviour
{
    private void Awake()
    {
        throw new NotImplementedException();
        GameManager.current.onItemObtained += AddItem;
    }

    private void AddItem(PlayerItem it)
    {
        //ye
    }


}
