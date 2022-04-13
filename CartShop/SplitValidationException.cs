namespace CartShop
{
    [Serializable]
    public class SplitValidationException : Exception
    {
        public SplitValidationException() { }
        public SplitValidationException(string message) : base(message) { }
        public SplitValidationException(string message, Exception inner) : base(message, inner) { }
        protected SplitValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
