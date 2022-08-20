using UnityEngine;

public class WayTrace : MonoBehaviour
{
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private Transform _pointDirection;

    public Transform PointSpawn => _pointSpawn;
    public Transform PointDirection => _pointDirection;

    public void TakePoints(EffectTrace effectTrace)
    {
        _pointSpawn.position = effectTrace.SpawnPoint.position;
        _pointDirection.position = effectTrace.DirectionPoint.position;
    }
}
