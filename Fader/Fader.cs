using UnityEngine;
using DG.Tweening;

public class Fader : MonoBehaviour
{
	enum Behaviour
	{
		DoNothing,
		FadeIn,
		Faded
	}

	[SerializeField, HideInInspector] Shader shader;
	Material mat;

	public Color color = Color.black;
	public float duration = 4f;
	[SerializeField] Behaviour onAwake;

	static readonly int FadeLevel = Shader.PropertyToID("_FadeLevel");

	void Reset()
	{
		shader = Shader.Find("Hidden/Fader");
	}

	void Awake()
	{
		mat = new Material(shader);
		mat.SetColor("_Color", color);

		switch (onAwake)
		{
			case Behaviour.FadeIn:
				mat.SetFloat(FadeLevel, 1f);
				FadeIn();
				break;

			case Behaviour.Faded:
				mat.SetFloat(FadeLevel, 1f);
				break;
		}
	}

	public void FadeIn()
	{
		mat.DOKill();
		mat.DOFloat(0f, FadeLevel, duration).SetEase(Ease.InCubic);
	}

	public void FadeOut()
	{
		mat.DOKill();
		mat.SetColor("_Color", color);
		mat.DOFloat(1f, FadeLevel, duration).SetEase(Ease.OutCubic);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, mat);
	}
}