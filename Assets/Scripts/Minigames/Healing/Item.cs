using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(FloatableObject))]
public class Item : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private SickDog.State _state;

    private Button _button;
    private SickDog _touchedDog;


    private void Start()
    {
        _button = GetComponent<Button>();
        _amountText.text = "" + _amount;
    }

    private void Update()
    {
        if (_amount == 0)
        {
            _button.interactable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dog"))
            _touchedDog = collision.GetComponent<SickDog>();
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
        if (_amount > 0)
        {
            _amount--;
            _amountText.text = "" + _amount;
            _touchedDog.ChangeState(_state);
        }
    }
}
