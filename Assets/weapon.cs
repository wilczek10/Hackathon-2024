using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab pocisku
    public Transform firePoint;    // Punkt, z kt�rego pocisk jest wystrzeliwany
    public float fireInterval = 10f; // Odst�p czasu mi�dzy strza�ami
    private Transform target;       // Automatycznie znajdowany obiekt gracza

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
            Debug.LogError("Nie znaleziono gracza! Upewnij si�, �e gracz ma tag 'Player'.");
        }

        fireTimer = fireInterval;
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
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Laser laserScript = laser.GetComponent<Laser>();

        if (laserScript != null)
        {
            laserScript.SetTarget(target);
        }
    }
}
