using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Elements
{
    public class CurrencyItem : MonoBehaviour
    {
        [SerializeField] private Text labelText;
        [SerializeField] private Text valueText;

        public string Label
        {
            get { return labelText.text; }
            set { labelText.text = value; }
        }

        public string Value
        {
            get { return valueText.text; }
            set { valueText.text = value; }
        }

        public float Height
        {
            get { return gameObject.GetComponent<RectTransform>().rect.height; }
        }
    }
}
