using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimingMinigame : MonoBehaviour
{
    private Animation _animation;
    private Slider _slider;

    private int _target;
    private float _speed;
    private bool _moving = true;

    [SerializeField] private RectTransform _hit;
    [SerializeField] private Bowl _bowl;
    [SerializeField] private int _reward;
    [SerializeField] private int _punishment;

    [SerializeField] private GameObject _shelter;
    [SerializeField] private ContentSwitcher _contentSwitcher;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _animation = GetComponent<Animation>();
        _contentSwitcher = FindObjectOfType<ContentSwitcher>();

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

        _animation.Play("Hide&Destroy");
    }
    private void Hit()
    {
        ShelterManagment.socialCredits += _reward;
        _bowl.ChangeState(Bowl.State.fill);
    }

    private void Miss()
    {
        ShelterManagment.socialCredits -= _reward;
        _bowl.ChangeState(Bowl.State.empty);
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

        float leftBorder = -(_hit.rect.width / 2) + (handler.rect.width / 2);
        float rightBorder = -leftBorder;

        if (handler.localPosition.x >= leftBorder && handler.localPosition.x <= rightBorder)
            return true;
        return false;
    }

    public void Destroy()
    {
        _contentSwitcher.OpenMC(_shelter);
        _contentSwitcher.Close(GetComponentInParent<Image>().gameObject);
    }
}
