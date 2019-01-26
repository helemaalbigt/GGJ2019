using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public GameState nextState;
    public abstract bool IsFinished();
}
