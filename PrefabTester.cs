using UnityEngine;

public class PrefabTester : MonoBehaviour
{
	[SerializeField] GameObject[] prefabs;
	GameObject currentInstance;
	int currentIndex;
	int PreviousIndex => currentIndex = (int) Mathf.Repeat(--currentIndex, prefabs.Length);
	int NextIndex => currentIndex = (int) Mathf.Repeat(++currentIndex, prefabs.Length);
	bool spawned;

	[SerializeField] TextMesh hud;

#if OCULUS
	static bool DownNextButton => OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight);
	static bool DownPreviousButton => OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft);
	static bool DownRespawnButton => OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.X);
	static bool DownDeleteButton => OVRInput.GetDown(OVRInput.RawButton.B) || OVRInput.GetDown(OVRInput.RawButton.Y);
	static bool DownIndexTrigger => OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger);
#else
	static bool DownNextButton => false;
	static bool DownPreviousButton => false;
	static bool DownRespawnButton => false;
	static bool DownDeleteButton => false;
	static bool DownIndexTrigger => false;
#endif

	void Start()
	{
		SetTextOnHud(prefabs[currentIndex].name);
	}

	void Update()
	{
		if (DownNextButton)
		{
			SpawnPrefab(NextIndex);
		}
		else if (DownPreviousButton)
		{
			SpawnPrefab(PreviousIndex);
		}
		else if (DownRespawnButton || DownIndexTrigger)
		{
			SpawnPrefab(currentIndex);
		}
		else if (DownDeleteButton)
		{
			if (currentInstance) Destroy(currentInstance);
		}

		if (spawned)
		{
			PlayParticleSurely();
			SetTextOnHud(prefabs[currentIndex].name);
			spawned = false;
		}
	}

	void SpawnPrefab(int index)
	{
		if (prefabs.Length == 0) throw new UnityException("Set prefabs on the inspector.");

		if (currentInstance) Destroy(currentInstance);
		currentInstance = Instantiate(prefabs[index], transform.position, Quaternion.identity);
		spawned = true;
	}

	void PlayParticleSurely()
	{
		var particle = currentInstance.GetComponent<ParticleSystem>();
		if (particle && particle.main.playOnAwake == false) particle.Play();
	}

	void SetTextOnHud(string prefabName)
	{
		var index = currentIndex.ToString() + " / " + prefabs.Length.ToString();
		var text = prefabName + "\n" + index;
		if (hud) hud.text = text;
	}
}