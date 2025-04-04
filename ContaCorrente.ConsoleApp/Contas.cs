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
        public int numero;
        public Movimentacao[] movimentacoes;

        public void Sacar(double quantia)
        {
            if (quantia <= saldoTotal)
            {
                if (quantia == saldoTotal)
                {
                    double limiteUsado = limite;

                    saldo = 0;
                    limite = 0;
                    saldoTotal = -limiteUsado;

                }
                else if(quantia > saldo)
                {
                    limite = limite - (quantia - saldo);
                    saldo = 0;
                }
                else if (saldo > 0)
                {
                    double novoSaldo = saldo - quantia;
                    saldo = novoSaldo;
                }
                else
                {
                    double novoSaldo = limite - quantia;
                    limite = novoSaldo;
                }
                    Movimentacao movimentacao = new Movimentacao();

                movimentacao.valor = quantia;
                movimentacao.tipo = "Débito";
                movimentacao.descricao = "Débito de R$" + quantia + " reais";

                int posicaoVazia = PegaPosicaoVazia();

                movimentacoes[posicaoVazia] = movimentacao;
            }
            else
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Erro! Saldo insuficiente");
                Console.WriteLine("---------------------------------------------");
                Thread.Sleep(1000);
                return;
            }
        }

        public void Depositar(double quantia)
        {
            double novoSaldo = saldo + quantia;
            saldo = novoSaldo;
            Movimentacao movimentacao = new Movimentacao();

            movimentacao.valor = quantia;
            movimentacao.tipo = "Crédito";
            movimentacao.descricao = "Crédito de R$" + quantia + " reais";

            int posicaoVazia = PegaPosicaoVazia();

            movimentacoes[posicaoVazia] = movimentacao;

        }
        public void TransferirPara(Contas contaDestino, double quantia)
        {
            this.Sacar(quantia);
            contaDestino.Depositar(quantia);
        }

        public void ExibirExtrato()
        {
            Console.WriteLine($"{saldo} + {limite}");
        }
        public int PegaPosicaoVazia()
        {
            for(int i = 0; i < movimentacoes.Length; i++)
            {
                if (movimentacoes[i] == null)
                    return i;
            }
            return -1;
        }
        public void ExibirSaldo()
        {
            saldoTotal = saldo + limite;


            Console.Clear();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"{nome}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"SALDO TOTAL: {saldoTotal:C2}R$");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Saldo: {saldo:C2} + {limite:C2}(Limite)");
            Console.WriteLine("---------------------------------------------");
        }
    }
}
