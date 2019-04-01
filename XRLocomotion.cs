using UnityEngine;

public class XRLocomotion : MonoBehaviour
{
	[SerializeField] float speed = 1f;

	new Transform camera;

#if OCULUS
	static Vector2 MoveAxis => OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
	static Vector2 RotateAxis => OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
#else
	static Vector2 MoveAxis => Vector2.zero;
	static Vector2 RotateAxis => Vector2.zero;
#endif

	void Awake()
	{
		camera = Camera.main.transform;
	}

	void Update()
	{
		Move();
		Rotate();
	}

	void Move()
	{
		var movement = new Vector3();

		movement.x += MoveAxis.x;
		movement.z += MoveAxis.y;

		if (Input.GetKey(KeyCode.W)) movement.z += speed;
		if (Input.GetKey(KeyCode.A)) movement.x -= speed;
		if (Input.GetKey(KeyCode.S)) movement.z -= speed;
		if (Input.GetKey(KeyCode.D)) movement.x += speed;

		movement = camera.TransformVector(movement);
		movement.y = 0f;
		movement *= Time.deltaTime;
		transform.Translate(movement, Space.Self);
	}

	void Rotate()
	{
		var rotation = RotateAxis.x;
		transform.Rotate(0f, rotation, 0f);
	}
}