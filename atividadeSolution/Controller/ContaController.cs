﻿using atividadeSolution.Model;
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
        public CrmServiceClient ServiceClient { get; set; }
        public Conta Conta { get; set; }

        public ContaController(CrmServiceClient crmServiceCliente)
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
    }
}
