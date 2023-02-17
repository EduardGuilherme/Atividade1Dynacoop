using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividadeSolution
{
    internal class Singleton
    {
        public static CrmServiceClient GetService()
        {
            string url = "orgb67475d6";
            string clientId = "83d4b16a-8f9c-4a05-a9b4-1c55f85e51fa";
            string clientSecret = "LrI8Q~fpWCD6WzR61Egceu2Ee_DsdzptztM6bcsc";
            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");

            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão Realizada com Sucesso");
            else
                Console.WriteLine("Erro na conexão.");

            return serviceClient;
        }
    }
}
