using System.Reflection;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenshotCapturer : MonoBehaviour
{
#if !UNITY_EDITOR
    void Awake (){
        Destroy(this);
    }
#endif

	enum Resolutions
	{
		OculusStoreScreenShot,
		FullHD,
	}

	[SerializeField] Resolutions resolution;

	EditorWindow GetMainGameView()
	{
		var T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		MethodInfo mainGameView = T.GetMethod("GetMainGameView", BindingFlags.NonPublic | BindingFlags.Static);
		var res = mainGameView.Invoke(null, null);
		return (EditorWindow) res;
	}

	void Start()
	{
		var gameView = GetMainGameView();
		var prop = gameView.GetType().GetProperty("currentGameViewSize", BindingFlags.NonPublic | BindingFlags.Instance);
		var gvSize = prop.GetValue(gameView, new object[0]);
		var gvSizeType = gvSize.GetType();

		switch (resolution)
		{
			case Resolutions.OculusStoreScreenShot:
				gvSizeType.GetProperty("width", BindingFlags.Public | BindingFlags.Instance).SetValue(gvSize, 2560);
				gvSizeType.GetProperty("height", BindingFlags.Public | BindingFlags.Instance).SetValue(gvSize, 1440);
				break;

			case Resolutions.FullHD:
				gvSizeType.GetProperty("width", BindingFlags.Public | BindingFlags.Instance).SetValue(gvSize, 1920);
				gvSizeType.GetProperty("height", BindingFlags.Public | BindingFlags.Instance).SetValue(gvSize, 1080);
				break;

			default:
				Debug.Log("This resolution is not implemented.");
				break;
		}
	}

	void Update()
	{
		// TODO : リニア色空間に対応していない（2018.2）
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var now = System.DateTime.Now.ToString().Replace("/", ".").Replace(":", ".");
			var filename = "SS " + now + ".png";
			ScreenCapture.CaptureScreenshot(filename);
		}
	}
}