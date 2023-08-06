using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] int gemsAmount = 0;
    [SerializeField] AudioClip gemSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public int GetCurrentGems()
    {
        return gemsAmount;
    }

    // public void ReduceCurrentEnergy(int gemsSpent)
    // {
    //     gemsAmount = gemsAmount - gemsSpent;
    // }

    public void IncreaseGems(int gemvalue)
    {
        gemsAmount += gemvalue;
        audioSource.PlayOneShot(gemSound);
    }


}
