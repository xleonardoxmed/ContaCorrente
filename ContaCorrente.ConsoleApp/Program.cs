﻿namespace ContaCorrente.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Usuários
            Contas LipeJipe = new Contas();
            LipeJipe.nome = "LipeJipe";
            LipeJipe.saldo = 5000;
            LipeJipe.numero = 01;
            LipeJipe.limite = 2000;
            LipeJipe.movimentacoes = new Movimentacao[10];

            Contas PedroPedra = new Contas();
            PedroPedra.nome = "PedroPedra";
            PedroPedra.saldo = 1000;
            PedroPedra.numero = 02;
            PedroPedra.limite = 100;
            PedroPedra.movimentacoes = new Movimentacao[10];

            Contas BenjaminTennisson = new Contas();
            BenjaminTennisson.nome = "BenjaminTenisson";
            BenjaminTennisson.saldo = 50;
            BenjaminTennisson.numero = 03;
            BenjaminTennisson.limite = 100;
            BenjaminTennisson.movimentacoes = new Movimentacao[10];

            bool sucesso = false;
            int usuario = 0;
            int operacao = 0;
            Contas contaSelecionada = new Contas();
            Contas[] ContasPossiveis = [LipeJipe, PedroPedra, BenjaminTennisson];

            while (true)
            {
   
                do
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Selecione o Usuário");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("1 - LipeJipe");
                    Console.WriteLine("2 - PedroPedra");
                    Console.WriteLine("3 - BenjaminTenisson");
                    Console.WriteLine("---------------------------------------------");
                    string entrada = Console.ReadLine()!;
                    sucesso = int.TryParse(entrada, out usuario);

                    if (!sucesso || usuario != 1 && usuario != 2 && usuario != 3)
                    {
                        Console.WriteLine("Erro! Insira uma opção válida");
                        Console.WriteLine("---------------------------------------------");
                        continue;
                    }
                    if (usuario == 1)
                        contaSelecionada = LipeJipe;
                    else if (usuario == 2)
                        contaSelecionada = PedroPedra;
                    else
                        contaSelecionada = BenjaminTennisson;

                } while (!sucesso || usuario != 1 && usuario != 2 && usuario != 3);

                while (operacao != '5')
                {
                    do
                    {
                        contaSelecionada.ExibirSaldo();
                        Console.WriteLine("Selecione a Operação");
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine("1 - Saque");
                        Console.WriteLine("2 - Depósito");
                        Console.WriteLine("3 - Transfência");
                        Console.WriteLine("4 - Exibir Extrato");
                        Console.WriteLine("5 - Sair da Conta");
                        string entrada = Console.ReadLine()!;
                        sucesso = int.TryParse(entrada, out operacao);

                        if (!sucesso || operacao != 1 && operacao != 2 && operacao != 3 && operacao != 4 && operacao != 5)
                        {
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine("Erro! Insira uma opção válida");
                            Console.WriteLine("---------------------------------------------");
                            continue;
                        }

                    } while (!sucesso || usuario != 1 && usuario != 2 && usuario != 3 && operacao != 4 && operacao != 5);

                    if (operacao == 1)
                    {
                        contaSelecionada.ExibirSaldo();
                        Console.WriteLine("Selecione a quantia que deseja sacar");
                        Console.WriteLine("---------------------------------------------");

                        double quantia = Convert.ToDouble(Console.ReadLine());
                        contaSelecionada.Sacar(quantia);
                        contaSelecionada.ExibirSaldo();                      
                        continue;
                    }

                    else if (operacao == 2)
                    {
                        contaSelecionada.ExibirSaldo();
                        Console.WriteLine("Selecione a quantia que deseja depositar");
                        Console.WriteLine("---------------------------------------------");

                        double quantia = Convert.ToDouble(Console.ReadLine());
                        contaSelecionada.Depositar(quantia);
                        contaSelecionada.ExibirSaldo();
                        continue;
                    }
                    else if (operacao == 3)
                    {
                        Contas[] ContasParaTransferir = ContasPossiveis.Where(c => c != contaSelecionada).ToArray();

                        contaSelecionada.ExibirSaldo();
                        Console.WriteLine("Selecione a quantia que deseja transferir");
                        Console.WriteLine("---------------------------------------------");
                        double quantia = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine("Para qual conta deseja transferir?");
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine($"1 - {ContasParaTransferir[0].nome} ");
                        Console.WriteLine($"2 - {ContasParaTransferir[1].nome}");
                        Console.WriteLine("---------------------------------------------");

                        int beneficiado = Convert.ToInt32(Console.ReadLine());
                        Contas contaDestino = new Contas();
                        if (beneficiado == 1)
                            contaDestino = ContasParaTransferir[0];
                        else
                            contaDestino = ContasParaTransferir[1];

                        contaSelecionada.TransferirPara(contaDestino, quantia);
                        contaSelecionada.ExibirSaldo();
                        continue;

                    }

                    else
                    {
                        Console.Clear();
                        break;
                    }
                }
            }
        }
    }
}
