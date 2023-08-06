using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    [SerializeField] Transform doorTransform;
    [SerializeField] float openAngle = 90f;
    [SerializeField] float openTime;
    [SerializeField] float closeTime;
    [SerializeField] NavMeshAgent navmeshagent;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isOpening;
    private bool isClosing;
    private float startTime;

    void Start()
    {
        initialRotation = doorTransform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void Update()
    {
        if (isOpening)
        {
            float t = (Time.time - startTime) / openTime;
            doorTransform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

            if (t >= 1f)
            {
                isOpening = false;
                navmeshagent.enabled = true;
            }
        }
        else if (isClosing)
        {
            float t = (Time.time - startTime) / closeTime;
            doorTransform.rotation = Quaternion.Lerp(targetRotation, initialRotation, t);

            if (t >= 1f)
            {
                isClosing = false;
            }
        }
    }

    public void OpenDoor()
    {
        if (!isOpening && !isClosing)
        {
            isOpening = true;
            startTime = Time.time;
            // navmeshagent.enabled = false;
            FindObjectOfType<PlayerMovement>().StopMoving();
        }
    }

    public void CloseDoor()
    {
        if (!isClosing && !isOpening)
        {
            isClosing = true;
            startTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            CloseDoor();
        }
    }
}
