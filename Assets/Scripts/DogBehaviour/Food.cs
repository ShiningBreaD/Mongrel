using UnityEngine;

[RequireComponent(typeof(FloatableObject))]
public class Food : MonoBehaviour
{
    [SerializeField] public FoodAnnotation data;
    [SerializeField] private bool _isFloatable = true;

    private Dog _touchedDog;

    public void Start()
    {
        if (!_isFloatable)
            GetComponent<FloatableObject>().enabled = false;
    }

    private void OnMouseDown()
    {
        transform.localScale += (Vector3)new Vector2(0.25f, 0.25f);
    }

    private void OnMouseUp()
    {
        transform.localScale -= (Vector3)new Vector2(0.25f, 0.25f);
        if (_touchedDog != null)
            Feed();
    }

    private void Feed()
    {
        _touchedDog.Feed(data.Nourishmen);
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dog"))
            _touchedDog = collision.GetComponent<Dog>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dog"))
            _touchedDog = null;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
