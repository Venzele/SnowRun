using UnityEngine;
using UnityEngine.Events;

public class Builder : MonoBehaviour
{
    [SerializeField] private SpawnerSnowball _spawnerSnowball;

    private Plate _currentPlate;

    public event UnityAction WentOnBridge;

    public void TryBuild(Player player, LayerMask plate)
    {
        Collider[] plates = Physics.OverlapSphere(player.transform.position, 0.5f, plate);

        if (plates.Length > 0)
        {
            for (int i = 0; i < plates.Length; i++)
            {
                _currentPlate = plates[i].GetComponent<Plate>();

                if (_currentPlate.IsBuilt == false && _spawnerSnowball.IsSpawn)
                {
                    WentOnBridge?.Invoke();
                    _currentPlate.Build();
                }
            }
        }
    }
}
