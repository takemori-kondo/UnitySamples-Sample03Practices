using Assets.UnlimitedFairytales.UnityUtils.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxSample : MonoBehaviour
{
    [SerializeField] Button _button;

    [SerializeField]
    ToastUI _toastUI_prefab;
    ToastUI _toastUI;

    void Start()
    {
        _toastUI = Instantiate(_toastUI_prefab, transform);
        _toastUI.gameObject.SetActive(false);
        _button.onClick.AddListener(UniTask.UnityAction(async () =>
        {
            await _toastUI.ShowAsync("Header text", "This is content text.", 5);
            Debug.Log($"{nameof(_toastUI.ShowAsync)} complete");
        }));
    }
}
