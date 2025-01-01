using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public Action OnChangeState;
    public bool IsActive { get; private set; }
    private readonly List<string> validTags = new List<string> { "Player", "Rock" };

    private List<Collider2D> validObjectsOnPlate = new List<Collider2D>();
    private float rockHeight;
    private Transform rockTransform;

    private void Update()
    {
        if (rockTransform) rockTransform.position = new Vector3(rockTransform.position.x, rockHeight, rockTransform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsValidObject(other) && !validObjectsOnPlate.Contains(other))
        {
            if (other.CompareTag("Rock")) AdjustRock(other);
            validObjectsOnPlate.Add(other);
            UpdateState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (validObjectsOnPlate.Contains(other))
        {
            if (other.CompareTag("Rock")) rockTransform = null;
            validObjectsOnPlate.Remove(other);
            UpdateState();
        }
    }

    private void UpdateState()
    {
        bool isPressed = validObjectsOnPlate.Count > 0;
        if (isPressed != IsActive)
        {
            IsActive = isPressed;
            animator.Play(isPressed ? Constants.MiscAnimations.PRESSURE_PLATE_PRESSED : Constants.MiscAnimations.PRESSURE_PLATE_IDLE);
            OnChangeState?.Invoke();
        }
    }

    private bool IsValidObject(Collider2D collider2D) => validTags.Contains(collider2D.tag);

    private void AdjustRock(Collider2D other) => StartCoroutine(StartLerpRockPosition(other.gameObject, other.transform.position + Vector3.up * 0.03f));

    private IEnumerator StartLerpRockPosition(GameObject go, Vector3 finalPos)
    {
        float timeElapsed = 0f;
        while (timeElapsed < 0.2f)
        {
            go.transform.position = Vector3.Lerp(go.transform.position, finalPos, timeElapsed / 0.2f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        rockHeight = finalPos.y;
        rockTransform = go.transform;
    }
}