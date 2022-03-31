using UnityEngine;
using TMPro;

public class TextWalker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _placeholder;
    [SerializeField] private Messages _messages;
    [SerializeField] private GameObject[] _choices;

    public void MakeChoice(int branchIndex)
    {
        _placeholder.text = _messages.NextMessageIn(branchIndex);
    }

    private void DisableChoices()
    {
        foreach (GameObject obj in _choices)
            obj.SetActive(false);
    }
}
