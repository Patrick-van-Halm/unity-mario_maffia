using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Platformer_Timed : Platformer
{
    public float timeLimit;
    public UnityEvent onTimerRunOut;
    public TMP_Text timerText;

    private Coroutine coroTimer;
    private float currentTime;

    private void Update()
    {
        if (coroTimer == null || currentTime < 0) return;

        currentTime -= Time.deltaTime;
        timerText.text = currentTime.ToString("N1");
    }

    public override void StartLevel()
    {
        base.StartLevel();
        coroTimer = StartCoroutine(RunTimer());
    }

    public override void FinishLevel()
    {
        coroTimer.Stop(this);
    }

    private IEnumerator RunTimer()
    {
        currentTime = timeLimit;
        yield return new WaitForSeconds(timeLimit);
        onTimerRunOut?.Invoke();
    }
}
