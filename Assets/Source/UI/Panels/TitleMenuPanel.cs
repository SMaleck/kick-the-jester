using Assets.Source.App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class TitleMenuPanel : MonoBehaviour
    {
        [Header("Panel Properties")]
        [SerializeField] Button startButton;

        private void Start()
        {
            startButton.OnClickAsObservable().Subscribe(_ => Kernel.SceneTransitionService.ToGame());
        }
    }
}
