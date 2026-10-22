using System;
using minibanco_dotnet.Models;

namespace minibanco_dotnet.Services
{
    public class MotorAnalise
    {
        public static void ProcessarEExibirRelatorio(Conta conta)
        {
            int totalTransacoes = 0;
            int pontosObtidos = 0;

            foreach (var transacao in conta.Extrato)
            {
                totalTransacoes++;

                if (transacao.Tipo == "Deposito")
                {
                    pontosObtidos += 10;
                }
                else if (transacao.Tipo == "Saque")
                {
                    pontosObtidos += 5;
                }
                else if (transacao.Tipo == "Tarifa")
                {
                    pontosObtidos += 0;
                }
            }

            // Cálculo do Índice de Saúde da Conta 
            int pontuacaoMaximaPossivel = totalTransacoes * 10;
            double indiceSaude = 100.0; 

            if (pontuacaoMaximaPossivel > 0)
            {

                indiceSaude = ((double)pontosObtidos / pontuacaoMaximaPossivel) * 100;
            }

            string classificacao = "";
            string acaoCorporativa = "";

            if (indiceSaude >= 90.0)
            {
                classificacao = "[ CONTA SAUDÁVEL ]";
                acaoCorporativa = "Oferecer investimentos e avaliar aumento de limite.";
            }
            else if (indiceSaude >= 60.0)
            {
                classificacao = "[ CONTA EM ATENÇÃO ]";
                acaoCorporativa = "Contato da gerência para orientação financeira e revisão de tarifas.";
            }
            else
            {
                classificacao = "[ CONTA EM RISCO ]";
                acaoCorporativa = "Bloquear aumento de limite e abrir análise de crédito.";
            }


            Console.WriteLine("====================================================================");
            Console.WriteLine("        MINIBANCO .NET - MOTOR DE GESTÃO DE CONTAS");
            Console.WriteLine("====================================================================\n");

            Console.WriteLine("> DADOS DA CONTA:");
            Console.WriteLine($"  - Tipo: {conta.GetType().Name}");
            Console.WriteLine($"  - Titular: {conta.Titular} | Conta: {conta.Numero}");
            

            if (conta is ContaCorrente cc)
            {
                Console.WriteLine($"  - Atributo Específico: Limite Cheque Especial: R$ {cc.LimiteChequeEspecial:F2}");
            }
            else if (conta is ContaPoupanca cp)
            {
                Console.WriteLine($"  - Atributo Específico: Rendimento: {cp.TaxaRendimentoMensal}% ao mês | Aniversário: dia {cp.DiaAniversario}");
            }
            else if (conta is ContaSalario cs)
            {
                Console.WriteLine($"  - Atributo Específico: Empregador: {cs.Empregador} | Limite Saques: {cs.LimiteSaquesMensais}");
            }

            Console.WriteLine($"  - Saldo Atual: R$ {conta.Saldo:F2}\n");

            Console.WriteLine($"> MOVIMENTAÇÕES REGISTRADAS ({totalTransacoes} TRANSAÇÕES):");
            if (totalTransacoes == 0)
            {
                Console.WriteLine("  (Nenhuma transação registrada)");
            }
            else
            {
                foreach (var t in conta.Extrato)
                {
                    string statusTag = t.Tipo == "Deposito" ? "[OK]" : (t.Tipo == "Saque" ? "[ ! ]" : "[ X ]");
                    int pts = t.Tipo == "Deposito" ? 10 : (t.Tipo == "Saque" ? 5 : 0);
                    Console.WriteLine($"  {statusTag} {t.Descricao,-30} -------- {t.Tipo}: R$ {t.Valor:F2} ({pts} pts)");
                }
            }

            Console.WriteLine($"\n> RESUMO DA PONTUAÇÃO:");
            Console.WriteLine($"  - Pontuação Atingida: {pontosObtidos} de {pontuacaoMaximaPossivel} pontos possíveis");
            Console.WriteLine($"  - Índice de Saúde: {indiceSaude:F1}%");
            Console.WriteLine($"  - Classificação Final: {classificacao}\n");

            Console.WriteLine("> RELATÓRIO DE PENDÊNCIAS E RECOMENDAÇÕES DA GERÊNCIA:");
            

            bool temTarifa = false;
            bool temSaqueAlto = false;

            foreach (var t in conta.Extrato)
            {
                if (t.Tipo == "Tarifa")
                {
                    temTarifa = true;
                }
                if (t.Tipo == "Saque" && t.Valor > 1000.00m) 
                {
                    temSaqueAlto = true;
                }
            }

            if (temTarifa)
            {
                Console.WriteLine("  * ITENS CRÍTICOS (TARIFAS):");
                Console.WriteLine("    - Cobranças de tarifa identificadas. Avaliar isenção ou migração de pacote.");
            }

            if (temSaqueAlto || conta.Saldo < 0)
            {
                Console.WriteLine("  * ITENS DE ATENÇÃO (SAQUES / LIMITE):");
                Console.WriteLine("    - Movimentações de saque expressivas ou uso de limite detectadas. Entrar em contato com o cliente.");
            }

            if (!temTarifa && !temSaqueAlto && conta.Saldo >= 0)
            {
                Console.WriteLine("  * Nenhuma pendência crítica identificada. Ação corporativa: " + acaoCorporativa);
            }
            else
            {
                Console.WriteLine($"  * Ação Corporativa Recomendada: {acaoCorporativa}");
            }

            Console.WriteLine("===================================================================\n");
        }
    }
}