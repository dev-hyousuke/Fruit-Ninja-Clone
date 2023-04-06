using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GameManager>().Explode();
        }
    }
}