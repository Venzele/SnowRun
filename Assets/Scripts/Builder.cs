using UnityEngine;
using UnityEngine.Events;

public class Builder : MonoBehaviour
{
    [SerializeField] private SpawnerSnowball _spawnerSnowball;

    private Plate _currentPlate;

    public event UnityAction WentOnBridge;

    public void TryBuild(Player player, LayerMask plate)
    {
        Collider[] Plates = Physics.OverlapSphere(player.transform.position, 0.5f, plate);

        if (Plates.Length > 0)
        {
            for (int i = 0; i < Plates.Length; i++)
            {
                _currentPlate = Plates[i].GetComponent<Plate>();

                if (_currentPlate.IsBuilt == false && _spawnerSnowball.IsSpawn)
                {
                    WentOnBridge?.Invoke();
                    _currentPlate.Build();
                }
            }
        }
    }
}
