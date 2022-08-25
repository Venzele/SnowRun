using UnityEngine;

public class SetterDirectionName : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Vector3.forward * int.MaxValue + Vector3.up * int.MinValue);
    }
}