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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animatorBase = GetComponentInChildren<Animator_Base>();
        time = animatorBase.GetAnimationLength(AnimationType.Dying);
        EntityStateManager = GetComponent<EntityStateManager>();
        EntityStateManager.OnDie += OnDie;
        monsterCharacter = GetComponent<MonsterCharacter>();
        shakeEntityEffect = new ShakeMultiplierTimes(transform, 5, 0.1f);

    }

    [Button]
    public void OnDie()
    {
        StartCoroutine(OnDieCoroutine());
        StartCoroutine(shakeEntityEffect.ShakeTransform());
    }

    private IEnumerator OnDieCoroutine()
    {
        animatorBase.ChangeAnimation(AnimationType.Dying);
        animatorBase.SetIsPlayDefaultAnimation(false);
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