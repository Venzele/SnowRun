using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayTrace : MonoBehaviour
{
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private Transform _pointDirection;

    public void TakePoints(Player player)
    {
        _pointSpawn.position = player.transform.position;
        _pointDirection.position = player.transform.position;
    }
}
