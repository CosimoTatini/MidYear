using UnityEngine;
using UnityEngine.AI;
using System.Linq;

/// <summary>
/// When is active, the UI arrow find the shortest path to the nearest DeliveryZone via NavMeshPath
/// </summary>
public class DriveNavigationArrow : MonoBehaviour
{
    public RectTransform arrowUI;
    public string deliveryTag = "DeliveryZone";
    private NavMeshPath _path;       
    private Transform _playerTransform;

    void Awake()
    {
        _path = new NavMeshPath();
        _playerTransform = transform;

        this.enabled=false;
        if (arrowUI != null) arrowUI.gameObject.SetActive(false);
    }

    void Update()
    {
        Transform target = GetClosestDeliveryTarget();
        if (target == null)
        {
            arrowUI.gameObject.SetActive(false);
            return;
        }

        arrowUI.gameObject.SetActive(true);

       
        if (NavMesh.CalculatePath(_playerTransform.position, target.position, NavMesh.AllAreas, _path))
        {
            if (_path.corners.Length > 1)
            {
                
                Vector3 nextWaypoint = _path.corners[1];
                RotateArrowTowards(nextWaypoint);
            }
        }
    }

    Transform GetClosestDeliveryTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag(deliveryTag)
            .Where(g => g.GetComponent<Collider>().enabled)
            .Select(g => g.transform);

        if (!targets.Any()) return null;

        return targets.OrderBy(t => Vector3.Distance(_playerTransform.position, t.position)).First();
    }

    void RotateArrowTowards(Vector3 targetPos)
    {
       
        Vector3 direction = targetPos - _playerTransform.position;

     
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        float cameraAngle = Camera.main.transform.eulerAngles.y;

        arrowUI.localRotation = Quaternion.Euler(0, 0, angle - cameraAngle - 90f);
    }
}