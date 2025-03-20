using UnityEngine;

public class ChairVR : MonoBehaviour
{
    public Transform seatPosition;
    [HideInInspector] public bool isOccupied = false;

    private void OnDrawGizmos()
    {
        if (seatPosition != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(seatPosition.position, 0.1f);
        }
    }
}