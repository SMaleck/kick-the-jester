namespace Assets.Source.App
{
    // ToDo Remove obsolete constants
    public static class Constants
    {        
        // FLOAT PRECISION FACTORS
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


        /* -------------------- TEXT MESH PRO TAGS */
        public const string TMP_SPRITE_COIN = "<sprite=0>";
    }
}