using minibanco_dotnet.Exceptions;

namespace minibanco_dotnet.Models
{
    public abstract class Conta
    {
        public int Numero { get; set; }
        public string Titular { get; set; }
        public decimal Saldo { get; protected set; }
        public List<Transacao> Extrato { get; set; }

        public void RegistrarTransacao(string descricao, string tipo, decimal valor)
        {
            Transacao transacao = new Transacao(descricao, tipo, valor);
            this.Extrato.Add(transacao);
        }

        public void Depositar(decimal valor, string descricao)
        {
            RegistrarTransacao(descricao, "Deposito", valor);
            this.Saldo += valor;    
        }

        public void Sacar(decimal valor, string descricao)
        {
            if (valor > this.Saldo)
            {
                throw new SaldoInsuficienteException("Saldo insuficiente para realizar o saque.");
            }
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            RegistrarTransacao(descricao, "Saque", valor);
            this.Saldo -= valor;
        }   

        public Conta(int numero, string titular)
        {
            this.Numero = numero;
            this.Titular = titular;
            this.Saldo = 0m;
            this.Extrato = new List<Transacao>();
        }

    }
}