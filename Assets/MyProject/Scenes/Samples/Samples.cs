using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Samples : MonoBehaviour
{
    [SerializeField] List<RectTransform> _pageList;
    [SerializeField] Button _btnPrevious;
    [SerializeField] Button _btnNext;
    int _currentPage;

    void Start()
    {
        _btnPrevious.onClick.AddListener(() =>
        {
            if (_currentPage <= 0) return;
            _currentPage--;
            UpdatePage();
        });
        _btnNext.onClick.AddListener(() =>
        {
            if (_pageList.Count - 1 <= _currentPage) return;
            _currentPage++;
            UpdatePage();
        });
        UpdatePage();
    }

    private void UpdatePage()
    {
        for (int i = 0; i < _pageList.Count; i++)
        {
            var page = _pageList[i];
            DOTween.Complete(page);
            var _ = page.DOAnchorPosX((i - _currentPage) * 1280, 1f);
        }
        _btnPrevious.interactable = 0 < _currentPage;
        _btnNext.interactable = _currentPage < _pageList.Count - 1;
    }
}
