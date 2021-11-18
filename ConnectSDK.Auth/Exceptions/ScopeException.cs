namespace ConnectSDK.Auth.Exceptions
{
    using System;

    public class ScopeException : Exception
    {
        public ScopeException(string scope)
            : base("Error! Scope '" + scope + "' is not supported by the current token.")
        {
        }
    }
    }
