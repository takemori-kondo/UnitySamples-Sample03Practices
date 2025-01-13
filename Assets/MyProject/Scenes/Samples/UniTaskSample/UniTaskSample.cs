using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UniTaskSample : MonoBehaviour
{
    static Action<Exception> unobservedTaskExceptionCallback;
    static void RegisterUnobservedTaskExceptionCallback()
    {
        object lockObj = new();
        lock (lockObj)
        {
            if (unobservedTaskExceptionCallback == null)
            {
                unobservedTaskExceptionCallback = (ex) =>
                {
                    Debug.Log($"On UniTask<T>/UniTask/UniTaskVoid unobserved exception : {ex.Message}");
                };
                UniTaskScheduler.UnobservedTaskException += unobservedTaskExceptionCallback;
            }
        }
    }

    // static / instance

    [SerializeField] Button _btnTask;
    [SerializeField] Button _btnUniTask;

    void Start()
    {
        RegisterUnobservedTaskExceptionCallback();
        UniTask.Action(async () =>
        {
            Debug.Log("Start begin");
            await UniTask.Delay(1000);
            throw new Exception("yeah!");
        })();
        _btnTask.onClick.AddListener(UniTask.UnityAction(async () =>
        {
            Debug.Log($"main thread : id={Thread.CurrentThread.ManagedThreadId}");
            await DoTask();
        }));
        _btnUniTask.onClick.AddListener(UniTask.UnityAction(async () =>
        {
            Debug.Log($"main thread : id={Thread.CurrentThread.ManagedThreadId}");
            await DoUniTask();
        }));
    }

    async Task DoTask()
    {
        string name = "DoTask";
        Debug.Log($"{name} begin");
        var tasks = new List<Task>();
        try
        {
            tasks.Add(Task.Run(async () =>
            {
                await Task.Delay(1000);
                Debug.Log($"{name}'s task1(id={Thread.CurrentThread.ManagedThreadId}) 1000ms");
                _btnTask.GetComponent<Image>().color = Color.red;
                Debug.Log($"{name}'s task1(id={Thread.CurrentThread.ManagedThreadId}) complete");
                throw new Exception("task1");
            }));
            tasks.Add(Task.Run(async () =>
            {
                await Task.Delay(1500);
                Debug.Log($"{name}'s task2(id={Thread.CurrentThread.ManagedThreadId}) 1500ms");
                _btnTask.GetComponent<Image>().color = Color.green;
                Debug.Log($"{name}'s task2(id={Thread.CurrentThread.ManagedThreadId}) complete");
                throw new Exception("task2");
            }));
            tasks.Add(Task.Run(async () =>
            {
                await Task.Delay(2000);
                Debug.Log($"{name}'s task3(id={Thread.CurrentThread.ManagedThreadId}) 2000ms");
                _btnTask.GetComponent<Image>().color = Color.blue;
                Debug.Log($"{name}'s task3(id={Thread.CurrentThread.ManagedThreadId}) complete");
                throw new Exception("task3");
            }));
            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            Debug.Log($"ex.Message : {ex.Message}");
            var exceptions = tasks.Where(task => task.Exception != null).Select(task => task.Exception);
            foreach (var e in exceptions)
            {
                Debug.Log($"tasks throw exception : {e.Message}");
            }
            throw;
        }
        Debug.Log($"{name} end");
    }

    async UniTask DoUniTask()
    {
        string name = "DoUniTask";
        Debug.Log($"{name} begin");
        var tasks = new List<UniTask>();
        try
        {
            tasks.Add(UniTask.Create(async () =>
            {
                await UniTask.Delay(1000);
                Debug.Log($"{name}'s task1(id={Thread.CurrentThread.ManagedThreadId}) 1000ms");
                _btnUniTask.GetComponent<Image>().color = Color.red;
                Debug.Log($"{name}'s task1(id={Thread.CurrentThread.ManagedThreadId}) complete");
                throw new Exception("task1");
            }));
            tasks.Add(UniTask.Create(async () =>
            {
                await UniTask.Delay(1500);
                Debug.Log($"{name}'s task2(id={Thread.CurrentThread.ManagedThreadId}) 1500ms");
                _btnUniTask.GetComponent<Image>().color = Color.green;
                Debug.Log($"{name}'s task2(id={Thread.CurrentThread.ManagedThreadId}) complete");
                throw new Exception("task2");
            }));
            tasks.Add(UniTask.Create(async () =>
            {
                await UniTask.Delay(2000);
                Debug.Log($"{name}'s task3(id={Thread.CurrentThread.ManagedThreadId}) 2000ms");
                _btnUniTask.GetComponent<Image>().color = Color.blue;
                Debug.Log($"{name}'s task3(id={Thread.CurrentThread.ManagedThreadId}) complete");
                throw new Exception("task3");
            }));
            await UniTask.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            Debug.Log($"ex.Message : {ex.Message}");
            Debug.Log("https://github.com/Cysharp/UniTask/issues/514");
            throw;
        }
        Debug.Log($"{name} end");
    }
}
