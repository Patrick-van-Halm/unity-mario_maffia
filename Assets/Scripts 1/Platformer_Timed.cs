using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platformer_Timed : Platformer
{
    public float timeLimit;
    public UnityEvent onTimerRunOut;

    private Coroutine coroTimer;

    public override void StartLevel()
    {
        base.StartLevel();
        coroTimer = StartCoroutine(RunTimer());
    }

    public override void FinishLevel()
    {
        StopCoroutine(coroTimer);
    }

    private IEnumerator RunTimer()
    {
        yield return new WaitForSeconds(timeLimit);
        onTimerRunOut?.Invoke();
    }
}
