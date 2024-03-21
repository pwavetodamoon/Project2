using Sirenix.OdinInspector;
using UnityEngine;

namespace Helper
{
    public class GetAnimationLength : MonoBehaviour
    {
        public float length => RefreshLength();

        [Button]
        public float RefreshLength()
        {
            var animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            var clip = clipInfo[0].clip;
            var length = clip.length;
            Debug.Log($"Animation length: {length}");
            return length;
        }
    }
}