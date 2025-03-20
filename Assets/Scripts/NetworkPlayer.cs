using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    private IKTargetFollowVRRig IKTargetFollowVRRig;

    private Transform headVRTarget;
    private Transform leftHandVRTarget;
    private Transform rightHandVRTarget;

    private NetworkVariable<Vector3> headPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> headRotation = new NetworkVariable<Quaternion>();

    private NetworkVariable<Vector3> leftHandPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> leftHandRotation = new NetworkVariable<Quaternion>();

    private NetworkVariable<Vector3> rightHandPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> rightHandRotation = new NetworkVariable<Quaternion>();

    private void Start()
    {
        IKTargetFollowVRRig = GetComponent<IKTargetFollowVRRig>();
    }

    private void Update()
    {
        if (IsOwner)
        {
            headVRTarget = VRRigReference.Singleton.headTarget;
            leftHandVRTarget = VRRigReference.Singleton.leftHandTarget;
            rightHandVRTarget = VRRigReference.Singleton.rightHandTarget;

            IKTargetFollowVRRig.head.vrTarget = headVRTarget;
            IKTargetFollowVRRig.leftHand.vrTarget = leftHandVRTarget;
            IKTargetFollowVRRig.rightHand.vrTarget = rightHandVRTarget;

            UpdateNetworkVariables();
        }
        else
        {
            IKTargetFollowVRRig.enabled = false;
            ApplyNetworkedTransforms();
        }
    }

    private void UpdateNetworkVariables()
    {
        headPosition.Value = headVRTarget.position;
        headRotation.Value = headVRTarget.rotation;

        leftHandPosition.Value = leftHandVRTarget.position;
        leftHandRotation.Value = leftHandVRTarget.rotation;

        rightHandPosition.Value = rightHandVRTarget.position;
        rightHandRotation.Value = rightHandVRTarget.rotation;
    }

    private void ApplyNetworkedTransforms()
    {
        IKTargetFollowVRRig.head.vrTarget.position = headPosition.Value;
        IKTargetFollowVRRig.head.vrTarget.rotation = headRotation.Value;

        IKTargetFollowVRRig.leftHand.vrTarget.position = leftHandPosition.Value;
        IKTargetFollowVRRig.leftHand.vrTarget.rotation = leftHandRotation.Value;

        IKTargetFollowVRRig.rightHand.vrTarget.position = rightHandPosition.Value;
        IKTargetFollowVRRig.rightHand.vrTarget.rotation = rightHandRotation.Value;
    }
}