using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using DG.Tweening;
using Model.Hero;
using Model.Monsters;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class OnDieEffect : MonoBehaviour
{
    private float time = 1;
    private float fadeTime = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator_Base animatorBase;
    [SerializeField] private ShakeMultiplierTimes shakeEntityEffect;
    [SerializeField] private EntityAction entityAction;
    [SerializeField] private MonsterCharacter monsterCharacter;
    public Transform model;
    private void Start()
    {
        monsterCharacter = GetComponentInParent<MonsterCharacter>();
        spriteRenderer = model.GetComponentInChildren<SpriteRenderer>();
        animatorBase = monsterCharacter.GetRef<Animator_Base>();
        entityAction = monsterCharacter.GetRef<EntityAction>();
        time = animatorBase.GetAnimationLength(AnimationType.Dying);

        entityAction.OnDie += OnDie;
        shakeEntityEffect = new ShakeMultiplierTimes(transform, 5, 0.1f);
    }

    [Button]
    public void OnDie()
    {
        Debug.Log("OnDie and play animation");
        animatorBase.SetIsPlayDefaultAnimation(false);
        animatorBase.ChangeAnimation(AnimationType.Dying);
        monsterCharacter.StopExecute();
        StartCoroutine(OnDieCoroutine());
        StartCoroutine(shakeEntityEffect.ShakeTransform());
    }

    private IEnumerator OnDieCoroutine()
    {
        yield return new WaitForSeconds(time);
        animatorBase.enabled = false;
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