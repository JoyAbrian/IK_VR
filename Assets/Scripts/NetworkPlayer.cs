using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    private IKTargetFollowVRRig IKTargetFollowVRRig;

    private Transform headVRTarget;
    private Transform leftHandVRTarget;
    private Transform rightHandVRTarget;

    private void Start()
    {
        IKTargetFollowVRRig = GetComponent<IKTargetFollowVRRig>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        headVRTarget = VRRigReference.Singleton.headTarget;
        leftHandVRTarget = VRRigReference.Singleton.leftHandTarget;
        rightHandVRTarget = VRRigReference.Singleton.rightHandTarget;

        IKTargetFollowVRRig.head.vrTarget = headVRTarget;
        IKTargetFollowVRRig.leftHand.vrTarget = leftHandVRTarget;
        IKTargetFollowVRRig.rightHand.vrTarget = rightHandVRTarget;
    }
}
