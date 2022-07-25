using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bridge : MonoBehaviour
{
    [SerializeField] MeshRenderer _renderer;

    private bool _isBuilt;

    public bool IsBuilt => _isBuilt;

    public void Build()
    {
        _isBuilt = true;
        _renderer.enabled = true;
    }
}
