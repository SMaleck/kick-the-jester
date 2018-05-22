using UnityEngine;

namespace Assets.Source.UI.Panels
{
    public class AbstractPanel : MonoBehaviour
    {
        [SerializeField] protected bool startActive = true;
        [SerializeField] protected bool resetScale = true;
        [SerializeField] protected bool resetPosition = true;


        public virtual void Setup()
        {
            this.gameObject.SetActive(startActive);

            // Reset local scale, because UGUI is sometimes changing when loading from code
            if (resetScale)
            {
                gameObject.transform.localScale = Vector3.one;
            }
            
            // Reset Position as well
            if (resetPosition)
            {
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
