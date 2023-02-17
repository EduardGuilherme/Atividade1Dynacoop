using System;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atividadeSolution.Controller;
using System.Runtime.CompilerServices;

namespace atividadeSolution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();
            ContaController contaController = new ContaController(serviceClient);

            Console.WriteLine("Bem vindo!");
            Menu();
            var opcao = Console.ReadLine();
            while (opcao != "0") 
            {
                
                if (opcao == "1")
                {
                    Console.WriteLine("Função de criação!");
                    CreateNewAccount(contaController);
                }
                else
                {
                    if(opcao == "2")
                    {
                        Console.WriteLine("Função De busca");
                        SearchContact(contaController);
                    }
                }
                Menu();
                break;
            }
            

            Console.ReadKey();
        }
        public static void Menu()
        {
            Console.WriteLine("Digite Sua Opção");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("1 - Criar Conta/Contato!");
            Console.WriteLine("2 - Pesquisar Contato");
            Console.WriteLine("-----------------------------");
        }
        public static void CreateNewAccount(ContaController contaController)
        {
            
            Console.WriteLine("Digite o nome da Conta");
            var nome = Console.ReadLine();
            Console.WriteLine("Digite o numero total de lojas");
            var nmTotal = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o id do Tipo de Cliente");
            var tipoCliente = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o valor total de vendas");
            var vltotaldevendas = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Digite o Cnpj");
            var cnpj = Console.ReadLine();
            //ValidadteDoc(contaController, cnpj);

            Console.WriteLine("Deseja Criar um contato para esta conta? S/N ");
            var resposta = Console.ReadLine();

            
            contaController.Create(nome, nmTotal, tipoCliente, vltotaldevendas, cnpj, resposta);

            /*if (resposta.ToUpper() == "S")
            {
                Guid idPrimario = Guid.NewGuid();
                Console.WriteLine("Digite o Nome do Contato");
                var nomeContato = Console.ReadLine();   
                contaController.CreateContact(idPrimario,nomeContato);
                contaController.Create(nome, nmTotal, tipoCliente, vltotaldevendas, resposta, nomeContato);

            }
            else
            {
                if(resposta.ToUpper() == "N"){
                    var nomeContato = "";
                    var idPrimario = new Guid();
                    contaController.Create(nome, nmTotal, tipoCliente, vltotaldevendas, resposta, nomeContato);
                }
                    
            }*/

        }
        public static void SearchContact(ContaController contaController)
        {
            Console.WriteLine("Qual o nome do contato relacionado a conta que você deseja pesquisar");
            var name = Console.ReadLine();
            Entity account = contaController.GetAccountByName(name);

            Console.WriteLine($"O telefone da conta recuperada é {account["firstname"]}");
        }
        public static void ValidadteDoc(ContaController contaController,string cnpj)
        {
            Console.WriteLine("Validando documento");
            //var cnpj = Console.ReadLine();
            Entity account = contaController.validateDoc(cnpj);
            if (account["atv_cnpj"].ToString() != "")
            {
                if (account["atv_cnpj"].Equals(cnpj))
                {
                    Console.WriteLine("O cnpj digitado já existe!");
                    Console.ReadKey();
                }
                else
                {

                }
                
            }
            Console.WriteLine($"O Cnpj da conta recuperada é {account["cnpj"]}");
        }

    }
}
