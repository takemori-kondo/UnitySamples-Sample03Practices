using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DOTweenSample : MonoBehaviour
{
    [SerializeField] Button _btn00;
    [SerializeField] Button _btn01;
    [SerializeField] Button _btn02;
    [SerializeField] Button _btn03;
    [SerializeField] Button _btn04;
    [SerializeField] Button _btn05;
    [SerializeField] CanvasGroup _canvasGroup00;
    [SerializeField] CanvasGroup _canvasGroup01;
    [SerializeField] CanvasGroup _canvasGroup02;
    [SerializeField] CanvasGroup _canvasGroup03;
    [SerializeField] CanvasGroup _canvasGroup04;
    [SerializeField] CanvasGroup _canvasGroup05;
    bool _isOn00 = true;
    bool _isOn01 = true;
    bool _isOn02 = true;
    readonly float _duration_sec = 0.5f;

    void Start()
    {
        _btn00.onClick.AddListener(() =>
        {
            _isOn00 = !_isOn00;
            if (_isOn00)
            {
                _canvasGroup00.transform.DOScale(1.0f, _duration_sec).SetEase(Ease.InOutCubic);
                _canvasGroup00.DOFade(1.0f, _duration_sec);
            }
            else
            {
                _canvasGroup00.transform.DOScale(0.0f, _duration_sec).SetEase(Ease.InOutCubic);
                _canvasGroup00.DOFade(0.0f, _duration_sec);
            }
        });
        _btn01.onClick.AddListener(() =>
        {
            _isOn01 = !_isOn01;
            if (_isOn01)
            {
                _canvasGroup01.transform.DOScaleX(1.0f, _duration_sec).SetEase(Ease.InOutBack);
                _canvasGroup01.DOFade(1.0f, _duration_sec);
            }
            else
            {
                _canvasGroup01.transform.DOScaleX(0.0f, _duration_sec).SetEase(Ease.InOutBack);
                _canvasGroup01.DOFade(0.0f, _duration_sec);
            }
        });
        _btn02.onClick.AddListener(() =>
        {
            _isOn02 = !_isOn02;
            if (_isOn02)
            {
                _canvasGroup02.transform.DOScaleY(1.0f, _duration_sec).SetEase(Ease.InOutElastic);
                _canvasGroup02.DOFade(1.0f, _duration_sec);
            }
            else
            {
                _canvasGroup02.transform.DOScaleY(0.0f, _duration_sec).SetEase(Ease.InOutElastic);
                _canvasGroup02.DOFade(0.0f, _duration_sec);
            }
        });

        var initialValue03 = ((RectTransform)(_canvasGroup03.transform)).anchoredPosition;
        _btn03.onClick.AddListener(() =>
        {
            Vector3 endValue = initialValue03 + new Vector2(50, 50);
            ((RectTransform)_canvasGroup03.transform).DOAnchorPos(endValue, _duration_sec).SetEase(Ease.InOutCubic);
        });

        var initialValue04 = ((RectTransform)(_canvasGroup04.transform)).anchoredPosition;
        _btn04.onClick.AddListener(() =>
        {
            Vector3 startValue = initialValue04 + new Vector2(50, 50);
            ((RectTransform)_canvasGroup04.transform).DOAnchorPos(startValue, _duration_sec).SetEase(Ease.InOutCubic).From();
        });
        _btn05.onClick.AddListener(() =>
        {
            Vector3 punch = new Vector2(50, 50);
            ((RectTransform)_canvasGroup05.transform).DOPunchAnchorPos(punch, _duration_sec).SetEase(Ease.InOutCubic);
        });
    }
}
