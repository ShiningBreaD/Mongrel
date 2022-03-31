using UnityEngine;

public class ContentSwitcher : MonoBehaviour
{
    public static Canvas MainCanvas;

    private Transform _parent;

    private void Start()
    {
        MainCanvas = FindObjectOfType<Canvas>();
        _parent = GetComponentInParent<Transform>();
    }

    public void OpenMC(GameObject content)
    {
        Instantiate(content, MainCanvas.transform);
    }

    public void Open(GameObject content)
    {
        Instantiate(content, _parent);
    }

    public void Close(GameObject content)
    {
        Destroy(content);
    }
}
