using TMPro;
using UnityEngine;

namespace Assets.Source.UI.Elements
{
    // ToDo Move Namespace
    public class CurrencyItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private TMP_Text valueText;

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
