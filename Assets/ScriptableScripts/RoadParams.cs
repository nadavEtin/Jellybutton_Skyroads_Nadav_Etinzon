using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/RoadParams", fileName = "RoadParams")]
public class RoadParams : ScriptableObject
{
    [SerializeField] private Object _roadPrefab;

    public Object SingleRoadPrefab => _roadPrefab;
}
