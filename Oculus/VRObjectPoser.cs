using UnityEngine;

/// <summary>
/// VR Object pose tracker. (The third Oculus Touch)
/// </summary>
public class VRObjectPoser : MonoBehaviour
{
	[SerializeField] Vector3 positionOffset;
	[SerializeField] Quaternion rotationOffset;

	void Update()
	{
		OVRPose p = OVRPlugin.GetNodePose(OVRPlugin.Node.DeviceObjectZero, OVRPlugin.Step.Render).ToOVRPose();

		if (OVRPlugin.GetNodePositionTracked(OVRPlugin.Node.DeviceObjectZero))
		{
			this.transform.position = p.position + positionOffset;
		}

		if (OVRPlugin.GetNodeOrientationTracked(OVRPlugin.Node.DeviceObjectZero))
		{
			this.transform.rotation = p.orientation * rotationOffset;
		}
	}
}