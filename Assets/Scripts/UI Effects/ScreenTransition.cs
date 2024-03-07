using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Effects
{
    public class ScreenTransition : MonoBehaviour
    {
        public Image image;
        public float duration;
        public float durationInTran = .25f;
        public YieldInstruction waitBetweenTransition = new WaitForSeconds(.5f);

        private void Awake()
        {
            image.gameObject.SetActive(false);
        }

        public IEnumerator StartTransition()
        {
            image.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            yield return image.DOFillAmount(1, duration).SetEase(Ease.OutQuart).WaitForCompletion();
        }

        public IEnumerator EndTransition()
        {
            yield return image.DOFillAmount(0, duration).SetEase(Ease.OutQuart).WaitForCompletion();
            yield return new WaitForEndOfFrame();
            image.gameObject.SetActive(false);
        }
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Transition();
        //    }
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {
        //        image.fillAmount = 0;
        //    }
        //}
    }
}