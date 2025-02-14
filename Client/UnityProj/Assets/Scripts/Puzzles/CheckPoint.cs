﻿using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Puzzle ParrentPuzzle;

    void OnTriggerEnter(Collider o)
    {
        Player p = o.GetComponent<Player>();
        if (p != null)
        {
            ParrentPuzzle.PlayerEnter = true;
            p.IsEnterPuzzleCheckPoint = true;
        }
    }

    void OnTriggerExit(Collider o)
    {
        Player p = o.GetComponent<Player>();
        if (p != null)
        {
            ParrentPuzzle.PlayerEnter = false;
            p.IsEnterPuzzleCheckPoint = false;
        }
    }
}