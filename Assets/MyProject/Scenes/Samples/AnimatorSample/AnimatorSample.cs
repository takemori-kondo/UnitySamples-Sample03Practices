﻿using UnlimitedFairytales.UnityUtils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorSample : MonoBehaviour
{
    const string SHOWING = "Showing";
    const string SHOWN = "Shown";
    const string CLOSING = "Closing";
    const string CLOSED = "Closed";

    [SerializeField] Button _button;
    [SerializeField] Animator _animator;

    bool _isClosed = true;

    void Start()
    {
        _button.onClick.AddListener(UniTask.UnityAction(async () =>
        {
            var nextStateName = _isClosed ? SHOWING : CLOSING;
            _isClosed = !_isClosed;
            _animator.SetTrigger(nextStateName);
            await AsyncUtil.DelayForAnimation(_animator, nextStateName);
            Debug.Log($"{nextStateName} complete");
        }));
    }
}
