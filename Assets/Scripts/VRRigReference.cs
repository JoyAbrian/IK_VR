using UnityEngine;

public class VRRigReference : MonoBehaviour
{
    public static VRRigReference Singleton;

    public Transform root;
    public Transform headTarget;
    public Transform leftHandTarget;
    public Transform rightHandTarget;

    private void Awake()
    {
        Singleton = this;
    }
}
