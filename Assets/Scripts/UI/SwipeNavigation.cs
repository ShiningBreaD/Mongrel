using UnityEngine;

public class SwipeNavigation : FloatableObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rootedPosition =
            new Vector3(collision.transform.localPosition.x * -1, rootedPosition.y, rootedPosition.z);
    }

    protected override void OnMouseDown()
    {
        isTouching = true;

        offset = navigationObject.position - 
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, navigationObject.position.y, 10f));
    }

    protected override void OnMouseDrag()
    {
        Vector3 desiredPosition = new Vector3(Input.mousePosition.x, navigationObject.position.y, 10f);
        navigationObject.position = Camera.main.ScreenToWorldPoint(desiredPosition) + offset;
    }
}
