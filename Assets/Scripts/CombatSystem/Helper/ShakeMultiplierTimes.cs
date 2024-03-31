using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShakeMultiplierTimes
{
    protected Transform target;
    protected int count;
    protected float magnitude;
    protected float durationPerShake = .1f;
    protected float durationGoBackCall = .05f;

    public ShakeMultiplierTimes(Transform target, int count, float magnitude)
    {
        this.target = target;
        this.count = count;
        this.magnitude = magnitude;
    }

    public void ChangeDurationPerShake(float durationPerShake)
    {
        this.durationPerShake = durationPerShake;
    }

    public void ChangeDurationGoBackCall(float durationGoBackCall)
    {
        this.durationGoBackCall = durationGoBackCall;
    }

    public virtual IEnumerator ShakeTransform()
    {
        var originalPosition = target.position;
        for (var i = 0; i < count; i++) yield return ShakeCharacterThenMoveBack(target, durationPerShake, durationGoBackCall, magnitude);
        target.position = originalPosition;
    }

    public IEnumerator ShakeCharacterThenMoveBack(Transform target, float durationShake,
        float durationGoBackCall, float magnitude)
    {
        var originalPosition = target.position;

        var x = Random.Range(-1f, 1f) * magnitude;
        var y = Random.Range(-1f, 1f) * magnitude;
        var newPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

        yield return target.DOMove(newPosition, durationShake).SetEase(Ease.OutCubic).WaitForCompletion();
        yield return target.DOMove(originalPosition, durationGoBackCall).SetEase(Ease.OutCubic).WaitForCompletion();
    }
}