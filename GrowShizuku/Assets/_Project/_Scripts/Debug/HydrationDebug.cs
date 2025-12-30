using Shizuku.Timers;
using UnityEngine;

public class HydrationDebug : MonoBehaviour
{
    [SerializeField] private HydrationTimer _hydrationTimer;

    private void Start()
    {
        _hydrationTimer.OnStateChanged += state =>
        {
            Debug.Log($"Hydration state: {state}");
        };
    }
}
