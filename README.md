# MiniBanco .NET — Motor de Gestão de Contas Bancárias

Aplicação de console desenvolvida em C# com .NET para gerenciar contas bancárias, registrar movimentações e gerar uma análise da saúde financeira de cada conta.

> **Projeto acadêmico:** Mini-Projeto Avaliativo — Desenvolvedor Back-End [.NET], Módulo 2, Semana 07.

## Objetivo

Construir um sistema bancário simples aplicando os fundamentos de C# e Programação Orientada a Objetos (POO). A aplicação permite trabalhar com diferentes tipos de conta, registrar depósitos e saques, executar o fechamento mensal e gerar relatórios com pontuação, classificação e recomendações para a gerência.

## Funcionalidades previstas

- Abertura de contas correntes, poupanças e contas salário.
- Registro de depósitos e saques.
- Armazenamento das movimentações no extrato de cada conta.
- Aplicação das regras específicas de cada tipo de conta no fechamento mensal.
- Cálculo da pontuação e do índice de saúde financeira.
- Classificação da conta como **Conta Saudável**, **Conta em Atenção** ou **Conta em Risco**.
- Exibição de tarifas e saques que precisam de acompanhamento.
- Geração de recomendações para a gerência.
- Menu interativo no terminal.
- Tratamento de entradas inválidas e operações não permitidas.

## Tipos de conta e regras de negócio

### Conta Corrente (`ContaCorrente`)

- Possui limite de cheque especial.
- Permite saque até o saldo disponível somado ao limite de cheque especial.
- No fechamento mensal, registra tarifa de manutenção de **R$ 10,00**.

### Conta Poupança (`ContaPoupanca`)

- Possui taxa de rendimento mensal e dia de aniversário.
- No fechamento mensal, calcula o rendimento pela fórmula:

  `rendimento = saldo × taxa de rendimento / 100`

- O rendimento é registrado como uma transação do tipo `Deposito`.

### Conta Salário (`ContaSalario`)

- Possui empregador e limite de saques mensais.
- Quando o saque ultrapassa o limite mensal, registra também uma tarifa de **R$ 5,00**.
- No fechamento mensal, reinicia a contagem de saques do mês.
- Não cobra tarifa de manutenção no fechamento mensal.

### Transações (`Transacao`)

Cada transação contém uma descrição, um tipo e um valor. Os tipos aceitos pela especificação são:

- `Deposito`
- `Saque`
- `Tarifa`

O valor da transação deve ser maior que zero. Tipos ou valores inválidos devem ser tratados com exceção.

## Índice de saúde financeira

Cada transação contribui para a pontuação da conta:

| Tipo de transação | Pontuação |
|---|---:|
| `Deposito` | 10 pontos |
| `Saque` | 5 pontos |
| `Tarifa` | 0 pontos |

A pontuação máxima corresponde ao número de transações multiplicado por 10.

**Fórmula do índice de saúde:**

`Índice (%) = (pontuação obtida / pontuação máxima possível) × 100`

O sistema deve tratar contas sem transações para evitar divisão por zero.

### Classificação

| Índice de saúde | Classificação | Ação recomendada |
|---|---|---|
| 90% a 100% | **Conta Saudável** | Oferecer investimentos e avaliar aumento de limite. |
| 60% a 89% | **Conta em Atenção** | Orientar o cliente e revisar tarifas. |
| 0% a 59% | **Conta em Risco** | Não aumentar o limite e abrir análise de crédito. |

O relatório também deve destacar tarifas como itens críticos e saques como itens de atenção, com recomendações adequadas às movimentações encontradas.

## Tecnologias e conceitos

- **C#**
- **.NET Console Application**
- Programação Orientada a Objetos
- Classes, objetos, propriedades e construtores
- Encapsulamento
- Herança e polimorfismo (`virtual` / `override`)
- Coleções `List<T>`
- Estruturas condicionais e laços de repetição (`if/else`, `switch`, `for`, `foreach`, `while`)
- Tratamento de exceções (`try/catch`)
- Git e GitHub

De acordo com o enunciado, as buscas, filtragens e cálculos das listas devem usar laços tradicionais (`for` ou `foreach`), sem LINQ.

## Estrutura do projeto

A estrutura sugerida para a solução é:

```text
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
```

## Pré-requisitos

- Git instalado.
- SDK do .NET compatível com o projeto.
- Terminal, PowerShell ou outro ambiente de linha de comando.

Para conferir se o .NET está instalado:

```bash
dotnet --version
```

## Como executar

### 1. Clonar o repositório

```bash
git clone https://github.com/NicolasOripkaStaskoviak/minibanco-dotnet.git
cd minibanco-dotnet
```

### 2. Restaurar as dependências

```bash
dotnet restore src/MiniBanco.ConsoleApp/MiniBanco.ConsoleApp.csproj
```

### 3. Executar a aplicação

```bash
dotnet run
```

Ao iniciar, a aplicação deverá apresentar o menu principal para abrir contas, registrar movimentações, aplicar o fechamento mensal, consultar relatórios e sair do sistema.

## Menu da aplicação

O menu especificado para o projeto contém as seguintes opções:

1. **Abrir Nova Conta** — escolher o tipo de conta e informar os dados necessários.
2. **Registrar Movimentação** — localizar uma conta e registrar depósito ou saque.
3. **Aplicar Fechamento Mensal** — executar as regras de fechamento de cada conta.
4. **Exibir Relatório das Contas** — consultar movimentações, pontuação, índice de saúde, classificação e pendências.
0. **Sair** — encerrar a aplicação.

Entradas inválidas, valores inadequados e saques não permitidos devem gerar mensagens claras, sem encerrar inesperadamente o programa.

## Verificações recomendadas

Ao validar a aplicação, confira pelo menos os seguintes cenários:

- [ ] Abrir uma conta de cada tipo.
- [ ] Registrar depósitos e saques válidos.
- [ ] Tentar realizar um saque não permitido.
- [ ] Informar um valor inválido ou texto onde se espera um número.
- [ ] Aplicar o fechamento mensal em cada tipo de conta.
- [ ] Verificar a tarifa da conta corrente.
- [ ] Verificar o rendimento da poupança.
- [ ] Verificar a tarifa e a reinicialização da contagem de saques da conta salário.
- [ ] Conferir o cálculo da pontuação e do índice de saúde.
- [ ] Conferir as três classificações possíveis.
- [ ] Consultar o relatório quando não houver contas cadastradas.
- [ ] Confirmar que o menu continua disponível após uma entrada inválida.

## Demonstração em vídeo

**Link do vídeo:** https://www.youtube.com/watch?v=DO6AhqO0jHk

---

Projeto desenvolvido para fins educacionais como parte do mini-projeto avaliativo de Back-End com .NET.
