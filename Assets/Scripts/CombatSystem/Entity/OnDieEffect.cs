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
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator_Base animatorBase;
    [SerializeField] private ShakeMultiplierTimes shakeEntityEffect;
    [SerializeField] private EntityStateManager EntityStateManager;
    [SerializeField] private MonsterCharacter monsterCharacter;

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
        animatorBase.SetIsPlayDefaultAnimation(false);
        animatorBase.ChangeAnimation(AnimationType.Dying);
        monsterCharacter.StopExecute();
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