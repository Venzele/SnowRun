using UnityEngine;

public class Snowball : MonoBehaviour
{
    public void TakeScale(float size)
    {
        transform.localScale = new Vector3(size, size, size);
    }
}
