using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SiteCidadesUsuarios
{
    public static class AutenticacaoUsuarios //mesmo usuário, classe estática
    {

        public static string username = "RafaBomfim";
        public static string password = "123";
        public static string token = "";

        public static async System.Threading.Tasks.Task<string> getTokenAsync() //retorna token do cliente/funcionário
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/token");
                client.DefaultRequestHeaders.Clear(); //limpa cabeçalho da requisição
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress.ToString());
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string> //body da requisição
                {
                    {"username", username},
                    {"password", password },
                    {"grant_type","password" }
                });

                var response = await client.SendAsync(request); //requisição com os dados

                var payLoad = JObject.Parse(await response.Content.ReadAsStringAsync()); //ler onteúdo da resposta para a váriavel

                var token = payLoad.Value<string>("access_token"); //busca chave e armazena valor do token na variável

                return token;
            }
        }
    }
}