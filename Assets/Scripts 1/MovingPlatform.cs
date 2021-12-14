using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Gameplay;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public Transform start;
    public Transform end;
    public float movementDuration;

    private PlayerCharacterController playerController;
    private Vector3 lastFramePosition;

    // Start is called before the first frame update
    void Start()
    {
        platform.transform.position = start.position;
        platform.transform.DOPath(new Vector3[] { start.position, end.position, start.position }, movementDuration).SetLoops(-1).SetEase(Ease.Linear);
    }

    public void SetPlayer(Collider col)
    {
        playerController = col.GetComponent<PlayerCharacterController>();
    }

    public void UnsetPlayer()
    {
        playerController = null;
    }

    private void Update()
    {
        if (playerController) playerController.ExternalMovement = platform.transform.position - lastFramePosition;
        lastFramePosition = platform.transform.position;
    }
}
