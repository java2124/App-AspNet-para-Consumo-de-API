using Newtonsoft.Json;
using SiteCidadesUsuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SiteCidadesUsuarios.Controllers
{
    public class UsuarioController : Controller
    {
        static IEnumerable<Cidade> cidade = null;

        private async System.Threading.Tasks.Task<IEnumerable<Cidade>> GetCidades()
        {
            IEnumerable<Cidade> cidade = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/api/cidades");
                client.DefaultRequestHeaders.Clear(); //limpa cabeçalho da requisição
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string token = await AutenticacaoUsuarios.getTokenAsync();
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token); //nunca esquecer do espaço após o "bearer"

                HttpResponseMessage resposta = await client.GetAsync(client.BaseAddress.ToString()); //resposta da requisição

                if (resposta.IsSuccessStatusCode)
                {
                    var conteudo = resposta.Content.ReadAsStringAsync().Result; //coloca a resposta da requisição na váriavel
                    cidade = JsonConvert.DeserializeObject<Cidade[]>(conteudo); //resultado que está no conteúdo Json é transformado em vetor
                }
            }
            return cidade;
        }

        // GET: Usuario
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            IEnumerable<Cidade> cidade = await GetCidades();
            IEnumerable<Usuario> usuarios = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/api/usuarios");
                client.DefaultRequestHeaders.Clear(); //limpa cabeçalho da requisição
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string token = await AutenticacaoUsuarios.getTokenAsync();
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                HttpResponseMessage resposta = await client.GetAsync(client.BaseAddress.ToString());

                if (resposta.IsSuccessStatusCode)
                {
                    var conteudo = resposta.Content.ReadAsStringAsync().Result;
                    usuarios = JsonConvert.DeserializeObject<Usuario[]>(conteudo);
                }
            }

            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public async System.Threading.Tasks.Task<ActionResult> Create() //método que irá exibir o formulário de criação de usuário e chamar o método que executa a ação
        {
            cidade = await GetCidades();

            ViewBag.cod_cidade = new SelectList(
                cidade,
                "cod_cidade",
                "nome_cidade"
            );

            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid) //se o model der inválido, mostra as informações e o porquê do erro
                {
                    ViewBag.cod_cidade = new SelectList(
                        cidade,
                        "cod_cidade",
                        "nome_cidade",
                        usuario.cod_cidade
                        );

                    return View(usuario);
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44399/api/usuarios");
                    client.DefaultRequestHeaders.Clear(); //limpa cabeçalho da requisição
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string token = await AutenticacaoUsuarios.getTokenAsync();
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                    await client.PostAsJsonAsync(client.BaseAddress.ToString(), usuario);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {

            IEnumerable<Cidade> cidade = await GetCidades();
            Usuario usuario = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/");
                client.DefaultRequestHeaders.Clear(); 
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string token = await AutenticacaoUsuarios.getTokenAsync();
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                HttpResponseMessage resposta = await client.GetAsync("api/usuarios/" +id);

                if (resposta.IsSuccessStatusCode)
                {
                    var conteudo = resposta.Content.ReadAsStringAsync().Result;
                    usuario = JsonConvert.DeserializeObject<Usuario>(conteudo);
                }
            }

            ViewBag.cod_cidade = new SelectList(
                        cidade,
                        "cod_cidade",
                        "nome_cidade",
                        usuario.cod_cidade
                     );

            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id, Usuario usuario) //método que executa a atualização
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.cod_cidade = new SelectList(
                        cidade,
                        "cod_cidade",
                        "nome_cidade",
                        usuario.cod_cidade
                        );

                    return View(usuario);
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44399/");
                    client.DefaultRequestHeaders.Clear(); 
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string token = await AutenticacaoUsuarios.getTokenAsync(); //geração de token
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                    id = usuario.cod_cliente; //para evitar erro de roteamento
                    await client.PutAsJsonAsync ("api/usuarios/"+id, usuario);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id)
        {
            IEnumerable<Cidade> cidade = await GetCidades();
            Usuario usuario = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string token = await AutenticacaoUsuarios.getTokenAsync();
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                HttpResponseMessage resposta = await client.GetAsync("api/usuarios/" + id);

                if (resposta.IsSuccessStatusCode)
                {
                    var conteudo = resposta.Content.ReadAsStringAsync().Result;
                    usuario = JsonConvert.DeserializeObject<Usuario>(conteudo);
                }
            }

            ViewBag.cod_cidade = new SelectList(
                        cidade,
                        "cod_cidade",
                        "nome_cidade",
                        usuario.cod_cidade
                     );

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id, Usuario usuario)
        {
            try
            {
                if(id != usuario.cod_cliente) //validação da PK
                {
                    return HttpNotFound();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44399/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string token = await AutenticacaoUsuarios.getTokenAsync(); 
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                    await client.DeleteAsync("api/usuarios/" + id);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
