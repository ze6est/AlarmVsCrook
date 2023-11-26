using System;
using UnityEngine;

public class AlarmDetector : MonoBehaviour
{
    public event Action<bool> TriggerWorked;

    private void OnTriggerEnter(Collider other) => 
        TriggerWorked?.Invoke(true);

    private void OnTriggerExit(Collider other) => 
        TriggerWorked?.Invoke(false);
}