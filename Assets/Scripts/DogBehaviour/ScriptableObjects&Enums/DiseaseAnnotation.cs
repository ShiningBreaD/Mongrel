using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Disease")]
public class DiseaseAnnotation : ScriptableObject
{
    public string Name => name;
    [field: SerializeField] public float Heaviness { get; private set; }
}
