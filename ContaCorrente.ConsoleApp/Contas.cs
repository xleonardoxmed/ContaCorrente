using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.ConsoleApp
{
    public class Contas
    {
        public double saldo;
        public int limite;
        public int numero;
        public Movimentacao[] movimentacoes;

        public void Sacar(double quantia)
        {
            if (quantia < saldo + limite)
            {
                double novoSaldo = saldo - quantia;
                saldo = novoSaldo;
                Movimentacao movimentacao = new Movimentacao();

                movimentacao.valor = quantia;
                movimentacao.tipo = "Débito";
                movimentacao.descricao = "Débito de R$" + quantia + " reais";

                int posicaoVazia = PegaPosicaoVazia();

                movimentacoes[posicaoVazia] = movimentacao;
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
    }
}
