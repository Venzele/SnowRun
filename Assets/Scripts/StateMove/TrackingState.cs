using UnityEngine;

public class TrackingState : MonoBehaviour, ITargetable
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] LayerMask _floor;

    public Vector3 IndicatePoint()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 target = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _floor))
        {
            target = new Vector3(raycastHit.point.x, 0f, raycastHit.point.z);
        }

        return target;
    }
}
