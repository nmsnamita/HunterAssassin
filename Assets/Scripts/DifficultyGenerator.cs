using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficult Generator", menuName = "ScriptableObjects/Difficulty", order = 1)]
public class DifficultyGenerator : ScriptableObject
{
    public Levels[] myLevels;

    [System.Serializable]
    public class Levels
    {
        [Header("Level Name")]
        public string levelName;

        [Header("Enemy Settings")]
        public int enemyDamage;
        public float enemyViewDistance;
        public float enemyFOVAngle;

    }
}



