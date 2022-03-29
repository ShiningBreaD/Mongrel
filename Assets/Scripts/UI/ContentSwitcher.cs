using UnityEngine;

public class ContentSwitcher : MonoBehaviour
{
    private Transform parent;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
    }

    public void Open(GameObject content)
    {
        Instantiate(content, parent);
    }

    public void Close(GameObject content)
    {
        Destroy(content);
    }
}
