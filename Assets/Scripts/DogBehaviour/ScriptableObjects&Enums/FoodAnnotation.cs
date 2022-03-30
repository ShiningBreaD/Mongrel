using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Food")]
public class FoodAnnotation : ScriptableObject
{
    public string Name => name;
    [field: SerializeField] public float Nourishmen { get; private set; }
}
