using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Mvc.Data
{
    [CreateAssetMenu(fileName = nameof(ViewUtilConfig), menuName = Constants.UMenuRoot + nameof(ViewUtilConfig))]
    public class ViewUtilConfig : ScriptableObject
    {
        [Header("Materials")]
        [SerializeField] private Material _desaturationMaterial;
        public Material DesaturationMaterial => _desaturationMaterial;
    }
}
