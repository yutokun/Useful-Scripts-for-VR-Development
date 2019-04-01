using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class Alignment
{
	[MenuItem("Tools/Alignment/Align Local Y")]
	public static void AlignLocalY()
	{
		var transforms = new List<Transform>(Selection.transforms);

		//localPosition.y が大きい順に並べ替え
		transforms.Sort((one, two) => (one.localPosition.y - two.localPosition.y) < 0 ? 1 : -1);

		var highest = transforms.Max(tr => tr.localPosition.y);
		var lowest = transforms.Min(tr => tr.localPosition.y);
		var interval = (highest - lowest) / (transforms.Count - 1);

		for (var i = 0; i < transforms.Count; i++)
		{
			var pos = transforms[i].localPosition;
			pos.y = highest - (interval * i);
			transforms[i].localPosition = pos;
		}
	}
}