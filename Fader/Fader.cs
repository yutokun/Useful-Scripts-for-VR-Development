using UnityEngine;

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
	public float duration = 2f;
	[SerializeField] Behaviour onAwake;

	static readonly int FadeLevel = Shader.PropertyToID("_FadeLevel");

	Coroutine tween;

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
		if (tween != null) TweenBase.Instance.StopCoroutine(tween);
		var fadeLevel = mat.GetFloat(FadeLevel);
		tween = Tween.Float(fadeLevel, 0f, duration, n => mat.SetFloat(FadeLevel, n));
	}

	public void FadeOut()
	{
		if (tween != null) TweenBase.Instance.StopCoroutine(tween);
		mat.SetColor("_Color", color);
		var fadeLevel = mat.GetFloat(FadeLevel);
		tween = Tween.Float(fadeLevel, 1f, duration, n => mat.SetFloat(FadeLevel, n));
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, mat);
	}
}