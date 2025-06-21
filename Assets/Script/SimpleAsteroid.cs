using UnityEngine;

public class SimpleAsteroid : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float rotateSpeed = 50f;

    [Header("Lifetime Respawn")]
    public float lifetime = 8f;
    public float respawnRadiusFromPlayer = 6f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Transform player;
    private float lifetimeTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomMovement();
        lifetimeTimer = lifetime;
    }

    void Update()
    {
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0f)
        {
            RespawnNearPlayer();
            lifetimeTimer = lifetime;
        }

        WrapAroundScreen();
    }

    void WrapAroundScreen()
    {
        Vector3 pos = transform.position;
        Vector3 screenPos = Camera.main.WorldToViewportPoint(pos);

        if (screenPos.x < -0.1f || screenPos.x > 1.1f) pos.x = -pos.x;
        if (screenPos.y < -0.1f || screenPos.y > 1.1f) pos.y = -pos.y;

        transform.position = pos;
    }

    void SetRandomMovement()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movement = new Vector2(randomX, randomY).normalized;
        rb.linearVelocity = movement * moveSpeed;
        rb.angularVelocity = Random.Range(-rotateSpeed, rotateSpeed);
    }

    void RespawnNearPlayer()
    {
        if (player == null) return;
        Vector2 offset = Random.insideUnitCircle.normalized * respawnRadiusFromPlayer;
        transform.position = (Vector2)player.position + offset;
        SetRandomMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile") || other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            NotifySpawnerDestroyed();
            Destroy(gameObject);
        }
    }
    void NotifySpawnerDestroyed()
    {
        SimpleAsteroidSpawner spawner = FindObjectOfType<SimpleAsteroidSpawner>();
        if (spawner != null)
        {
            spawner.NotifyAsteroidDestroyed(gameObject);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                RespawnNearPlayer();
                lifetimeTimer = lifetime;
            }
        }
    }
}