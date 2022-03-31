using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FloatableObject))]
public class Item : MonoBehaviour
{
    [SerializeField] private int amount;
    
    private Button _button;
    private Dog _touchedDog;


    private void Start()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (amount == 0)
        {
            _button.interactable = false;
        }
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

    private void OnMouseDown()
    {
        transform.localScale += (Vector3)new Vector2(0.25f, 0.25f);
    }

    private void OnMouseUp()
    {
        transform.localScale -= (Vector3)new Vector2(0.25f, 0.25f);
        if (_touchedDog != null)
            Use();
    }

    private void Use()
    {
        if (amount > 0)
        {
            amount--;
            _touchedDog.Heal();
        }
    }
}
