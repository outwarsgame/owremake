using UnityEngine;
using System.Collections;

public class shot : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float damage;
    public bool killOnCollision;

    public GameObject deathPrefab;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
        
        if (lifetime > 0)
            StartCoroutine(deathClock());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            return;
        
        if(killOnCollision)
            death();
    }

    void death()
    {
        Instantiate(deathPrefab, rb.position, Quaternion.Euler(0, 0, 0));

        Destroy(gameObject);
    }

    IEnumerator deathClock()
    {
        yield return new WaitForSeconds(lifetime);

        death();
    }
}
