namespace minibanco_dotnet.Exceptions
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException(string mensagem) : base(mensagem)
    {
    }
    }
}