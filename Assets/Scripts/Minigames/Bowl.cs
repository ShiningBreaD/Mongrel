using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Food))]
public class Bowl : MonoBehaviour
{
    [SerializeField] private Sprite _full, _empty;

    private Image _image;
    private Food _food;
    [SerializeField] private RectTransform _rectTransform;

    public enum State { fill = 0, empty = 1 }

    private void Start()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _food = GetComponent<Food>();
    }

    public void ChangeState(State state)
    {
        if (state == State.fill)
        {
            _image.sprite = _full;
            _rectTransform.sizeDelta = new Vector2(705, 522);
            Dog.Instance.Feed(_food.data.Nourishmen);
            
        }
        else
        {
            _image.sprite = _empty;
            _rectTransform.sizeDelta = new Vector2(705, 348);
        }
    }
}
