using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    [SerializeField, HideInInspector] Shader shader;
    Material mat;
    [SerializeField] float fadeTime = 0.2f;
    WaitForSeconds wait;

    void Reset()
    {
        shader = Shader.Find("Hidden/Fader");
    }

    void Awake()
    {
        mat = new Material(shader);
        wait = new WaitForSeconds(fadeTime);
    }

    public IEnumerator FadeIn()
    {
        mat.DOFloat(0, "_FadeLevel", fadeTime);
        yield return wait;
    }

    public IEnumerator FadeOut()
    {
        mat.DOFloat(1, "_FadeLevel", fadeTime);
        yield return wait;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
