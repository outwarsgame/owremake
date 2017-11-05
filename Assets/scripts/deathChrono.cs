using UnityEngine;
using System.Collections;

public class deathChrono : MonoBehaviour
{
    public float lifetime;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
