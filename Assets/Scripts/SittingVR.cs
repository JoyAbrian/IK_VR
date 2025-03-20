using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class SittingVR : MonoBehaviour
{
    public Rig sittingRig;
    public float sitThreshold = 0.5f;
    public bool isSitting = false;
    private Transform currentSeat;
    private Collider chairCollider;

    private Transform xrRig;
    private Transform headTarget;
    private Vector3 lockedPosition;
    private Quaternion lockedRotation;
    private CharacterController characterController;
    private ChairVR currentChair;

    private void Start()
    {
        headTarget = VRRigReference.Singleton.headTarget;
        characterController = GetComponent<CharacterController>();
        xrRig = VRRigReference.Singleton.root;
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
            ChairVR chair = hit.collider.GetComponent<ChairVR>();

            if (chair != null && !chair.isOccupied)
            {
                currentSeat = chair.seatPosition;
                currentChair = chair;
                chairCollider = hit.collider;
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

        if (currentSeat != null && xrRig != null)
        {
            Vector3 headOffset = headTarget.position - xrRig.position;
            lockedPosition = currentSeat.position - headOffset;
            lockedRotation = currentSeat.rotation;

            if (currentChair != null)
            {
                currentChair.isOccupied = true;
            }
        }
    }

    private void StandUp()
    {
        sittingRig.weight = 0;
        isSitting = false;

        if (currentChair != null)
        {
            currentChair.isOccupied = false;
        }

        if (chairCollider != null)
        {
            StartCoroutine(DisableColliderForSeconds(2f));
        }
    }

    private void FixedUpdate()
    {
        if (isSitting && xrRig != null)
        {
            xrRig.position = lockedPosition;
            xrRig.rotation = lockedRotation;
        }
    }

    private IEnumerator DisableColliderForSeconds(float duration)
    {
        chairCollider.enabled = false;
        yield return new WaitForSeconds(duration);
        chairCollider.enabled = true;
    }
}