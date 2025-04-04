using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.ConsoleApp
{
    public class Contas
    {
        public string nome;
        public double saldo;
        public double saldoTotal;
        public double limite;        
        public double limiteInicial; 
        public int numero;
        public Movimentacao[] movimentacoes;

        public void Sacar(double quantia)
        {
            if (quantia <= saldo + limite)
            {
                if (quantia <= saldo)
                {
                    saldo -= quantia;
                }
                else
                {
                    double restante = quantia - saldo;
                    saldo = 0;
                    limite -= restante;
                }

                saldoTotal = saldo + limite;

                Movimentacao movimentacao = new Movimentacao();
                movimentacao.valor = quantia;
                movimentacao.tipo = "Débito";
                movimentacao.descricao = "Débito de R$" + quantia + " reais";

                int posicaoVazia = PegaPosicaoVazia();
                if (posicaoVazia != -1)
                    movimentacoes[posicaoVazia] = movimentacao;
            }
            else
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Erro! Saldo insuficiente");
                Console.WriteLine("---------------------------------------------");
                Thread.Sleep(1000);
            }
        }

        public void Depositar(double quantia)
        {
            saldo += quantia;

            Movimentacao movimentacao = new Movimentacao();
            movimentacao.valor = quantia;
            movimentacao.tipo = "Crédito";
            movimentacao.descricao = "Crédito de R$" + quantia + " reais";

            int posicaoVazia = PegaPosicaoVazia();
            if (posicaoVazia != -1)
                movimentacoes[posicaoVazia] = movimentacao;
        }

        public void TransferirPara(Contas contaDestino, double quantia)
        {
            if (contaDestino == null || contaDestino == this)
            {
                Console.WriteLine("Erro! Conta de destino inválida.");
                Thread.Sleep(1000);
                return;
            }

            if (quantia <= 0)
            {
                Console.WriteLine("Erro! A quantia deve ser maior que zero.");
                Thread.Sleep(1000);
                return;
            }

            if (quantia > saldo + limite)
            {
                Console.WriteLine("Erro! Saldo insuficiente para realizar a transferência.");
                Thread.Sleep(1000);
                return;
            }

            if (quantia <= saldo)
            {
                saldo -= quantia;
            }
            else
            {
                double restante = quantia - saldo;
                saldo = 0;
                limite -= restante;
            }

            saldoTotal = saldo + limite;
            if (saldo == 0 && limite < limiteInicial)
            {
                saldoTotal = -(limiteInicial - limite);
            }

            Movimentacao movimentacao = new Movimentacao();
            movimentacao.valor = quantia;
            movimentacao.tipo = "Débito";
            movimentacao.descricao = $"Transferência de R${quantia:F2} para {contaDestino.nome}";
            movimentacoes[PegaPosicaoVazia()] = movimentacao;

            contaDestino.DepositarTransferencia(quantia, this.nome);
        }

        public void ExibirExtrato()
        {
            ExibirSaldo();
            Console.WriteLine($"Extrato da conta:");
            Console.WriteLine("---------------------------------------------");

            bool temMovimentacoes = false;

            foreach (Movimentacao mov in movimentacoes)
            {
                if (mov != null)
                {
                    Console.WriteLine($"{mov.tipo} - {mov.descricao} - Valor: R${mov.valor:F2}");
                    temMovimentacoes = true;
                }
            }

            if (!temMovimentacoes)
            {
                Console.WriteLine("Nenhuma movimentação registrada.");
            }

            if (saldo == 0 && limite < limiteInicial)
            {
                saldoTotal = -(limiteInicial - limite);
            }
            else
            {
                saldoTotal = saldo + limite;
            }

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"SALDO TOTAL ATUAL: R${saldoTotal:F2}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public int PegaPosicaoVazia()
        {
            for (int i = 0; i < movimentacoes.Length; i++)
            {
                if (movimentacoes[i] == null)
                    return i;
            }
            return -1;
        }

        public void ExibirSaldo()
        {
           
            if (saldo == 0 && limite < limiteInicial)
            {
                saldoTotal = -(limiteInicial - limite);
            }
            else
            {
                saldoTotal = saldo + limite;
            }

            Console.Clear();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"{nome}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"SALDO TOTAL: {saldoTotal:C2}R$");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Saldo: {saldo:C2} + {limite:C2} (Limite)");
            Console.WriteLine("---------------------------------------------");
        }

        public void DepositarTransferencia(double quantia, string remetente)
        {
            saldo += quantia;

            Movimentacao movimentacao = new Movimentacao();
            movimentacao.valor = quantia;
            movimentacao.tipo = "Crédito";
            movimentacao.descricao = $"Recebido R${quantia:F2} de {remetente}";

            int posicaoVazia = PegaPosicaoVazia();
            movimentacoes[posicaoVazia] = movimentacao;
        }
    }
}
