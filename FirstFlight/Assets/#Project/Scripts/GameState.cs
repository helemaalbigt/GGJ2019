using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public event Action onStateEnabled;

    public GameState nextState;
    public abstract bool IsFinished();

    protected void OnEnable()
    {
        if (onStateEnabled != null)
            onStateEnabled();
    }
}
