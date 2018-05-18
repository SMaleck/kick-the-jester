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

        // SCENES
        public const string SCENE_GAME = "Game";
        public const string SCENE_SHOP = "Shop";


        // ToDo: Figure out actual factor
        public const int FLOAT_PRECISION_DIGITS = 4;
        public const int FLOAT_PRECISION_FACTOR = 100;
        public const float UNIT_TO_METERS_FACTOR = 5f;


        /* -------------------- ITEM TAGS */
        public const string TAG_GROUND = "Ground";
        public const string TAG_OBSTACLE = "Obstacle";
        public const string TAG_BOOST = "Boost";
        public const string TAG_PICKUP = "Pickup";


        /* -------------------- Jester Configuration */
        public const float MAX_SPEED = 5f;
    }
}