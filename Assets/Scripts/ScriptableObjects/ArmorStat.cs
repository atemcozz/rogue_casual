using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObjects/Armor")]
public class ArmorStat : ItemStat
{
    enum ArmorPart{Helmet, Breastplate, Boots}
    [SerializeField]ArmorPart armorPart;
    [SerializeField] int baseArmor;
    [SerializeField]int damageBonus,lifeBonus, manaBonus, criticalBonus,attackSpeedBonus;
}
