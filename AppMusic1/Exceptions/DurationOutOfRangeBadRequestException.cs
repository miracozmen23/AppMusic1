namespace AppMusic1.Exceptions
{
    public class DurationOutOfRangeBadRequestException : BadRequestException
    {
        public DurationOutOfRangeBadRequestException() : 
            base("Maximum duration should be less than 600 seconds and greater than 60 seconds.")
        {          
        }
    }
}
