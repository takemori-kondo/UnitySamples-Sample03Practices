using Assets.UnlimitedFairytales.UnityUtils.UI;
using System.Collections;
using UnityEngine;

namespace Assets.UnlimitedFairytales.TapEffects
{
    public sealed class TapEffect : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] GameObject _tapEffect_prefab;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var distance = CanvasUtil.GetPixelEqualSizeDistance(Screen.height, _camera.fieldOfView) / 10.0f;
                var pos = Input.mousePosition;
                pos.z = distance;
                CreateTapEffect(pos);
            }
        }

        // Unity event functions & event handlers / pure code

        void CreateTapEffect(Vector3 tapPos)
        {
            var instantiated = Instantiate(_tapEffect_prefab, transform);
            if (instantiated != null)
            {
                var pos3D = _camera.ScreenToWorldPoint(tapPos);
                instantiated.transform.position = pos3D;
                StartCoroutine(AutoDestroy(instantiated));
            }
        }

        IEnumerator AutoDestroy(GameObject instantiated)
        {
            var effects = instantiated.GetComponentsInChildren<ParticleSystem>(true);
            var eachIsPlaying = true;
            while (eachIsPlaying)
            {
                eachIsPlaying = false;
                foreach (var effect in effects)
                {
                    if (effect.isPlaying)
                    {
                        eachIsPlaying = true;
                    }
                }
                yield return null;
            }
            Destroy(instantiated);
        }
    }
}
