﻿using UnityEngine;

/// <summary>
/// Change skybox and update IBL by re-baking Reflection Probe.
/// </summary>
public class SkyboxChanger : MonoBehaviour
{
	// Cubemap of Reflection Probe
	[SerializeField] Cubemap map;

	public void ChangeWithIBL(Material newSkybox)
	{
		RenderSettings.skybox = newSkybox;

		// Baking settings
		var camera = new GameObject("CubemapCamera").AddComponent<Camera>();
		camera.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		camera.cullingMask = 0;
		camera.allowHDR = true;

		// Update IBL
		camera.RenderToCubemap(map);
		Destroy(camera.gameObject);
	}
}