using UnityEngine;

namespace Helper
{
    public class GetAnimationLength : MonoBehaviour
    {
        public float length => RefreshLength();
        public float RefreshLength()
        {
            var animator = GetComponent<Animator>();
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            var clip = clipInfo[0].clip;
            var length = clip.length;
            Debug.Log($"Animation length: {length}");
            return length;
        }
    }
}
