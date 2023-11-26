using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmAudio : MonoBehaviour
{
    private const float _maxVolume = 1f;
    private const float _minVolume = 0;

    [SerializeField] private AlarmDetector _detector;
    [SerializeField] private float _speedChangeVolume;

    private AudioSource _audioSource;
    private Coroutine _upChangeVolumeJob;
    private Coroutine _downChangeVolumeJob;
    private bool _isGameEnabled;

    private void OnValidate() =>
        _audioSource = GetComponent<AudioSource>();    

    private void Start()
    {
        _detector.TriggerWorked += OnTriggerWorked;

        _isGameEnabled = true;
        _audioSource.volume = 0;
        _audioSource.Play();
    }

    private void OnTriggerWorked(bool isEnter)
    {
        if(isEnter)
            SwitchCoroutine(out _upChangeVolumeJob, _downChangeVolumeJob, _maxVolume);
        else
            SwitchCoroutine(out _downChangeVolumeJob, _upChangeVolumeJob, _minVolume);
    }

    private void SwitchCoroutine(out Coroutine included, Coroutine switchable, float targetVolume)
    {
        if (switchable != null)
            StopCoroutine(switchable);

        included = StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_isGameEnabled)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedChangeVolume * Time.deltaTime);
            yield return null;
        }
    }
}