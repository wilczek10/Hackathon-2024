using UnityEngine;

public class bron : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab pocisku
    public Transform firePoint;    // Punkt, z którego pocisk jest wystrzeliwany

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Sprawdzenie, czy gracz klikn¹³ prawym przyciskiem myszy
        if (Input.GetMouseButtonDown(1) && (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "trzymaniebroni" || _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "bieganiezbronia"))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Pobierz pozycjê kursora w przestrzeni œwiata
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ustaw Z na 0, poniewa¿ pracujemy w 2D

        // Tworzenie nowego pocisku
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        pocisk laserScript = laser.GetComponent<pocisk>();

        if (laserScript != null)
        {
            laserScript.SetTarget(mousePosition);
        }
    }
}
