using UnityEngine;

public class Blade : MonoBehaviour
{
    public Vector3 direction;

    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private bool slicing;

    private Vector3 GetMousePosition() => mainCamera.ScreenToWorldPoint(Input.mousePosition);
    private bool GetMouseButtonDown() => Input.GetMouseButtonDown(0);
    private bool GetMouseButtonUp() => Input.GetMouseButtonUp(0);

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable() => StopSlice();

    private void OnDisable() => StopSlice();

    private void Update() => InputSlice();

    private void InputSlice()
    {
        if (GetMouseButtonDown())
            StartSlice();
        else if (GetMouseButtonUp())
            StopSlice();
        else if (slicing)
            ContinueSlice();
    }

    private void StartSlice()
    {
        Vector3 position = GetMousePosition();
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 newPosition = GetMousePosition();
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}
