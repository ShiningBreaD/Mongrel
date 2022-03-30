using UnityEngine;

public class ContentSwitcher : MonoBehaviour
{
    private Transform _parent;

    private void Start()
    {
        _parent = GetComponentInParent<Transform>();
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
