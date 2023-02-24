using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Activities.Presentation.Debug;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace atividadeSolution.Model
{
    public class Conta
    {
        public IOrganizationService serviceClient { get; set; }

        public Conta(IOrganizationService crmServiceClient)
        {
            this.serviceClient = crmServiceClient;  
        }

        public Conta(CrmServiceClient crmServiceClient) 
        {
            this.serviceClient = crmServiceClient;
        }

        public Guid CreateAccount(string nome, int nmtotal, int value, decimal money,string cnpj, string resposta)//
        {
            //Console.WriteLine("Iniciando a Criação de Conta");
            
            Entity conta = new Entity("account");
            conta["name"] = nome;
            conta["atv_numerototaldelojas"] = nmtotal;
            conta["atv_tipodecliente"] = new OptionSetValue(value);//751550000
            conta["atv_valortotalderenda"] = new Money(money);
            conta["atv_cnpj"] = cnpj;
            Guid accoutId = serviceClient.Create(conta);

            //var resposta = Console.ReadLine();

            if (resposta.ToUpper() == "S")
            {
                Console.WriteLine("Digite o Nome do Contato");
                var nomeContato = Console.ReadLine();//

                Console.WriteLine("Digite o Cpf do Contato");
                var cpf = Console.ReadLine();

                Console.WriteLine($@"https://orgb67475d6.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&pagetype=entityrecord&etn=account&id={accoutId}");
                
                CreateContact(accoutId, nomeContato, cpf);
                //return accoutId;


            }
            else
            {
                if (resposta.ToUpper() == "N")
                {
                    Console.WriteLine("Fim do programa!");
                   
                }

            }
              
            return accoutId;
        }

        public Guid CreateContact(Guid accoutId, string nomeContato, string cpf)
        {
            Console.WriteLine("Estou no CreateContact");
            Entity contato = new Entity("contact");
            contato["accountid"] = new EntityReference("account", new Guid($"{accoutId}"));
            contato["firstname"] = nomeContato;
            contato["atv_cpf"] = cpf;
            Guid contactId = serviceClient.Create(contato);
            Console.WriteLine($@"https://orgb67475d6.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&pagetype=entitylist&etn=contact&viewid={contactId}&viewType=1039");
            Console.WriteLine("fim CreateContact");
            return contactId;
        }
        public Entity GetAccountByName(string name)
        {
            QueryExpression queryExpression = new QueryExpression("contact");
            queryExpression.ColumnSet.AddColumns("firstname");
            queryExpression.AddLink("account", "contactid", "primarycontactid");
            queryExpression.Criteria.AddCondition("firstname", ConditionOperator.BeginsWith, name);
            EntityCollection accounts = this.serviceClient.RetrieveMultiple(queryExpression);

            if (accounts.Entities.Count() > 0)
                return accounts.Entities.FirstOrDefault();
            else
                Console.WriteLine("Nenhuma conta encontrada com esse nome");

            return null;
            
        }
        public Entity GetAccountById(Guid id, string[] columns)
        {
            return serviceClient.Retrieve("account", id, new ColumnSet(columns));
        }
        public Entity GetAccountById(Guid id)
        {
            var context = new OrganizationServiceContext(this.serviceClient);

            return (from a in context.CreateQuery("account")
                    join b in context.CreateQuery("contact")
                    on ((EntityReference)a["primarycontactid"]).Id equals b["contactid"]
                    where (Guid)a["accountid"] == id
                    select a).ToList().FirstOrDefault();
        }
        public Entity validateDoc(string cnpj)
        {
            QueryExpression queryExpression = new QueryExpression("account");
            queryExpression.ColumnSet.AddColumns("atv_cnpj");
            queryExpression.Criteria.AddCondition("atv_cnpj", ConditionOperator.BeginsWith, cnpj);
            EntityCollection accounts = this.serviceClient.RetrieveMultiple(queryExpression);

            return accounts.Entities.FirstOrDefault();  
        }
        public void IncrementOrDecrementNumberOfOpp(Entity oppAccount, bool? decrementOrIncrement)
        {
            int numberOfOpp = oppAccount.Contains("atv_numdeoportunidades") ? (int)oppAccount["atv_numdeoportunidades"] : 0;

            if (Convert.ToBoolean(decrementOrIncrement))
            {
                numberOfOpp += 1;
            }
            else
            {
                numberOfOpp -= 1;
            }
            oppAccount["atv_numdeoportunidades"] = numberOfOpp;
            serviceClient.Update(oppAccount);
        }

    }
}
