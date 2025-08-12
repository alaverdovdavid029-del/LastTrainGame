using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera cam;
    public float damage = 30f;
    public float range = 120f;
    public float fireRate = 0.25f;
    public ParticleSystem muzzle;
    float nextFire = 0f;

    void Start()
    {
        if (cam == null && Camera.main != null) cam = Camera.main;
    }

    public void Shoot()
    {
        if (Time.time < nextFire) return;
        nextFire = Time.time + fireRate;

        if (muzzle != null) muzzle.Play();

        if (cam == null) return;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            BotAI bot = hit.collider.GetComponent<BotAI>();
            if (bot != null)
            {
                bot.TakeDamage(damage);
            }
        }
    }
}
