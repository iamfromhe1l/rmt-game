using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0, 1000)] private int _maxHealth = 10;
    [SerializeField] public int _health = 10; // Нужно пофиксить инкапсулцию
}
