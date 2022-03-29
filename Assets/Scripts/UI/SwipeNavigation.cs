using UnityEngine;

public class SwipeNavigation : MonoBehaviour
{
    [SerializeField] private Transform navigationObject;
    [SerializeField] private float speedOfReturn;

    private Vector3 offset;
    [SerializeField] private Vector3 rootedPosition;
    private bool isCardDragging = false;

    private void Start()
    {
        rootedPosition = navigationObject.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rootedPosition = 
            new Vector3(collision.transform.localPosition.x * -1, rootedPosition.y, rootedPosition.z);
    }

    private void Update()
    {
        if (!isCardDragging)
            SetToRootedPosition();
    }

    private void OnMouseDown()
    {
        isCardDragging = true;

        offset = navigationObject.position - 
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, navigationObject.position.y, 10f));
    }

    private void OnMouseDrag()
    {
        Vector3 desiredPosition = new Vector3(Input.mousePosition.x, navigationObject.position.y, 10f);
        navigationObject.position = Camera.main.ScreenToWorldPoint(desiredPosition) + offset;
    }

    private void OnMouseUp()
    {
        isCardDragging = false;
    }

    private void SetToRootedPosition()
    {
        navigationObject.localPosition = Vector3.Lerp(navigationObject.localPosition, rootedPosition, Time.deltaTime * speedOfReturn);
    }
}
