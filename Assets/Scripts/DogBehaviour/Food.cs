using UnityEngine;

[RequireComponent(typeof(FloatableObject))]
public class Food : MonoBehaviour
{
    [SerializeField] private FoodAnnotation _data;
    private Animation _animation;

    private Dog _touchedDog;

    private void Start()
    {
        _animation = GetComponent<Animation>();
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
        _touchedDog.Feed(_data.Nourishmen);
        _animation.Play();
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
