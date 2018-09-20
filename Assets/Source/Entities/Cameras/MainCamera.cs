using Assets.Source.Entities.Jester;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities.Cameras
{
    public class MainCamera : AbstractMonoEntity
    {
        [SerializeField] private Camera _camera;


        private JesterEntity _jester;
        private Transform _jesterTransform;
        private Vector3 _origin;

        private float offsetX = 3.5f;        
        private float offsetXOnLanding = 5.5f;
        private bool increaseOffset = false;


        [Inject]
        private void Inject(JesterEntity jester)
        {
            _jester = jester;
            _jesterTransform = jester.GoTransform;
            _origin = transform.position;

            Observable.EveryUpdate()
                .Subscribe(_ => OnUpdate())
                .AddTo(this);

            Debug.Log("CAMERA INJECT METHOD");
        }

        private void Start()
        {
            Debug.Log("CAMERA INJECT RAN: " + (_jester != null).ToString());

        }

        private void OnUpdate()
        {
            Vector3 targetPos = _jesterTransform.position;
            Vector3 currentPos = GoTransform.position;

            GoTransform.position = new Vector3(targetPos.x + offsetX, Mathf.Clamp(targetPos.y, _origin.y, float.MaxValue), currentPos.z);

            if (increaseOffset)
            {
                offsetX += 3 * Time.deltaTime;
                increaseOffset = offsetX < offsetXOnLanding;
            }
        }


        // ToDo randomly shake cmaera when jester lands
        public void Shake()
        {

        }
    }
}
