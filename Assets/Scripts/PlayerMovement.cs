using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float playerMoveSpeed = 5f;
    [SerializeField] RaycastHit hit;
    [SerializeField] Camera cinemachineCamera;
    [SerializeField] private GameObject clickmarkerPrefab;
    [SerializeField] AudioSource footStepAS;
    [SerializeField] Transform visualObjects;
    [SerializeField] Transform targetEnemy;
    public bool hasDied;

    NavMeshAgent navMeshAgent;
    LineRenderer lineRenderer;
    public Animator animator;
    GameManage gameManage;
    AudioSource audioSource;
    ShowSettings showSettings;
    Mesh VisionConeMesh;

    string groundTag = "Ground";
    string collectibleTag = "Collectible";
    string enemyTag = "Enemy";
    bool walking;
    private LayerMask targetMask;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        gameManage = FindObjectOfType<GameManage>();
        showSettings = FindObjectOfType<ShowSettings>();

        lineRenderer.positionCount = 0;
        navMeshAgent.speed = playerMoveSpeed;
        //int currentlevel = int.Parse(SceneManager.GetActiveScene().name);
        //Debug.Log("the current level is "+ currentlevel);
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && !hasDied && !gameManage.levelCleared)
        {
            ClickToMove();
        }

        else if (targetEnemy != null)
        {
            MoveTowardsEnemy();
        }

        if (Vector3.Distance(navMeshAgent.destination, transform.position) <= navMeshAgent.stoppingDistance)
        {
            StopMoving();
        }

        else if (navMeshAgent.hasPath)
        {
            DisplayPath();
        }
    }

    public void StopMoving()
    {
        clickmarkerPrefab.transform.SetParent(transform);
        clickmarkerPrefab.SetActive(false);
        walking = true;
        animator.SetBool("Run", false);
        StopFootstepSound();
        navMeshAgent.SetDestination(transform.position);
    }

    private void ClickToMove()
    {
        Ray ray = cinemachineCamera.ScreenPointToRay(Input.mousePosition);

        if (!showSettings.isPaused)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag) || hit.collider.CompareTag(collectibleTag))
                {
                    animator.SetBool("Run", true);
                    ActivateClickMarker();
                    clickmarkerPrefab.transform.position = hit.point;
                    navMeshAgent.SetDestination(hit.point);
                    if (walking)
                    {
                        PlayFootstepSound();
                    }
                }
                if (hit.collider.CompareTag(enemyTag))
                {
                    targetEnemy = hit.collider.transform;
                }
            }
        }
    }

    private void MoveTowardsEnemy()
    {
        ActivateClickMarker();
        clickmarkerPrefab.transform.position = targetEnemy.position;
        animator.SetBool("Run", true);
        if (walking)
        {
            PlayFootstepSound();
        }
        navMeshAgent.SetDestination(targetEnemy.position);
    }
    public void StopMovement()
    {
        navMeshAgent.isStopped = true;
    }

    private void ActivateClickMarker()
    {
        clickmarkerPrefab.transform.SetParent(visualObjects);
        clickmarkerPrefab.SetActive(true);
    }
        int VisionConeResolution = 120;
        float difficultyFOV = 120f;
        float difficultyViewDistance = 5f;
        [SerializeField] LayerMask VisionObstructingLayer;
        MeshFilter MeshFilter_;
    // void DrawVisionCone()
    // {
    //     int[] triangles = new int[(VisionConeResolution - 1) * 3];
    //     Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
    //     Vertices[0] = Vector3.zero;
    //     float Currentangle = -difficultyFOV / 2;
    //     float angleIcrement = difficultyFOV / (VisionConeResolution - 1);
    //     float Sine;
    //     float Cosine;

    //     for (int i = 0; i < VisionConeResolution; i++)
    //     {
    //         Sine = Mathf.Sin(Currentangle);
    //         Cosine = Mathf.Cos(Currentangle);
    //         Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
    //         Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
    //         if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, difficultyViewDistance, VisionObstructingLayer))
    //         {
    //             Vertices[i + 1] = VertForward * hit.distance;
    //         }
    //         else
    //         {
    //             Vertices[i + 1] = VertForward * difficultyViewDistance;
    //         }


    //         Currentangle += angleIcrement;
    //     }
    //     for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
    //     {
    //         triangles[i] = 0;
    //         triangles[i + 1] = j + 1;
    //         triangles[i + 2] = j + 2;
    //     }
    //     VisionConeMesh.Clear();
    //     VisionConeMesh.vertices = Vertices;
    //     VisionConeMesh.triangles = triangles;
    //     MeshFilter_.mesh = VisionConeMesh;
    // }

    void DisplayPath()
    {
        lineRenderer.positionCount = navMeshAgent.path.corners.Length;
        lineRenderer.SetPosition(0, transform.position);

        if (navMeshAgent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < navMeshAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(navMeshAgent.path.corners[i].x, navMeshAgent.path.corners[i].y, navMeshAgent.path.corners[i].z);
            lineRenderer.SetPosition(i, pointPosition);
        }
    }

    void PlayFootstepSound()
    {
        footStepAS.enabled = true;
    }

    void StopFootstepSound()
    {
        footStepAS.enabled = false;
    }
}
