using System;
using minibanco_dotnet.Exceptions;

namespace minibanco_dotnet.Models
{
    public class ContaSalario : Conta
    {
        public string Empregador { get; set; }
        public int LimiteSaquesMensais { get; set; }
        private int saquesRealizadosNoMes;

        public ContaSalario(int numero, string titular, string empregador, int limiteSaquesMensais) : base(numero, titular)
        {
            this.Empregador = empregador;
            this.LimiteSaquesMensais = limiteSaquesMensais;
            this.saquesRealizadosNoMes = 0;
        }

        public override void Sacar(decimal valor, string descricao)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            if (valor > this.Saldo)
            {
                throw new SaldoInsuficienteException("Saldo insuficiente para realizar o saque.");
            }

            // Efetua o saque normal
            this.Saldo -= valor;
            RegistrarTransacao(descricao, "Saque", valor);

            // Incrementa o contador de saques do mês
            this.saquesRealizadosNoMes++;

            // Se o saque ultrapassar o limite mensal gratuito, aplica a tarifa de R$ 5,00
            if (this.saquesRealizadosNoMes > this.LimiteSaquesMensais)
            {
                decimal tarifaExcedente = 5.00m;
                this.Saldo -= tarifaExcedente;
                RegistrarTransacao("Tarifa de saque excedente", "Tarifa", tarifaExcedente);
            }
        }

        public override void AplicarFechamentoMensal()
        {
            // Zera a contagem de saques do mês
            this.saquesRealizadosNoMes = 0;
        }
    }
}