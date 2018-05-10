namespace Assets.Source.App
{
    public static class Constants
    {
        public const string GO_SERVICES = "_Services";
        public const string GO_GAME_LOGIC = "_GameLogic";
        public const string GO_REPOSITORIES = "_Repositories";
        public const string GO_PLAYER_PROFILE = "_PlayerProfile";
        public const string GO_USER_CONTROL = "UserControl";        
        public const string GO_JESTER = "Jester";
        public const string UI_CANVAS = "UICanvas";

        // PLAYER PREFS KEYS
        public const string PREF_KICK_FORCE = "kick_force";
        public const string PREF_KICK_FORCE_INFLIGHT = "kick_force_inflight";
        public const string PREF_KICK_COUNT = "kick_count";
        public const string PREF_BEST_DISTANCE = "best_distance";
        public const string PREF_CURRENCY = "currency";

        // SCENES
        public const string SCENE_GAME = "Default";
        public const string SCENE_SHOP = "Shop";

        // TAGS
        public const string TAG_GROUND = "Ground";
        public const string TAG_OBSTACLE = "Obstacle";
        public const string TAG_BOOST = "Boost";
        public const string TAG_PICKUP = "Pickup";

        // ToDo: Figure out actual factor
        public const int FLOAT_PRECISION_DIGITS = 4;
        public const int FLOAT_PRECISION_FACTOR = 1000;
        public const float UNIT_TO_METERS_FACTOR = 5f;
        
    }
}