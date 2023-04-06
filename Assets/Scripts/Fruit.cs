using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    public Material material;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceEffect;
    private GameManager gameManager;

    public int points = 1;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Blade blade = collision.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {        
        gameManager.IncreaseScore(points);

        DisableWholeFruit();

        EnableSlicedFruit();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void DisableWholeFruit()
    {
        fruitCollider.enabled = false;
        whole.SetActive(false);
    }

    private void EnableSlicedFruit()
    {
        sliced.SetActive(true);
        juiceEffect.Play();
    }
}
