using System;
using UnityEngine;

public class ToggleDrift : MonoBehaviour
{
    public event Action onPlayerEntered;

    public PlayerMovement _playerMovement;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerMovement.applyDrift = true;
            _playerMovement.applyLift = true;

            if (onPlayerEntered != null)
                onPlayerEntered();
        }
    }
}