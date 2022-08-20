using UnityEngine;

public class Plate : MonoBehaviour
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
