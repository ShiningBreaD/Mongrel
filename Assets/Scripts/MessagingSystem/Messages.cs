using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MessaggingSystem/Messages")]
public class Messages : ScriptableObject
{
    [SerializeField] private List<string> messages;
    public int Length => messages.Count;

    public string this[int index]
    {
        get => messages[index];
    }
}
