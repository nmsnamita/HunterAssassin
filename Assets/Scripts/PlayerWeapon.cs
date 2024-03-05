using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEditor;


public class PlayerWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask enemies;
    [SerializeField] Transform startpoint;
    [SerializeField] Weapons_Selection weapons;
    [HideInInspector]public string weapon_name; 
    [SerializeField] GameObject visioncone;
    [SerializeField] GameObject weapons_obj;
    bool validated;
    Animator anime;
    NavMeshAgent navMeshAgent;
    PlayerMovement movement;
    GameObject[] enemy;
    
    
    void Start()
    {
        validated = true;
        anime = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        movement = GetComponent<PlayerMovement>();
        string currentSceneName = SceneManager.GetActiveScene().name;
        // enemy = GameObject.FindGameObjectsWithTag("Enemy");
        // addlocation();
        foreach (Weapons_Selection.WeaponLevels item in weapons.mylevels)
        {
            if (currentSceneName == item.levelName)
            {
                
                weapon_name = item.weapon;
                var something = item.selected_weapon;
                //difficultyViewDistance = levels.enemyViewDistance;
            }
        }
        //enemy = GameObject.FindObjectsOfType<EnemyMovement>();
        validating(weapon_name);
        //checkinginrange();
    }

    private void validating(string chosen)
    {
        if(chosen == "Knife")
        {
            Debug.Log("nothing to be showed here");
            visioncone.SetActive(false);
            weapons_obj.SetActive(false);
            validated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("validation"+ validated);
        if (validated)
        {
            //detection();
            
        }
    }
    private void FixedUpdate() 
    {
        if (weapon_name != "Knife")
        {
            CreateFOV();
        }
        //if(countofenemies !=enemy.Length)
        updatelocation();
    }

    private void detection()
    {
        //Debug.Log("this is being called");
        RaycastHit hit ;
        Debug.DrawRay(startpoint.position,Vector3.forward*2,Color.red);
        //Debug.DrawS
        if(Physics.Raycast(startpoint.position,Vector3.forward,out hit,2f,enemies))
        {
            Debug.Log("detected : "+ hit.collider.name);
            movement.StopMoving();
            //navMeshAgent.Stop();
            //navMeshAgent.SetDestination(transform.position);
            anime.SetBool("Run",false);
            anime.SetBool("detected",true);
        
        }
        else
        {
            anime.SetBool("detected",false);
        }
        
    }
    List<Transform> enemypos = new List<Transform>();
    [SerializeField] int countofenemies;
    void addlocation()
    {
        foreach (GameObject item in enemy)
        {
            enemypos.Add(item.transform);
        }
        countofenemies = enemypos.Count;
    }
    void updatelocation()
    {
        
        //checkdistance();
    }
    float least = 100;
    //GameObject nearest = new GameObject();
    // void checkdistance()
    // {
    //     foreach (Transform item in enemypos)
    //     {
    //         float dist = Vector3.Distance(transform.position,item.position);
    //         if(dist < least)
    //         {
    //             least = dist;
    //             nearest = item.gameObject;
    //         }
    //     }
    //     Debug.Log(" Closest:" + nearest.name);
    // }
    bool canSeePlayer;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] float radius;
    //GameObject fireat = new GameObject();
    private void CreateFOV()
    {
        //bool temp = true;
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);
        Debug.Log("the distance is" + radius);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;
            Debug.Log("the angle is: "+Vector3.Angle(transform.forward, direction));
            if (Vector3.Angle(transform.forward, direction) < 50 / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                Debug.DrawRay(transform.position,direction * distance,Color.red);

                if (!Physics.Raycast(transform.position, direction, distance, obstructionMask))
                {
                    //Debug.Log("detected");
                    canSeePlayer = true;
                    movement.StopMoving();
                    //fireat = target.gameObject;
                    damagetheenemy(target.gameObject);
                    //navMeshAgent.Stop();
                    //navMeshAgent.SetDestination(transform.position);
                    anime.SetBool("Run",false);
                    anime.SetBool("detected",true);
                    //temp = true;
                    
                }
                else
                {
                    canSeePlayer = false;
                    anime.SetBool("detected",false);
                    //temp = false;

                }
            }
            else
            {
                canSeePlayer = false;
                //anime.SetBool("detected",false);
            }
            //Debug.Log("the bool detected is set at "+temp);
        }
        else if (canSeePlayer)
        {
            Invoke("setting_delay",3f);
            // canSeePlayer = false;
            // anime.SetBool("detected",false);
        }
    }
    void setting_delay()
    {
        canSeePlayer = false;
        anime.SetBool("detected",false);
    }

    public void damagetheenemy(GameObject aim)
    {
        Enemy temp = aim.GetComponent<Enemy>();
        temp.DecreaseHealth(10);
    }
    public void donothing()
    {
        Debug.Log("Shooting Sound: pewpewpewpewpew");
    }
    

    void checkinginrange()
    {
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 2f;
        sphereCollider.center = new Vector3(0,0,0);
        sphereCollider.isTrigger= true;

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Vector3 enemypos = other.gameObject.transform.position;
            float angle = Vector3.Angle(enemypos,transform.position);
            float newangle = angle *Mathf.Rad2Deg;
            Debug.Log("the angle generated is "+ newangle);
        }
    }
}
