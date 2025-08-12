using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    public Camera playerCamera;
    private float rotationX = 0f;
    public Rigidbody rb;
    public Weapon gun;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // look
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        if (playerCamera != null) playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        // fire (mouse or touch fallback)
        if (Input.GetButtonDown("Fire1") && gun != null)
        {
            gun.Shoot();
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        Vector3 vel = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);
        rb.velocity = vel;
    }
}
