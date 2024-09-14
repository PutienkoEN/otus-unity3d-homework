using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class UnitObject : MonoBehaviour
    {
        [SerializeField] public string unitTypeUid;
        [SerializeField] public int hitPoints;
        [SerializeField] public int speed;
        [SerializeField] public int damage;
    }
}