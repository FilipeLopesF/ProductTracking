namespace _3DTrackingProducts.Api.Models.Exceptions
{
    public class VerticalLineException : Exception
    {
        public double x { get; set; }
        public double angle { get; set; }
        public VerticalLineException(double Xposition, double angle)
        {
            x = Xposition;
            this.angle = angle;
        }
    }
}
