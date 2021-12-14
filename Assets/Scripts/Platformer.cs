using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{
    [SerializeField] private List<GameObject> otherDifficulties = new List<GameObject>();

    public virtual void StartLevel()
    {
        for(int i = 0; i < otherDifficulties.Count; i++)
        {
            otherDifficulties[i].SetActive(false);
        }
    }

    public virtual void FinishLevel() { }
}
