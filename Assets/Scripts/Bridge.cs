using UnityEngine;

public class Bridge : MonoBehaviour
{
    private Plate[] plates;
    private int _maxPlates;

    private void Start()
    {
        plates = this.GetComponentsInChildren<Plate>();
        _maxPlates = plates.Length;
    }

    public bool CheckBuild()
    {
        if (plates != null)
        {
            int quantityPlates = 0;

            foreach (var plate in plates)
            {
                if (plate.IsBuilt)
                    quantityPlates++;
            }

            if (quantityPlates == _maxPlates)
                return true;

            return false;
        }

        return true;
    }
}
