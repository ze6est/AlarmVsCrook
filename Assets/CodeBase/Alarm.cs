using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speedChangeVolume;

    private Coroutine _upChangeVolumeJob;
    private Coroutine _downChangeVolumeJob;
    private bool _isGameEnabled;

    private void OnValidate() => 
        _audioSource = GetComponent<AudioSource>();

    private void Start()
    {
        _isGameEnabled = true;
        _audioSource.volume = 0;
        _audioSource.Play();
    }

    private void OnTriggerEnter(Collider other) =>
        SwitchCoroutine(out _upChangeVolumeJob, _downChangeVolumeJob, 1);

    private void OnTriggerExit(Collider other) =>
        SwitchCoroutine(out _downChangeVolumeJob, _upChangeVolumeJob, 0);

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_isGameEnabled)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedChangeVolume * Time.deltaTime);
            yield return null;
        }        
    }

    private void SwitchCoroutine(out Coroutine included, Coroutine switchable, float targetVolume)
    {
        if (switchable != null)
            StopCoroutine(switchable);

        included = StartCoroutine(ChangeVolume(targetVolume));
    }
}