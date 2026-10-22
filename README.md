# MiniBanco .NET - Motor de Gestão de Contas

## 1. Objetivo
Aplicação desenvolvida em Console Application utilizando C# e Programação Orientada a Objetos (POO) para gerenciar contas bancárias de diferentes tipos (Corrente, Poupança e Salário), registrar movimentações financeiras, realizar fechamento mensal, calcular o índice de saúde financeira e emitir relatórios detalhados para a gerência.

## 2. Estrutura do Projeto
minibanco-dotnet/
├── src/
│   └── MiniBanco.ConsoleApp/
│       ├── Program.cs
│       ├── Models/
│       │   ├── Transacao.cs
│       │   ├── Conta.cs
│       │   ├── ContaCorrente.cs
│       │   ├── ContaPoupanca.cs
│       │   └── ContaSalario.cs
│       ├── Exceptions/
│       │   └── SaldoInsuficienteException.cs
│       ├── Services/
│       │   └── MotorAnalise.cs
│       └── MiniBanco.ConsoleApp.csproj
└── README.md

## 3. Funcionalidades e Regras de Negócio
- Transacoes: Validação estrita de tipos (Deposito, Saque, Tarifa) e valores estritamente positivos, lançando exceções em caso de dados inválidos.
- Tipos de Conta:
  - Conta Corrente: Possui limite de cheque especial e tarifa fixa de manutenção mensal.
  - Conta Poupança: Possui taxa de rendimento percentual mensal e dia de aniversário.
  - Conta Salário: Possui controle de limite de saques gratuitos por mês e cobrança de tarifa por excedente.
- Motor de Analise: Varredura de extrato utilizando laços tradicionais (sem LINQ) para calcular a pontuação obtida, o índice de saúde financeira (%) e a classificação da conta (Saudável, Em Atenção ou Em Risco).
- Tratamento de Excecoes: Proteção do menu principal contra exceções de saldo insuficiente, argumentos inválidos e erros de formato de digitação.

## 4. Instrucoes de Execucao
Para executar o sistema via terminal:

1. Abra o terminal na raiz do repositório.
2. Navegue até o diretório do projeto:
   cd src/MiniBanco.ConsoleApp
3. Execute a aplicação:
   dotnet run
