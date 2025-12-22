using Shizuku.Managers;
using UnityEngine;
using UnityEngine.UI;

public class WaterButton : MonoBehaviour
{
    private Button _waterButton;

    private void Awake()
    {
        _waterButton = GetComponent<Button>();
    }

    private void Start()
    {
        _waterButton.onClick.AddListener(WaterPlant);
    }

    private void WaterPlant()
    {
        GrowthManager.Source.Water();
    }
}
