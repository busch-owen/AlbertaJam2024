using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "AGOS/Stats/CharacterStats", order = 1)]
public class CharacterStatsSO : ScriptableObject
{
    [field: Header("Gameplay Stats")]
    [field: SerializeField] public float Health { get; private set; }
}