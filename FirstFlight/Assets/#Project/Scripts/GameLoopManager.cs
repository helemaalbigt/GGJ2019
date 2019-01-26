using System.Collections;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public GameState _startState;

    void Start()
    {
        StartCoroutine(DoState(_startState));
    }

    IEnumerator DoState(GameState state)
    {
        state.gameObject.SetActive(true);

        while (!state.IsFinished())
            yield return null;

        state.gameObject.SetActive(false);

        StartCoroutine(DoState(state.nextState));
    }
}
