using System.Text.Json;
using client.Model;
using DbPgSql;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Validator;

namespace cafeteria.Controllers
{
    public class sellerController : Controller
    {
        ///<summary>
        ///Cadastra um novo vendedor.
        ///</summary>
        ///<response code="200">Vendedor cadastrado com sucesso!</response>
        [ProducesResponseType(typeof(List<sellerSchema>), 200)]
        [HttpPost("/registerSeller")]
        public static async Task<string> registerSeller([FromBody] Seller seller)
        {
            string response = "";
            sellerValidator validator = new sellerValidator();
            ValidationResult result = validator.Validate(seller);//Faz a validação do usuário.
            string message = result.ToString();
            if (!result.IsValid)
            {
                return message;
            }
            else
            {
                using (var pgsql = new pgsql())
                {
                    try
                    {
                        List<Seller> IsEmailValid = pgsql.Sellers.Where(x => x.email == seller.email).ToList();//Verifica se o email já existe no banco.                                                    
                        if (IsEmailValid.Count == 0)
                        {//Persiste o usuário, caso ele não esteja na base.
                            List<Seller> IsDocumentValid = pgsql.Sellers.Where(x => x.cpf == seller.cpf).Where(x => x.cnpj == seller.cnpj).ToList();//Verifica se os documentos já existem no banco.
                            if (IsDocumentValid.Count == 0)
                            {
                                seller.uuId = Guid.NewGuid().ToString(seller.uuId);//Gera o uuId do usuário.
                                seller.password = BCrypt.Net.BCrypt.HashPassword(seller.password);//Encripta a senha do usuário.
                                seller.email = seller.email.ToString().ToLower();//Salva o email no banco em lower case.
                                pgsql.Sellers.Add(seller);
                                await pgsql.SaveChangesAsync();
                                message = "OK";//Usuário salvo com sucesso.
                                response = JsonSerializer.Serialize(new { seller.sellerId, seller.uuId, message });//Retorno para o front.
                            }
                            else
                            {
                                message = "DOCUMENT_EXISTS";//Cpf ou Cnpj já existente no banco.
                                response = JsonSerializer.Serialize(new { message });
                            }
                        }
                        else
                        {
                            message = "EMAIL_EXISTS";//Email já existente no banco.
                            response = JsonSerializer.Serialize(new { message });
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

        ///<summary>
        ///Recupera um vendedor salvo no banco.
        ///</summary>
        ///<response code="200">Vendedor recuperado com sucesso!</response>
        [HttpGet("/getSeller/{Id}")]
        public static async Task<string> getSeller([FromRoute] int sellerId)//Método para o front receber um usuário especifico.      
        {
            string response = "";
            string message = response.ToString();
            //Product product = new Product();
            using (var pgsql = new pgsql())
            {
                List<Seller> dbUsers = pgsql.Sellers.Where(x => x.sellerId == sellerId).ToList();//Acessa o banco de procuro o usuário pelo userId.
                if (dbUsers.Count > 0)//Se o userId for maior que 0, ou seja, existente, vai retornar o usuário.
                {
                    response = JsonSerializer.Serialize(new { dbUsers });
                }
                else
                {
                    message = "USER_NOT_FOUND";//Usuário não encontrado.
                    response = JsonSerializer.Serialize(new { message });
                }
            }
            return response;
        }

        ///<summary>
        ///Edita as informações de um vendedor.
        ///</summary>
        ///<response code="200">Vendedor editado com sucesso!</response>
        [HttpPut("/editSeller/{Id}")]
        public static async Task<String> editSeller([FromBody] Seller seller)//Passar o id e uuId para edição.
        {
            string response = "";
            string message = response.ToString();
            using (var pgsql = new pgsql())
            {
                try
                {
                    var sellers = new Seller();
                    pgsql.Sellers.Update(seller);//Edita um usuário existente no bacno.
                    await pgsql.SaveChangesAsync();
                    message = "OK";//Usuário editado com sucesso.
                    response = JsonSerializer.Serialize(new { message });
                }
                catch (Exception e)
                {
                    message = "USER_NOT_FOUND";//Usuário não encontrado.
                    response = JsonSerializer.Serialize(new { message });
                }
            }
            return response;
        }

        ///<summary>
        ///Exclui um vendedor cadastrado.
        ///</summary>
        ///<response code="200">Vendedor deletado com sucesso!</response>
        [HttpDelete("/deleteSeller/{Id}")]
        public static async Task<string> deleteSeller([FromRoute] int Id)//Passar o id para deletar.
        {
            string response = "";
            string message = response.ToString();
            using (var pgsql = new pgsql())
            {
                try
                {
                    Seller seller = pgsql.Sellers.Find(Id);//Acha o usário pelo Id no banco.
                    pgsql.Sellers.Remove(seller);//Deleta o usuário do banco.
                    pgsql.SaveChanges();
                    message = "OK";//Usuário deletado com sucesso.
                    response = JsonSerializer.Serialize(new { message });
                }
                catch (Exception e)
                {
                    message = "USER_NOT_FOUND";//Usuário não encontrado.
                    response = JsonSerializer.Serialize(new { message });
                }
            }
            return response;
        }
    }
}

