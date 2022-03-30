using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MessaggingSystem/Messages")]
public class Messages : ScriptableObject
{
    [SerializeField] private List<Branch> _branches;
    private int _index = -1;

    public string NextMessageIn(int branchIndex)
    {
        List<string> messages = _branches[branchIndex].messages;

        if (_index == messages.Count - 1)
            _index = -1;

        return messages[++_index];
    }

    public string PreviousMessageIn(int branchIndex)
    {
        List<string> messages = _branches[branchIndex].messages;

        if (_index <= 0)
            _index = messages.Count;

        return messages[--_index];
    }

    [Serializable]
    private class Branch
    {
        public string name;
        public List<string> messages;
    }
}
