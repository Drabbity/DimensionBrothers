using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    int coins = 0;

    public int GetCoins()
    {
        return coins;
    }

    public void IncrementCoins()
    {
        coins++;
    }

}
