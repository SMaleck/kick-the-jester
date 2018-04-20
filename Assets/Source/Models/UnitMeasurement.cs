using Assets.Source.App;

namespace Assets.Source.Models
{
    public class UnitMeasurement
    {
        public float Units;
        public float Meters
        {
            get { return Units * Constants.UNIT_TO_METERS_FACTOR; }            
        }

        public UnitMeasurement(float Units = 0f)
        {
            this.Units = Units;
        }
    }
}
