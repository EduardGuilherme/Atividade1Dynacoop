using atividadeSolution.Model;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividadeSolution.Controller
{
    public class ContaController
    {
        public IOrganizationService ServiceClient { get; set; }
        public Conta Conta { get; set; }

        public ContaController(IOrganizationService crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Conta = new Conta(ServiceClient);
        }

        public Guid Create(string nome, int nmtotal, int value, decimal money,string cnpj, string resposta)//
        {
            return Conta.CreateAccount(nome,nmtotal,value,money,cnpj, resposta);//
        }
       
        public Entity GetAccountByName(string name)
        {
            return Conta.GetAccountByName(name);
        }

        public Entity validateDoc(string cnpj)
        {
            return Conta.validateDoc(cnpj);
        }
        public Entity GetAccountById(Guid id)
        {
            return Conta.GetAccountById(id);
        }
        public Entity GetAccountById(Guid id, string[] columns)
        {
            return Conta.GetAccountById(id, columns);
        }
        public void IncrementOrDecrementNumberOfOpp(Entity oppAccount, bool? incrementOrDecrement)
        {
            Conta.IncrementOrDecrementNumberOfOpp(oppAccount, incrementOrDecrement);
        }
    }
}
