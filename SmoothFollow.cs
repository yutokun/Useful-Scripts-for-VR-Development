using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	[SerializeField] Transform followTarget;
	[SerializeField, Range(0f, 1f)] float followFactor = 0.05f;

	[SerializeField, Header("Position")] bool followPosition = true;
	[SerializeField] Vector3 positionOffset = Vector3.forward;
	Vector3 prevPosition;

	[SerializeField, Header("Rotation")] bool followRotation = true;
	[SerializeField] Vector3 eulerOffset;
	Quaternion prevRotation;

	void Start()
	{
		followTarget = followTarget ?? transform.parent ?? null;
	}

	void Update()
	{
		if (followTarget == null) return;

		FollowPosition();
		FollowRotation();
	}

	void FollowPosition()
	{
		if (followPosition == false) return;

		transform.position = Vector3.Lerp(prevPosition, followTarget.position, followFactor);
		prevPosition = transform.position;
		transform.Translate(positionOffset);
	}

	void FollowRotation()
	{
		if (followRotation == false) return;

		// 回転を適用
		transform.rotation = Quaternion.Slerp(prevRotation, followTarget.rotation, followFactor);
		prevRotation = transform.rotation;
		transform.Rotate(eulerOffset);
	}
}