using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.MyProject.Code.Scripts
{
    [DefaultExecutionOrder(-10)]
    public class Main : MonoBehaviour
    {
        static void DoingLog(string name)
        {
            Debug.Log($"{name} doing");
        }
        static void DoneLog(string name)
        {
            Debug.Log($"{name} done");
        }

        public bool IsApplicationStarted { get; private set; }

        void Start()
        {
            UniTask.Void(async () =>
            {
                var eventName = "ApplicationStart";
                DoingLog(eventName);

                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
                Screen.SetResolution(1280, 720, false);
                Screen.fullScreenMode = FullScreenMode.Windowed;
                await UniTask.Delay(0);

                IsApplicationStarted = true;
                DoneLog(eventName);
            });
        }

        void Update()
        {
            if (!IsApplicationStarted) return;
        }

        void OnApplicationPause(bool pauseStatus)
        {
            var eventName = pauseStatus ? "Suspend" : "Resume";
            DoingLog(eventName);

            if (pauseStatus)
            {
                // Suspend
            }
            else
            {
                // Resume
            }

            DoneLog(eventName);
        }

        void OnApplicationQuit()
        {
            var eventName = "ApplicationQuit";
            DoingLog(eventName);

            DoneLog(eventName);
        }
    }
}
