namespace Assets.Source.App
{
    public static class Constants
    {        
        public const string GO_GAME_LOGIC = "_GameLogic";
        public const string GO_USER_CONTROL = "UserControl";        
        public const string GO_JESTER = "Jester";        

        // SCENES
        public const string SCENE_GAME = "Game";
        public const string SCENE_SHOP = "Shop";
        
        public const int FLOAT_PRECISION_DIGITS = 4;
        public const int FLOAT_PRECISION_FACTOR = 100;

        // The Jester is 3x3 units big
        // We assume here that the jester is around 1,5m high
        public const float UNIT_TO_METERS_FACTOR = 2f;


        /* -------------------- ITEM TAGS */
        public const string TAG_GROUND = "Ground";
        public const string TAG_OBSTACLE = "Obstacle";
        public const string TAG_BOOST = "Boost";
        public const string TAG_PICKUP = "Pickup";
    }
}