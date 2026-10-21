using System;
using System.Collections.Generic;
using minibanco_dotnet.Exceptions;

namespace minibanco_dotnet.Models
{
    public class ContaCorrente : Conta
    {
        public decimal LimiteChequeEspecial { get; set; }

        public ContaCorrente(int numero, string titular, decimal limiteChequeEspecial) : base(numero, titular)
        {
            this.LimiteChequeEspecial = limiteChequeEspecial;
        }

        public override void Sacar(decimal valor, string descricao)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            // Na Conta Corrente, o cliente pode usar o Saldo + o Limite do Cheque Especial
            decimal saldoDisponivel = this.Saldo + this.LimiteChequeEspecial;

            if (valor > saldoDisponivel)
            {
                throw new SaldoInsuficienteException("Saldo e limite de cheque especial insuficientes para realizar o saque.");
            }

            RegistrarTransacao(descricao, "Saque", valor);
            this.Saldo -= valor;
        }

        public override void AplicarFechamentoMensal()
        {
            decimal tarifaManutencao = 10.00m;
            
            // A tarifa desconta do saldo e é registrada no extrato
            this.Saldo -= tarifaManutencao;
            RegistrarTransacao("Tarifa de manutenção", "Tarifa", tarifaManutencao);
        }
    }
}