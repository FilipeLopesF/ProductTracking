namespace _3DTrackingProducts.Api.Models.Exceptions
{
    public class ParallelLinesException : Exception
    {
        private readonly string _message;
        public ParallelLinesException(string message)
        {
            _message = message;
        }
    }
}
