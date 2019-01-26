using UnityEngine;

public class ToggleDrift : MonoBehaviour
{
    public PlayerMovement _playerMovement;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered " + other.gameObject.name + " with layer " + other.gameObject.layer);

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            _playerMovement.applyDrift = true;
    }
}
