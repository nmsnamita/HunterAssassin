using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float timeAfterDrop = 2f;
    [SerializeField] float force;

    Rigidbody rb;
    SphereCollider sphereCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        sphereCollider = GetComponent<SphereCollider>();

        StartCoroutine(CollectAfterTime());
    }

    void Update()
    {
        Invoke("GoToPlayer", timeAfterDrop);
    }

    private void GoToPlayer()
    {

        transform.position =Vector3.MoveTowards(transform.position,player.transform.position,0.3f);
        DestoryOverAll();
    }

    void DestoryOverAll()
    {
        Destroy(gameObject, 10f);
    }

    IEnumerator CollectAfterTime()
    {
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        sphereCollider.enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an object with the "Player" or "Ground" tag
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstruction"))
        {
            GetComponent<SphereCollider>().isTrigger =true;
            GetComponent<BoxCollider>().isTrigger = true;
        }

        
    }
}
