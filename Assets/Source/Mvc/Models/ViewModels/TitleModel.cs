using UniRx;

namespace Assets.Source.Mvc.Models.ViewModels
{
    public class TitleModel
    {
        public BoolReactiveProperty IsFirstStart = new BoolReactiveProperty();

        public ReactiveCommand OpenSettings = new ReactiveCommand();
        public ReactiveCommand OpenTutorial = new ReactiveCommand();
        public ReactiveCommand OpenCredits = new ReactiveCommand();
    }
}
