namespace ContaCorrente.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Usuários
            Contas LipeJipe = new Contas();
            LipeJipe.saldo = 0;
            LipeJipe.numero = 0;
            LipeJipe.limite = 0;
            LipeJipe.movimentacoes = new Movimentacao[10];
            Contas PedroPedra = new Contas();
            LipeJipe.saldo = 0;
            LipeJipe.numero = 0;
            LipeJipe.limite = 0;
            LipeJipe.movimentacoes = new Movimentacao[10];

            Contas BenjaminTennisson = new Contas();
            LipeJipe.saldo = 0;
            LipeJipe.numero = 0;
            LipeJipe.limite = 0;
            LipeJipe.movimentacoes = new Movimentacao[10];
           
            bool sucesso = false;
            int usuario = 0;
            int operacao = 0;
            Contas contaSelecionada = new Contas();

            do
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Selecione o Usuário");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1 - LipeJipe");
                Console.WriteLine("2 - PedroPedra");
                Console.WriteLine("3 - BenjaminTenisson");
                string entrada = Console.ReadLine()!;
                sucesso = int.TryParse(entrada, out usuario);

                if(!sucesso || usuario != 1 && usuario != 2 && usuario != 3)
                {
                    Console.WriteLine("---------------------------------------------");
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

            do
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Selecione a Operação");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1 - Saque");
                Console.WriteLine("2 - Depósito");
                Console.WriteLine("3 - Transfência");
                Console.WriteLine("4 - Exibir Extrato");
                string entrada = Console.ReadLine()!;
                sucesso = int.TryParse(entrada, out operacao);

                if (!sucesso || operacao != 1 && operacao != 2 && operacao != 3 && operacao != 4)
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Erro! Insira uma opção válida");
                    Console.WriteLine("---------------------------------------------");
                    continue;
                }
                if (operacao == 1)
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine($"Saldo: {contaSelecionada.saldo} + {contaSelecionada.limite}(Limite)");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Selecione a quantia que deseja sacar");
                    Console.WriteLine("---------------------------------------------");

                    double quantia = Convert.ToDouble(Console.ReadLine());
                    contaSelecionada.Sacar(quantia);
                }
    
                else if (operacao == 2)
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine($"Saldo: {contaSelecionada.saldo} + {contaSelecionada.limite}(Limite)");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Selecione a quantia que deseja depositar");
                    Console.WriteLine("---------------------------------------------");

                    double quantia = Convert.ToDouble(Console.ReadLine());
                    contaSelecionada.Depositar(quantia);
                }
                else if (operacao == 3)
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine($"Saldo: {contaSelecionada.saldo} + {contaSelecionada.limite}(Limite)");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Selecione a quantia que deseja trasnferir");
                    Console.WriteLine("---------------------------------------------");
                    double quantia = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Para qual conta deseja transferir?");
                    Console.WriteLine("---------------------------------------------");
                    int beneficiado = Convert.ToInt32(Console.ReadLine());

                    //if beneficiado => Fulano.contaDestino

                    contaSelecionada.TransferirPara(beneficiado, quantia);

                }
                   


            } while (!sucesso || usuario != 1 && usuario != 2 && usuario != 3);





        }
    }   
}
