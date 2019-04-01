﻿using System.Collections;
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

public static class Tween
{
	public static Coroutine Scale(this Transform target, float scale, float duration)
	{
		return TweenBase.Instance.StartCoroutine(ScaleCoroutine(target, scale, duration));
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

	public static Coroutine FadeText(this TextMeshPro target, Color color, float duration)
	{
		return TweenBase.Instance.StartCoroutine(FadeTextCoroutine(target, color, duration));
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

	public static Coroutine FadeMaterial(this Material target, Color color, float duration)
	{
		return TweenBase.Instance.StartCoroutine(FadeMaterialCoroutine(target, color, duration));
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
}