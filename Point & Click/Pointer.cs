using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Pointer : MonoBehaviour
{
	LineRenderer line;
	RaycastHit hit;
	IPointable pointable, prevPointable;
	public bool isRunning = true;

#if OCULUS
	static bool Trigger => OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One);
#else
	static bool Trigger => false;
#endif

	void Start()
	{
		line = GetComponent<LineRenderer>();
		line.useWorldSpace = false;
		line.positionCount = 2;
	}

	void Update()
	{
		if (isRunning == false) return;

		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), transform.InverseTransformPoint(hit.point), 0.5001f));
			pointable = hit.transform.GetComponent<IPointable>();
			if (Trigger) pointable?.Click();
		}
		else
		{
			line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), Vector3.forward * 0.2f, 0.5001f));
			pointable = null;
		}

		if (pointable != prevPointable)
		{
			prevPointable?.UnPoint();
			pointable?.Point();
			prevPointable = pointable;
		}
	}
}