using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TweenBase : MonoBehaviour
{
	static TweenBase InstanceCache;

	public static TweenBase Instance
	{
		get
		{
			if (InstanceCache) return InstanceCache;

			var obj = new GameObject("TweenBase");
			return InstanceCache = obj.AddComponent<TweenBase>();
		}
	}
}

public class TweenRunner
{
	public TweenRunner(Coroutine coroutine)
	{
		this.coroutine = coroutine;
	}

	Coroutine coroutine;

	public void Kill()
	{
		TweenBase.Instance.StopCoroutine(coroutine);
		coroutine = null;
	}
}

public static class Tween
{
	public static TweenRunner Scale(this Transform target, float scale, float duration)
	{
		var tween = TweenBase.Instance.StartCoroutine(ScaleCoroutine(target, scale, duration));
		return new TweenRunner(tween);
	}

	static IEnumerator ScaleCoroutine(Transform target, float scale, float duration)
	{
		var initial = target.localScale;
		var startTime = Time.time;
		var targetScale = Vector3.one * scale;
		while (target.localScale != targetScale)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.localScale = Vector3.Lerp(initial, targetScale, lerpTime);
			yield return null;
		}
	}

	public static TweenRunner FadeText(this TextMeshPro target, Color color, float duration)
	{
		var tween = TweenBase.Instance.StartCoroutine(FadeTextCoroutine(target, color, duration));
		return new TweenRunner(tween);
	}

	static IEnumerator FadeTextCoroutine(TextMeshPro target, Color color, float duration)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}
	}

	public static TweenRunner FadeMaterial(this Material target, Color color, float duration)
	{
		var tween = TweenBase.Instance.StartCoroutine(FadeMaterialCoroutine(target, color, duration));
		return new TweenRunner(tween);
	}

	static IEnumerator FadeMaterialCoroutine(Material target, Color color, float duration)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}
	}

	public static TweenRunner Float(float start, float end, float duration, Action<float> action)
	{
		var tween = TweenBase.Instance.StartCoroutine(FloatCoroutine(start, end, duration, action));
		return new TweenRunner(tween);
	}

	static IEnumerator FloatCoroutine(float start, float end, float duration, Action<float> action)
	{
		var medium = start;
		var startTime = Time.time;
		while (medium != end)
		{
			var lerpTime = (Time.time - startTime) / duration;
			medium = Mathf.Lerp(start, end, lerpTime);
			action.Invoke(medium);
			yield return null;
		}
	}
}