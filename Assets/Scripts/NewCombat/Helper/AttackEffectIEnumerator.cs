using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace NewCombat.Helper
{
    public static class AttackEffectIEnumerator
    {
        public static IEnumerator ShakeCharacterThenMoveBack(Transform target, float durationShake,
            float durationGoBackCall, float magnitude)
        {
            var originalPosition = target.position;

            var x = Random.Range(-1f, 1f) * magnitude;
            var y = Random.Range(-1f, 1f) * magnitude;
            var newPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            yield return target.DOMove(newPosition, durationShake).SetEase(Ease.OutCubic).WaitForCompletion();
            yield return target.DOMove(originalPosition, durationGoBackCall).SetEase(Ease.OutCubic).WaitForCompletion();
        }

        public static IEnumerator ShakeCharacterMultiplierTimes(Transform target, float magnitude, int count)
        {
            var originalPosition = target.position;
            for (var i = 0; i < count; i++) yield return ShakeCharacterThenMoveBack(target, .1f, .05f, magnitude);
            target.position = originalPosition;
        }
    }
}