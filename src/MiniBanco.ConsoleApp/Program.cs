using System;
using System.Collections.Generic;
using minibanco_dotnet.Models;
using minibanco_dotnet.Exceptions;
using minibanco_dotnet.Services;

namespace minibanco_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Conta> contas = new List<Conta>();
            bool executando = true;

            // RF11: Menu principal em um laço de repetição contínuo
            while (executando)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("====================================================================");
                    Console.WriteLine("        MINIBANCO .NET - MOTOR DE GESTÃO DE CONTAS");
                    Console.WriteLine("====================================================================");
                    Console.WriteLine("1. Abrir Nova Conta");
                    Console.WriteLine("2. Registrar Movimentação (Depósito ou Saque)");
                    Console.WriteLine("3. Aplicar Fechamento Mensal");
                    Console.WriteLine("4. Exibir Relatório das Contas");
                    Console.WriteLine("0. Sair");
                    Console.WriteLine("--------------------------------------------------------------------");
                    Console.Write("Escolha uma opção: ");

                    string opcao = Console.ReadLine();
                    Console.WriteLine();

                    switch (opcao)
                    {
                        case "1":
                            AbrirNovaConta(contas);
                            break;
                        case "2":
                            RegistrarMovimentacao(contas);
                            break;
                        case "3":
                            AplicarFechamentoMensal(contas);
                            break;
                        case "4":
                            ExibirRelatorios(contas);
                            break;
                        case "0":
                            executando = false;
                            Console.WriteLine("Encerrando a aplicação. Obrigado!");
                            break;
                        default:
                            Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
                // RF12: Tratamento de exceções no menu principal (impedindo que o programa feche abruptamente)
                catch (SaldoInsuficienteException ex)
                {
                    Console.WriteLine($"\n[ERRO DE SALDO] {ex.Message}");
                    Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
                    Console.ReadKey();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO] {ex.Message}");
                    Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
                    Console.ReadKey();
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n[ERRO DE FORMATO] Você digitou um caractere inválido onde se esperava um número.");
                    Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO INESPERADO] {ex.Message}");
                    Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
                    Console.ReadKey();
                }
            }
        }

        static void AbrirNovaConta(List<Conta> contas)
        {
            Console.WriteLine("--- ABRIR NOVA CONTA ---");
            Console.WriteLine("Selecione o tipo de conta:");
            Console.WriteLine("1. Conta Corrente");
            Console.WriteLine("2. Conta Poupança");
            Console.WriteLine("3. Conta Salário");
            Console.Write("Opção: ");
            string tipo = Console.ReadLine();

            Console.Write("Número da Conta (ex: 1001): ");
            int numero = int.Parse(Console.ReadLine());

            Console.Write("Nome do Titular: ");
            string titular = Console.ReadLine();

            if (tipo == "1")
            {
                Console.Write("Limite de Cheque Especial (R$): ");
                decimal limite = decimal.Parse(Console.ReadLine());

                ContaCorrente cc = new ContaCorrente(numero, titular, limite);
                contas.Add(cc);
                Console.WriteLine("\n[Sucesso] Conta Corrente aberta com sucesso!");
            }
            else if (tipo == "2")
            {
                Console.Write("Taxa de Rendimento Mensal (% ex: 0.5): ");
                double taxa = double.Parse(Console.ReadLine());

                Console.Write("Dia de Aniversário (ex: 10): ");
                int dia = int.Parse(Console.ReadLine());

                ContaPoupanca cp = new ContaPoupanca(numero, titular, taxa, dia);
                contas.Add(cp);
                Console.WriteLine("\n[Sucesso] Conta Poupança aberta com sucesso!");
            }
            else if (tipo == "3")
            {
                Console.Write("Nome do Empregador: ");
                string empregador = Console.ReadLine();

                Console.Write("Limite de Saques Mensais Gratuitos: ");
                int limiteSaques = int.Parse(Console.ReadLine());

                ContaSalario cs = new ContaSalario(numero, titular, empregador, limiteSaques);
                contas.Add(cs);
                Console.WriteLine("\n[Sucesso] Conta Salário aberta com sucesso!");
            }
            else
            {
                Console.WriteLine("\n[Aviso] Tipo de conta inválido. Operação cancelada.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        static void RegistrarMovimentacao(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Não há contas cadastradas no banco no momento.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("--- REGISTRAR MOVIMENTAÇÃO ---");
            Console.Write("Digite o número da conta: ");
            int numero = int.Parse(Console.ReadLine());

            Conta contaEncontrada = null;
            // Varredura tradicional por laço (RF09)
            foreach (var c in contas)
            {
                if (c.Numero == numero)
                {
                    contaEncontrada = c;
                    break;
                }
            }

            if (contaEncontrada == null)
            {
                Console.WriteLine("\n[Aviso] Conta não encontrada!");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Conta localizada: {contaEncontrada.Titular} ({contaEncontrada.GetType().Name})");
            Console.WriteLine("Operação:");
            Console.WriteLine("1. Depósito");
            Console.WriteLine("2. Saque");
            Console.Write("Escolha (1 ou 2): ");
            string op = Console.ReadLine();

            Console.Write("Descrição da movimentação (ex: Salário, Aluguel): ");
            string descricao = Console.ReadLine();

            Console.Write("Valor (R$): ");
            decimal valor = decimal.Parse(Console.ReadLine());

            if (op == "1")
            {
                contaEncontrada.Depositar(valor, descricao);
                Console.WriteLine("\n[Sucesso] Depósito efetuado com sucesso!");
            }
            else if (op == "2")
            {
                contaEncontrada.Sacar(valor, descricao);
                Console.WriteLine("\n[Sucesso] Saque efetuado com sucesso!");
            }
            else
            {
                Console.WriteLine("\n[Aviso] Operação desconhecida.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        static void AplicarFechamentoMensal(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada para aplicar fechamento.");
            }
            else
            {
                // Demonstração de polimorfismo puro: chamando AplicarFechamentoMensal() 
                // sem verificar o tipo da conta
                foreach (var conta in contas)
                {
                    conta.AplicarFechamentoMensal();
                }
                Console.WriteLine("[Sucesso] Fechamento mensal aplicado em todas as contas do banco!");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        static void ExibirRelatorios(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta aberta até o momento.");
            }
            else
            {
                foreach (var conta in contas)
                {
                    MotorAnalise.ProcessarEExibirRelatorio(conta);
                    Console.WriteLine("\nPressione qualquer tecla para ver a próxima conta (ou voltar)...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Pressione qualquer tecla para retornar ao menu principal...");
            Console.ReadKey();
        }
    }
}