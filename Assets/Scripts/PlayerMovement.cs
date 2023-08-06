using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerMoveSpeed = 5f;
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

    string groundTag = "Ground";
    string collectibleTag = "Collectible";
    string enemyTag = "Enemy";
    string UITag = "UI";
    bool walking;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = playerMoveSpeed;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = .25f;
        lineRenderer.endWidth = 0.25f;
        lineRenderer.positionCount = 0;

        animator = GetComponent<Animator>();

        gameManage = FindObjectOfType<GameManage>();

        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasDied && !gameManage.levelCleared)
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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(groundTag) || hit.collider.CompareTag(collectibleTag) && !hit.collider.CompareTag(UITag))
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

    private void ActivateClickMarker()
    {
        clickmarkerPrefab.transform.SetParent(visualObjects);
        clickmarkerPrefab.SetActive(true);
    }

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
