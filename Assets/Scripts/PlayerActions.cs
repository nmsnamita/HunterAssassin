//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] bool hasAttacked;

    [Header("Collectibles")]
    [SerializeField] Transform gems;
    [SerializeField] float spawnRadius;
    [SerializeField] int minGems;
    [SerializeField] int maxGems;

    [Header("Effects")]
    [SerializeField] ParticleSystem bloodParticles;
    [SerializeField] GameObject bloodSplash;
    [SerializeField] AudioClip knifeSFX;
    [SerializeField] GameManage manager;

    Animator animator;
    AudioSource audioSource;
    [SerializeField] GameObject VirtualCamera;
    bool drawCircle = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        VirtualCamera = GameObject.FindGameObjectWithTag("virtualcamera");
    }

    void Update()
    {
        if (hasAttacked)
        {
            animator.SetBool("Attack", false);
            hasAttacked = false;
        }
    }
    private void FixedUpdate() {
        //gunshot();
    }

    IEnumerator SpawnGems(Vector3 spawnLocation)
    {
        yield return null;
        int noOfGems = Random.Range(minGems, maxGems);
        for (int i = 0; i < noOfGems; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0.1f;
            Vector3 spawnPosition = spawnLocation + randomPosition;

            Instantiate(gems, spawnPosition, Quaternion.identity);
        }

    }

    void KnifeStabSound()
    {
        if (knifeSFX != null)
        {
            audioSource.PlayOneShot(knifeSFX);
        }
        drawCircle = !drawCircle;
        VirtualCamera.GetComponent<CameraShake>().ShakeCamera();
        
    }

    void OnDrawGizmos()
    {
        if(drawCircle)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(transform.position, 4f);
        }
        
        
    }
    [SerializeField] LayerMask enemydetection;
    void gunshot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.forward,out hit))
        {
            if(hit.collider.tag == "Enemy")
            {
                Debug.Log("detection was"+ hit.collider.gameObject.name);
            }
           
        }
        Debug.DrawRay(transform.position,Vector3.forward*2,Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!hasAttacked)
            {
                KnifeStabSound();
                bloodParticles.Play();
                bloodSplash.transform.localScale = new Vector3(1f, 1f, 1f);
                Instantiate(bloodSplash, other.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(SpawnGems(other.gameObject.transform.position));
                Destroy(other.gameObject);
                manager.enemycount(other.gameObject,transform.position);

                hasAttacked = true;
            }
            else
            {
                return;
            }
           
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
