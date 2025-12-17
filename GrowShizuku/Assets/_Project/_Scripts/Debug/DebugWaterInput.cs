using Shizuku;
using Shizuku.Managers;
using UnityEngine;

public class DebugWaterInput : MonoBehaviour
{
    private IGrowthSource _plant;

    private void Start()
    {
        _plant = GrowthManager.Source;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _plant.Water();
        }
#endif
    }
}

