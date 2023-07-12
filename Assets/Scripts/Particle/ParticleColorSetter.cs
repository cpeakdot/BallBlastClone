using UnityEngine;

public class ParticleColorSetter : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;

    public void UpdateColor(Color color)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            ParticleSystem.MainModule mainModule = particleSystems[i].main;
            mainModule.startColor = color;
        }
    }
}
