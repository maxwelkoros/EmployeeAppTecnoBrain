using System;
using System.Runtime.Serialization;

namespace EmpApp
{
    [Serializable]
    public class ValidationException : Exception
    {
        private string _Message;

        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
            _Message = message;
        }
        public override string Message
        {
            get { return _Message; }
        }

       
    }
}