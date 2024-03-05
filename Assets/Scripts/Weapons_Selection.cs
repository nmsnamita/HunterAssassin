using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapom Selector", menuName = "ScriptableObjects/Weapons", order = 1)]
public class Weapons_Selection : ScriptableObject
{
    public WeaponLevels[] mylevels; 
    // Start is called before the first frame update
    [System.Serializable]
    public class WeaponLevels
    {
        [Header("Level Name")]
        public string levelName;

        [Header("Weapons Type")]
        public string weapon;
        public weapontype selected_weapon;
    }
    public enum weapontype
    {
        Knife,
        Shotgun

    }
    private void OnValidate()
    {
        AssignLevelNames();
    }
    private void AssignLevelNames()
    {
        if (mylevels != null)
        {
            for (int i = 0; i < mylevels.Length; i++)
            {
                mylevels[i].levelName = "Level " + (i + 1);
            }
        }
    }
}
