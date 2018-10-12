using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Util.UI
{
    public class ImageSpinner : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        private readonly Vector3 _direction = new Vector3(0, 0, -1);
        private readonly float speedSeconds = 0.02f;

        private void Start()
        {
            if (_image != null)
            {
                _image.rectTransform
                    .DOLocalRotate(_direction, speedSeconds)
                    .SetLoops(-1, LoopType.Incremental);
            }
        }
    }
}
