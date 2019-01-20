using UnityEngine;
using DG.Tweening;

public class Fader : MonoBehaviour
{
	[SerializeField, HideInInspector] Shader shader;
	Material mat;

	[SerializeField] float fadeTime = 4f;
	[SerializeField] bool fadedOnAwake;

	void Reset()
	{
		shader = Shader.Find("Hidden/Fader");
	}

	void Awake()
	{
		mat = new Material(shader);
		if (fadedOnAwake) mat.SetFloat("_FadeLevel", 1f);
	}

	public void FadeIn()
	{
		mat.DOKill();
		mat.DOFloat(0f, "_FadeLevel", fadeTime).SetEase(Ease.InCubic);
	}

	public void FadeOut()
	{
		mat.DOKill();
		mat.DOFloat(1f, "_FadeLevel", fadeTime).SetEase(Ease.OutCubic);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, mat);
	}
}