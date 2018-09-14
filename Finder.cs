using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
	/// <summary>
	/// target のすべての子孫からタイプ T のオブジェクトを検索し、リストを返します。
	/// </summary>
	/// <param name="target">子孫を検索するターゲット</param>
	/// <typeparam name="T">検索するタイプ</typeparam>
	/// <returns>発見した T のリスト</returns>
	public static List<T> FindObjectsOfTypeInDescendants<T>(Transform target)
	{
		var allTransforms = new List<Transform>();
		GetAllDescendants(target, ref allTransforms);

		var list = new List<T>();
		foreach (var obj in allTransforms) list.AddRange(obj.GetComponents<T>());

		return list;
	}

	static void GetAllDescendants(Transform target, ref List<Transform> allTransforms)
	{
		if (target.childCount == 0) return;
		foreach (Transform item in target)
		{
			allTransforms.Add(item);
			GetAllDescendants(item, ref allTransforms);
		}
	}
}