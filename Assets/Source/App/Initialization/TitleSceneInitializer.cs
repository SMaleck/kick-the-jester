using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views;
using Zenject;

namespace Assets.Source.App.Initialization
{
    public class TitleSceneInitializer : AbstractSceneInitializer, IInitializable
    {
        [Inject] private readonly TitleView _titleView;
        [Inject] private readonly SettingsView _settingsView;
        [Inject] private readonly CreditsView _creditsView;
        [Inject] private readonly TutorialView _tutorialView;
        [Inject] private readonly ResetProfileConfirmationView _resetProfileConfirmationView;

        public void Initialize()
        {
            SetupView(_titleView);

            SetupClosableView(_creditsView, ClosableViewType.Credits);
            SetupClosableView(_resetProfileConfirmationView, ClosableViewType.ResetProfileConfirmation);
            SetupClosableView(_settingsView, ClosableViewType.Settings);
            SetupClosableView(_tutorialView, ClosableViewType.Tutorial);
        }
    }
}
