using System;
using minibanco_dotnet.Exceptions;

namespace minibanco_dotnet.Models
{
    public class ContaPoupanca : Conta
    {
        public double TaxaRendimentoMensal { get; set; }
        public int DiaAniversario { get; set; }

        public ContaPoupanca(int numero, string titular, double taxaRendimentoMensal, int diaAniversario) : base(numero, titular)
        {
            this.TaxaRendimentoMensal = taxaRendimentoMensal;
            this.DiaAniversario = diaAniversario;
        }

        public override void AplicarFechamentoMensal()
        {
            // O rendimento é calculado sobre o saldo atual: saldo * taxa / 100
            if (this.Saldo > 0)
            {
                decimal rendimento = this.Saldo * (decimal)(this.TaxaRendimentoMensal / 100.0);
                
                // Usamos o método Depositar para atualizar o saldo e registrar no extrato
                Depositar(rendimento, "Rendimento mensal");
            }
        }
    }
}