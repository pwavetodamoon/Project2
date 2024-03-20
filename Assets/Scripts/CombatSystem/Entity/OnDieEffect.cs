using System.Collections;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using DG.Tweening;
using Model.Hero;
using Model.Monsters;
using Sirenix.OdinInspector;
using UnityEngine;

public class OnDieEffect : MonoBehaviour
{
    private float time = 1;
    private float fadeTime = 1;
    private SpriteRenderer spriteRenderer;
    private Animator_Base animatorBase;
    private ShakeMultiplierTimes shakeEntityEffect;
    private EntityStateManager EntityStateManager;
    private MonsterCharacter monsterCharacter;

    private void Start()
    {
        monsterCharacter = GetComponent<MonsterCharacter>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animatorBase = monsterCharacter.GetAnimatorBase();
        time = animatorBase.GetAnimationLength(AnimationType.Dying);
        EntityStateManager = GetComponent<EntityStateManager>();
        EntityStateManager.OnDie += OnDie;
        shakeEntityEffect = new ShakeMultiplierTimes(transform, 5, 0.1f);

    }

    [Button]
    public void OnDie()
    {
        if (monsterCharacter == null || animatorBase == null) return;
        animatorBase.SetIsPlayDefaultAnimation(false);
        animatorBase.ChangeAnimation(AnimationType.Dying);
        StartCoroutine(OnDieCoroutine());
        StartCoroutine(shakeEntityEffect.ShakeTransform());
    }

    private IEnumerator OnDieCoroutine()
    {

        yield return new WaitForSeconds(time);
        spriteRenderer.DOFade(0, fadeTime).OnComplete(() =>
        {
            monsterCharacter.KillMonster();
        });
        StopAllCoroutines();
    }
    [Button]
    public void Reset()
    {
        animatorBase.ChangeAnimation(AnimationType.Walk);
        spriteRenderer.DOFade(1, 0);
    }
}