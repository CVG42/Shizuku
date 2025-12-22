using UnityEngine;

[CreateAssetMenu(fileName = "PlantDefinition", menuName = "Grow Shizuku/Plants/PlantDefinition")]
public class PlantDefinition : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _plantName;
    [SerializeField] private string _plantDescription;
    [SerializeField] private PlantStateData[] _plantStates;

    public string Id => _id;
    public string PlantName => _plantName;
    public string PlantDescription => _plantDescription;
    public PlantStateData[] PlantStates => _plantStates;
}
