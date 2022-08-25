using UnityEngine;

public class Bridge : MonoBehaviour
{
    private Plate[] _plates;
    private int _maxPlates;

    private void Start()
    {
        _plates = this.GetComponentsInChildren<Plate>();
        _maxPlates = _plates.Length;
    }

    public bool CheckBuild()
    {
        if (_plates != null)
        {
            int quantityPlates = 0;

            foreach (var plate in _plates)
            {
                if (plate.IsBuilt)
                {
                    quantityPlates++;
                }
            }

            if (quantityPlates == _maxPlates)
                return true;

            return false;
        }

        return true;
    }
}
