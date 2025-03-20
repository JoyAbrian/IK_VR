using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SittingVR : MonoBehaviour
{
    public Rig sittingRig;
    public float sitThreshold = 0.5f;
    public bool isSitting = false;

    private Transform headTarget;
    private Vector3 lockedPosition;
    private Quaternion lockedRotation;
    private CharacterController characterController;

    private void Start()
    {
        headTarget = VRRigReference.Singleton.headTarget;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isSitting && IsNearSeat())
        {
            SitDown();
        }
        else if (isSitting && IsMoving())
        {
            StandUp();
        }
    }

    private bool IsNearSeat()
    {
        RaycastHit hit;
        if (Physics.Raycast(headTarget.position, Vector3.down, out hit, sitThreshold))
        {
            if (hit.collider.CompareTag("Chair"))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsMoving()
    {
        return characterController.velocity.magnitude > 0.1f;
    }

    private void SitDown()
    {
        sittingRig.weight = 1;
        isSitting = true;

        lockedPosition = transform.position;
        lockedRotation = transform.rotation;
    }

    private void StandUp()
    {
        sittingRig.weight = 0;
        isSitting = false;
    }

    private void FixedUpdate()
    {
        if (isSitting)
        {
            transform.position = lockedPosition;
            transform.rotation = lockedRotation;
        }
    }
}