using UnityEngine;
using UnityEngine.UI;

public class SickDog : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Sprite _angry, _broken, _healed;

    public enum State { angry, broken, healed }

    public void ChangeState(State state)
    {
        if (state == State.broken)
        {
            _image.sprite = _broken;
            _rectTransform.sizeDelta = new Vector2(885, 558);
        }
        else if (state == State.healed)
        {
            _image.sprite = _healed;
            _rectTransform.sizeDelta = new Vector2(885, 506);
            Dog.Instance.Heal(1f - Dog.Instance.health);
            Dog.Instance.isSick = false;
        }
    }
}
