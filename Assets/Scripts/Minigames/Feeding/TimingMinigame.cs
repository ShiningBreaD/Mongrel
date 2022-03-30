using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimingMinigame : MonoBehaviour
{
    private Slider _slider;
    private int _target;
    private float _speed;
    private bool _moving = true;

    [SerializeField] private RectTransform hit;
    [SerializeField] private int reward;
    [SerializeField] private int punishment;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        
        _slider.value = 0;
        _target = 1;
    }

    private void OnMouseDown()
    {
        _moving = false;
        if (HandlerInHit())
            Hit();
        else
            Miss();
    }

    private void Hit()
    {
        ShelterManagment.socialCredits += reward;
    }

    private void Miss()
    {
        ShelterManagment.socialCredits -= reward;
    }

    private void Update()
    {
        if (_moving)
            MoveHandler();
    }

    private void MoveHandler()
    {
        if (_slider.value == 0)
            _target = 1;
        else if (_slider.value == 1)
            _target = 0;

        _speed = Mathf.PingPong(Time.time, 1.4f) + 0.9f;

        _slider.value = Mathf.MoveTowards(_slider.value, _target, Time.deltaTime * _speed);
    }

    private bool HandlerInHit()
    {
        RectTransform handler = _slider.handleRect;

        float leftBorder = -(hit.rect.width / 2) + (handler.rect.width / 2);
        float rightBorder = -leftBorder;

        if (handler.localPosition.x >= leftBorder && handler.localPosition.x <= rightBorder)
            return true;
        return false;
    }
}
