using UnityEngine;

public class GroundShaderColor : MonoBehaviour
{
    [SerializeField] MeshRenderer mRenderer;
    [SerializeField] Color baseColor = Color.white;

    MaterialPropertyBlock propertyBlock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        propertyBlock = new();
        propertyBlock.SetColor("_BaseColor", baseColor);
        mRenderer.SetPropertyBlock(propertyBlock);
    }
}
