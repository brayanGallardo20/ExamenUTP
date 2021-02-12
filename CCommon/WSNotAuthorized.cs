using System;

namespace CCommon
{
    public class WSNotAuthorized : Exception
    {
        public WSNotAuthorized()
        {
        }

        public WSNotAuthorized(string message)
            : base(message)
        {
        }

        public WSNotAuthorized(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
