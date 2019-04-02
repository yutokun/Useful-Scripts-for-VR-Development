using UnityEngine;
using DG.Tweening;

public class Fader : MonoBehaviour
{
	enum Behaviour
	{
		DoNothing,
		FadeInOnAwake,
		FadedOnAwake
	}

	[SerializeField, HideInInspector] Shader shader;
	Material mat;

	[SerializeField] Color color = Color.black;
	[SerializeField] float duration = 4f;
	[SerializeField] Behaviour behaviour;

	static readonly int FadeLevel = Shader.PropertyToID("_FadeLevel");

	void Reset()
	{
		shader = Shader.Find("Hidden/Fader");
	}

	void Awake()
	{
		mat = new Material(shader);
		mat.SetColor("_Color", color);

		switch (behaviour)
		{
			case Behaviour.FadeInOnAwake:
				mat.SetFloat(FadeLevel, 1f);
				FadeIn();
				break;

			case Behaviour.FadedOnAwake:
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