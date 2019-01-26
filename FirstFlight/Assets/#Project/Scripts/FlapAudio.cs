using UnityEngine;

public class FlapAudio : MonoBehaviour
{
    public AudioSource _source;
    public FlapData _leftFlap;
    public FlapData _rightFlap;

    void Start()
    {
        _leftFlap.onFlap += PlayFlap;
        _rightFlap.onFlap += PlayFlap;
    }

    private void PlayFlap()
    {
        if(_source.isPlaying)
            _source.Stop();

        _source.Play();
    }
}
