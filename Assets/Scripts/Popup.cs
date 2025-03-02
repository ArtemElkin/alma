using UnityEngine;
using DG.Tweening;
public class Popup : MonoBehaviour
{
    [SerializeField] private RectTransform _previewRT, _infoRT;
    [SerializeField] private CanvasGroup _popupBodyAplhaGroup, _infoBodyAplhaGroup;
    private Vector2 _targetPosition, _startPosition;
    private Sequence _animation;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        _popupBodyAplhaGroup = GetComponent<CanvasGroup>();
        _popupBodyAplhaGroup.alpha = 0f;
        _targetPosition = _previewRT.anchoredPosition;
        _startPosition = new Vector2(_targetPosition.x, -Screen.height / 2f);
        _previewRT.anchoredPosition = _startPosition;
        _infoBodyAplhaGroup.alpha = 0f;
    }
    public void Show()
    {
        gameObject.SetActive(true);
        _previewRT.gameObject.SetActive(true);
        _previewRT.anchoredPosition = _startPosition;
        ServiceLocator.Current.Get<MouseConroller>().blockMouse = true;
        _animation = DOTween.Sequence();
        _animation
            .Append(_popupBodyAplhaGroup.DOFade(1f, 0.5f))
            .Join(_previewRT.DOAnchorPos(_targetPosition, 0.5f));

        _infoRT.gameObject.SetActive(false);
        _infoBodyAplhaGroup.alpha = 0f;
    }
    public void Hide()
    {
        KillCurrentAnimationIfActive();
        _animation = DOTween.Sequence();
        _animation
            .Append(_popupBodyAplhaGroup.DOFade(0f, 0.5f))
            .OnComplete(() => {
                gameObject.SetActive(false);
                ServiceLocator.Current.Get<MouseConroller>().blockMouse = false;
                });
    }
    public void ShowInfo()
    {
        _infoRT.gameObject.SetActive(true);
        _animation = DOTween.Sequence();
        _animation
            .Append(_infoBodyAplhaGroup.DOFade(1f, 0.25f))
            .OnComplete(() => {
                _previewRT.gameObject.SetActive(false);
            });
    }
    private void KillCurrentAnimationIfActive()
    {
        if (_animation != null && _animation.active)
            _animation.Kill();
    }
}
