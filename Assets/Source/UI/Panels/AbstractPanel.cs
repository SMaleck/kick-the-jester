using UnityEngine;

namespace Assets.Source.UI.Panels
{
    public class AbstractPanel : MonoBehaviour
    {
        [SerializeField] protected bool startActive = true;
        [SerializeField] protected bool resetScale = true;
        [SerializeField] protected bool resetPosition = true;
        [SerializeField] protected Vector3 localPosition = Vector3.zero;

        protected PanelLoader owner;

        public virtual void Setup()
        {
            this.owner = GetComponentInParent<PanelLoader>();
            this.gameObject.SetActive(startActive);

            // Reset local scale, because UGUI is sometimes changing when loading from code
            if (resetScale)
            {
                gameObject.transform.localScale = Vector3.one;
            }
            
            // Reset Position as well
            if (resetPosition)
            {
                transform.localPosition = localPosition;
            }
        }


        public void Show()
        {
            this.gameObject.SetActive(true);
        }


        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
