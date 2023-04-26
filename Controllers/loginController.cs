using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using client.Model;
using DbPgSql;

namespace cafeteria.Controllers
{
    public class loginController : Controller
    {
        ///<summary>
        ///Loga o usuário.
        ///</summary>
        ///<response code="200">Usuário logado com sucesso!</response>
        [HttpPost ("/validatelogin")]
        public static async Task<string> validateLogin ([FromBody]loginDTO client)
        {
            string response = "";
            string message = response.ToString();
            using (var pgsql = new pgsql())
            {
                try
                {
                    List<Client> dbUser = pgsql.Clients.Where(x => x.email == client.email).ToList();//Confere no banco o email do usuário.
                    string passwordFromUser = client.password;
                    if (dbUser.Count > 0)
                        {
                            if (BCrypt.Net.BCrypt.Verify(passwordFromUser, dbUser[0].password))//Válida a senha do usuário.
                            {
                                message = "OK";//Usário com credenciais válidas.
                                response = JsonSerializer.Serialize(new {dbUser});//Retorna o usuário com todos os dados salvos no banco.
                            }
                            else
                            {
                                message = "INVALID_LOGIN";//Usuário com credenciais inválidas.
                                response = JsonSerializer.Serialize(new {message});
                            }
                        }
                    else
                    {
                        message = "INVALID_LOGIN";
                        response = JsonSerializer.Serialize(new {message});
                    }
                    List<Seller> dbSeller = pgsql.Sellers.Where(x => x.email == client.email).ToList();//Confere no banco o email do usuário.
                    string passwordFromSeller = client.password;
                    if (dbUser.Count > 0)
                        {
                            if (BCrypt.Net.BCrypt.Verify(passwordFromSeller, dbSeller[0].password))//Válida a senha do usuário.
                            {
                                message = "OK";//Usário com credenciais válidas.
                                response = JsonSerializer.Serialize(new {dbSeller});//Retorna o usuário com todos os dados salvos no banco.
                            }
                            else
                            {
                                message = "INVALID_LOGIN";//Usuário com credenciais inválidas.
                                response = JsonSerializer.Serialize(new {message});
                            }
                        }
                    else
                    {
                        message = "INVALID_LOGIN";
                        response = JsonSerializer.Serialize(new {message});
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                return response;
            }
        }
    }
}