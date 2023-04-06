using UnityEngine;

public class Juice : MonoBehaviour
{
    private Material material;

    private void Awake() => material = GetComponentInParent<Fruit>().material;

    private void Start() => SetTextureSameAsFruit();

    private void SetTextureSameAsFruit()
        => GetComponent<ParticleSystem>().GetComponent<Renderer>().material = material;
}