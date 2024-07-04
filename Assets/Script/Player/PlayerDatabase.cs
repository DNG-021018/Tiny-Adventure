using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerDatabase : ScriptableObject
{
    public Player_SO[] player_SO;

    public int GetPlayerLength()
    {
        return player_SO.Length;
    }

    public Player_SO GetPlayerIndex(int index)
    {
        return player_SO[index];
    }
}
