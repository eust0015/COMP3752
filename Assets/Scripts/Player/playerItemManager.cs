using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerItemManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.current.onItemObtained += AddItem;
        throw new NotImplementedException();
        
    }

    private void AddItem(PlayerItem it)
    {
        //ye
    }


}
