using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountDownTimer : MonoBehaviour
{
    public class Timer
    {
        public string TimerId { get; private set; }
        public float Duration { get; private set; }
        public float RemainingTime { get; private set; }
        public Action OnComplete { get; private set; }
        public bool IsRunning { get; private set; }

        public Timer(string timerId, float duration, Action onComplete)
        {
            TimerId = timerId;
            Duration = duration;
            RemainingTime = duration;
            OnComplete = onComplete;
            IsRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (!IsRunning) return;

            RemainingTime -= deltaTime;
            if (RemainingTime <= 0)
            {
                RemainingTime = 0;
                IsRunning = false;
                OnComplete?.Invoke();
            }
        }

        public string GetFormattedTime()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(RemainingTime);
            return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }

        public string GetSecondsString()
        {
            return Mathf.CeilToInt(RemainingTime).ToString();
        }
    }

    private Dictionary<string, Timer> timers = new Dictionary<string, Timer>();

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        List<string> completedTimers = new List<string>();

        foreach (var kvp in timers)
        {
            Timer timer = kvp.Value;
            timer.Update(deltaTime);

            if (!timer.IsRunning)
            {
                completedTimers.Add(kvp.Key);
            }
        }

        foreach (string timerId in completedTimers)
        {
            timers.Remove(timerId);
        }
    }

    public void StartTimer(string timerId, float duration, Action onComplete)
    {
        if (timers.ContainsKey(timerId))
        {
            Debug.LogWarning($"Timer with ID {timerId} already exists. Restarting the timer.");
            timers.Remove(timerId);
        }

        Timer timer = new Timer(timerId, duration, onComplete);
        timers.Add(timerId, timer);
    }

    public void StopTimer(string timerId)
    {
        if (timers.ContainsKey(timerId))
        {
            timers.Remove(timerId);
        }
        else
        {
            Debug.LogWarning($"No timer found with ID {timerId}.");
        }
    }

    public string GetFormattedTime(string timerId)
    {
        if (timers.ContainsKey(timerId))
        {
            return timers[timerId].GetFormattedTime();
        }

        Debug.LogWarning($"No timer found with ID {timerId}.");
        return "00:00:00";
    }

    public string GetSecondsString(string timerId)
    {
        if (timers.ContainsKey(timerId))
        {
            return timers[timerId].GetSecondsString();
        }

        Debug.LogWarning($"No timer found with ID {timerId}.");
        return "0";
    }
}
