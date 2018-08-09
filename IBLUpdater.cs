using UnityEngine;

public class IBLUpdater : MonoBehaviour
{
    //ReflectionProbe のキューブマップを指定します
    [SerializeField] Cubemap map;

    public void Change(Material mat)
    {
        //スカイボックス変更
        RenderSettings.skybox = mat;

        //カメラ作成・任意の設定
        var camera = new GameObject("CubemapCamera").AddComponent<Camera>();
        camera.transform.position = Vector3.zero;
        camera.transform.rotation = Quaternion.identity;
        camera.cullingMask = 0;
        camera.allowHDR = true;

        //IBL 更新
        camera.RenderToCubemap(map);
        Destroy(camera.gameObject);
    }
}