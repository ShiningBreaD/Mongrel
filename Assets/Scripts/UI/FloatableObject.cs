using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FloatableObject : MonoBehaviour
{
    [SerializeField] protected Transform navigationObject;
    [SerializeField] protected float speedOfReturn;

    protected Vector3 offset;
    protected Vector3 rootedPosition;

    protected bool isTouching;

    private void Start()
    {
        rootedPosition = navigationObject.localPosition;
    }

    private void Update()
    {
        if (!isTouching)
            SetToRootedPosition();
    }

    protected virtual void OnMouseDown()
    {
        isTouching = true;

        offset = navigationObject.position - 
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }

    protected virtual void OnMouseDrag()
    {
        Vector3 desiredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        navigationObject.position = Camera.main.ScreenToWorldPoint(desiredPosition) + offset;
    }

    private void OnMouseUp()
    {
        isTouching = false;
    }

    private void SetToRootedPosition()
    {
        navigationObject.localPosition = Vector3.Lerp(navigationObject.localPosition, rootedPosition, Time.deltaTime * speedOfReturn);
    }
}
