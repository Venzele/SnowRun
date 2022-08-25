using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    public bool IsBuilt => _renderer.enabled;

    public void Build()
    {
        _renderer.enabled = true;
    }
}
