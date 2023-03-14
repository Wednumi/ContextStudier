namespace ContextStudier.Core.Exceptions
{
    public class NotAllowedForRequesterException : Exception
    {
        public NotAllowedForRequesterException()
            :base("The requester is not allowed to perform the action")
        {            
        }
    }
}
