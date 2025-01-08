using UnityEngine;

public class HeightCalibrate : MonoBehaviour
{
    [SerializeField] private Transform xrCamera; // The XR Camera (Player Head)
    [SerializeField] private float defaultHeight = 1.6f; // Default character height in meters

    public void AdjustCharacterHeight()
    {
        if (xrCamera != null)
        {
            float userHeight = xrCamera.position.y;
            float scaleFactor = userHeight / defaultHeight;

            transform.localScale = new Vector3(
                scaleFactor, scaleFactor, scaleFactor
            );

            Debug.Log($"Player Height: {userHeight:F2} m. Character Scale Adjusted: {scaleFactor:F2}");
        }
    }
}