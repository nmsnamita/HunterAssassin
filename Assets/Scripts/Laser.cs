using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damageAmount = 10;
    [SerializeField] GameObject laserBeam;
    [SerializeField] float laserTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(OnAndOff());
    }

    IEnumerator OnAndOff()
    {
        laserBeam.SetActive(true);
        yield return new WaitForSeconds(laserTimer);
        laserBeam.SetActive(false);
        yield return new WaitForSeconds(laserTimer);
    }
}
