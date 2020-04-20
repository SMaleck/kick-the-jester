using Assets.Source.Features.Cheats;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views;
using Zenject;

namespace Assets.Source.App.Initialization
{
    public class GameSceneInitializer : AbstractSceneInitializer, IInitializable
    {
        [Inject] private readonly BestDistanceMarkerView _bestDistanceMarkerView;
        [Inject] private readonly HudView _hudView;
        [Inject] private readonly PauseView _pauseView;
        [Inject] private readonly ResetProfileConfirmationView _resetProfileConfirmationView;
        [Inject] private readonly RoundEndView _roundEndView;
        [Inject] private readonly SettingsView _settingsView;
        [Inject] private readonly UpgradeScreenView _upgradeScreenView;
        [Inject] private readonly AchievementsScreenView _achievementsScreenView;
        [Inject] private readonly CheatView _cheatView;

        public void Initialize()
        {
            InitViews();
        }

        private void InitViews()
        {
            SetupView(_bestDistanceMarkerView);
            SetupView(_hudView);

            SetupClosableView(_roundEndView, ClosableViewType.RoundEnd);
            SetupClosableView(_pauseView, ClosableViewType.Pause);
            SetupClosableView(_settingsView, ClosableViewType.Settings);
            SetupClosableView(_resetProfileConfirmationView, ClosableViewType.ResetProfileConfirmation);
            SetupClosableView(_upgradeScreenView, ClosableViewType.Upgrades);
            SetupClosableView(_achievementsScreenView, ClosableViewType.Achievements);

            SetupClosableView(_cheatView);
        }
    }
}
