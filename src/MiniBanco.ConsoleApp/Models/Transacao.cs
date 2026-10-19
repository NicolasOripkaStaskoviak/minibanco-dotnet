namespace minibanco_dotnet.Models
{
    public class Transacao
    {
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }

        public Transacao(string descricao, string tipo, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor da transação deve ser maior que zero.");
            }

            if (tipo != "Deposito" && tipo != "Saque" && tipo != "Tarifa")
            {
                throw new ArgumentException("Tipo de transação inválido. Permite-se apenas: Deposito, Saque ou Tarifa.");
            }

            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
        }
    }
}