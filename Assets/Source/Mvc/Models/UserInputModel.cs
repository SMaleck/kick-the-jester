using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class UserInputModel
    {
        public ReactiveCommand OnClickedAnywhere = new ReactiveCommand();
        public ReactiveCommand OnPause = new ReactiveCommand();
    }
}
