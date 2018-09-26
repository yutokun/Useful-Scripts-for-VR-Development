using UnityEngine;

/// <summary>
/// Oculus Touch で動く、シンプルな移動プログラムです。
/// 上下移動を行うには、LIndexTrigger を Joystick Axis、9th Axis で定義する必要があります。
/// 回転を行うには、RThumbstickHorizontal を Joystick Axis、4th Axis で定義する必要があります。
/// </summary>
public class XRLocomotion : MonoBehaviour
{
	[SerializeField] float speed = 0.1f;
	static bool Elevate => Input.GetAxis("LIndexTrigger") > 0.8f;

	void Update()
	{
		var movement = new Vector3();

		if (Elevate)
		{
			movement.y = Input.GetAxis("Vertical");
		}
		else
		{
			movement.x = Input.GetAxis("Horizontal");
			movement.z = Input.GetAxis("Vertical");
		}

		movement *= speed;
		transform.Translate(movement, Space.Self);

		transform.Rotate(0, Input.GetAxis("RThumbstickHorizontal"), 0);
	}
}