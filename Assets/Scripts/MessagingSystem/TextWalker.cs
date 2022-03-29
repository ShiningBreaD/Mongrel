using UnityEngine;
using TMPro;

public class TextWalker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI placeholder;
    [SerializeField] private Messages messages;
    private int index = -1;

    public void NextMessage()
    {
        if (index == messages.Length - 1)
            index = -1;

        placeholder.text = messages[++index];
    }

    public void PreviousMessage()
    {
        if (index <= 0)
            index = messages.Length;

        placeholder.text = messages[--index];
    }
}
