using UnityEngine;

public class bronboss : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab pocisku
    public Transform firePoint;    // Punkt, z którego pocisk jest wystrzeliwany
    public float fireInterval = 10f; // Odstêp czasu miêdzy strza³ami
    private Transform target;       // Automatycznie znajdowany obiekt gracza
    private Animator animator;
    private float fireTimer;

    void Start()
    {
        // Automatyczne znalezienie gracza po tagu "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("Nie znaleziono gracza! Upewnij siê, ¿e gracz ma tag 'Player'.");
        }

        fireTimer = fireInterval;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval && target != null)
        {
            Fire();
            fireTimer -= fireInterval;
        }
    }

    void Fire()
    {
        GameObject rakieta = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        rakieta laserScript = rakieta.GetComponent<rakieta>();
        Debug.Log("strzal");
        animator.Play("ataklaser");

        if (laserScript != null)
        {
            laserScript.SetTarget(target);
        }
    }
}
