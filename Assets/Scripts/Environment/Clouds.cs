using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float moveSpeed = 2f;
    private Material cloudMaterial;

    private void Awake() 
    {
        cloudMaterial = meshRenderer.material;
    }

    private void Update() 
    {
        Vector2 textureOffset = cloudMaterial.mainTextureOffset;
        textureOffset.x += moveSpeed * Time.deltaTime;
        cloudMaterial.mainTextureOffset = textureOffset;
    }

}
