namespace Assets.Source.Features.PlayerData
{
    public class PlayerAttributesModel
    {
        public float KickForce { get; private set; }
        public float ShootForce { get; private set; }
        public int Shots { get; private set; }
        public float MaxVelocityX { get; private set; }
        public float MaxVelocityY { get; private set; }

        public void SetKickForce(float value)
        {
            KickForce = value;
        }

        public void SetShootForce(float value)
        {
            ShootForce = value;
        }

        public void SetShots(int value)
        {
            Shots = value;
        }

        public void SetMaxVelocityX(float value)
        {
            MaxVelocityX = value;
        }

        public void SetMaxVelocityY(float value)
        {
            MaxVelocityY = value;
        }
    }
}
