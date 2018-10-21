using UnityEngine;
using System.Collections.Generic;

public class SmoothFollow : MonoBehaviour
{
	[SerializeField] Transform followTarget;

	[SerializeField, Header("Position")] bool followPosition = true;
	[SerializeField] int smoothness = 20;
	[SerializeField] Vector3 positionOffset = Vector3.forward;
	List<Vector3> parentPositions = new List<Vector3>();

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

		// 位置を加算
		parentPositions.Add(followTarget.position);
		if (parentPositions.Count > smoothness) parentPositions.RemoveAt(0);

		// 追従対象の平均位置を計算
		var pos = Vector3.zero;
		foreach (var item in parentPositions) pos += item;
		pos /= parentPositions.Count;

		// 位置を適用
		transform.position = pos;
		transform.Translate(positionOffset);
	}

	void FollowRotation()
	{
		if (followRotation == false) return;

		// 回転を適用
		transform.rotation = Quaternion.Slerp(prevRotation, followTarget.rotation, 0.05f);
		transform.Rotate(eulerOffset);

		prevRotation = transform.rotation;
	}

	void OnValidate()
	{
		parentPositions = new List<Vector3>(smoothness);
	}
}