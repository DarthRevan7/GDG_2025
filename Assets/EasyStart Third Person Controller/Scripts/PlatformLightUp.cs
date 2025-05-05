using UnityEngine;

public class PlatformLightUp : MonoBehaviour
{
    public Material normalMaterial;
    public Material glowingMaterial;
    public float glowDuration = 5f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = normalMaterial;
    }

    public void Glow()
    {
        StopAllCoroutines();
        StartCoroutine(GlowRoutine());
    }

    private System.Collections.IEnumerator GlowRoutine()
    {
        rend.material = glowingMaterial;
        yield return new WaitForSeconds(glowDuration);
        rend.material = normalMaterial;
    }
}
