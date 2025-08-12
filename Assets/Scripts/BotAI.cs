using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BotAI : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public Weapon weapon;
    public float engageDistance = 12f;
    public float health = 100f;
    public float respawnDelay = 5f;
    public Transform respawnPoint;
    bool alive = true;

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!alive || target == null) return;
        float d = Vector3.Distance(transform.position, target.position);
        if (d > engageDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            agent.isStopped = true;
            transform.LookAt(target.position);
            if (weapon != null) weapon.Shoot();
        }
    }

    public void TakeDamage(float amount)
    {
        if (!alive) return;
        health -= amount;
        if (health <= 0f) Die();
    }

    void Die()
    {
        alive = false;
        var r = GetComponent<Renderer>();
        if (r != null) r.enabled = false;
        foreach (var c in GetComponents<Collider>()) c.enabled = false;
        if (agent != null) agent.isStopped = true;
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        if (respawnPoint != null) transform.position = respawnPoint.position;
        health = 100f;
        alive = true;
        var r = GetComponent<Renderer>();
        if (r != null) r.enabled = true;
        foreach (var c in GetComponents<Collider>()) c.enabled = true;
        if (agent != null) agent.isStopped = false;
    }
}
