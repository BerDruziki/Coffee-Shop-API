using System.Text.Json;
using client.Model;
using DbPgSql;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Validator;

namespace cafeteria.Controllers
{
    public class coffeeController : Controller
    {
        ///<summary>
        ///Cadastra um novo café.
        ///</summary>
        ///<response code="200">Café cadastrado com sucesso!</response>
        [ProducesResponseType(typeof(List<coffeeSchema>), 200)]
        [HttpPost ("/registerCoffee")]
       public static async Task<string> registerCoffee ([FromBody]Coffee coffee)
        {
            string response = "";
            coffeeValidator validator = new coffeeValidator();
            ValidationResult result = validator.Validate(coffee);//Valida o produto.
            string message = result.ToString();
            if(!result.IsValid){
                    return message;
                }
            else
                {
                    using (var pgsql = new pgsql())
                        {
                        try
                            {
                                coffee.uuId = Guid.NewGuid().ToString(coffee.uuId);//Gera o uuId do produto.
                                pgsql.Coffee.Add(coffee);//Adiciona o produto no banco de dados.
                                await pgsql.SaveChangesAsync(); 
                                message = "OK";//Produto salvo com sucesso.
                                response = JsonSerializer.Serialize(new {coffee.coffeeId, coffee.uuId, message});//retorno para o front.
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
        ///Recupera um café salvo no banco.
        ///</summary>
        ///<response code="200">Café recuperado com sucesso!</response>
        [HttpGet ("/getCoffee/{clientId}")]
       public static async Task<string> getCoffee ([FromRoute]int sellerId)//Método para o front receber uma lista de produtos de um usuário especifico.      
        {
            string response = "";
            string message = response.ToString();                                                        
            Coffee coffee = new Coffee();
            using (var pgsql = new pgsql())
            {
                List<Coffee> dbCoffee = pgsql.Coffee.Where(x => x.sellerId == sellerId).ToList();//Acessa o banco de procuro o produto pelo userId.
                if(dbCoffee.Count > 0)//Se o userId for maior que 0, ou seja, existente, vai retornar os produtos daquele usuário.
                {
                    response = JsonSerializer.Serialize(new {dbCoffee});                           
                }
                else
                {
                    message = "WITHOUT_PRODUCTS";//Usuário sem produtos cadastrados.
                    response = JsonSerializer.Serialize(new {message});
                }
            }
            return response;
        }

        ///<summary>
        ///Edita as informações de um café.
        ///</summary>
        ///<response code="200">Café editado com sucesso!</response>
        [HttpPut ("/editCoffee/{Id}")]
       public static async Task<String> editCoffee ([FromBody] Coffee coffee)//É necessário informar o Id e o uuId para que ocorra o update.
        {
            string response = "";
            string message = response.ToString();
            using (var pgsql = new pgsql()) 
            {
            try
            {
                var coffees = new Coffee();
                pgsql.Coffee.Update(coffee);//Edita o produto no banco de dados.
                await pgsql.SaveChangesAsync();
                message = "OK";//Produto editado com sucesso.
                response = JsonSerializer.Serialize(new {message});
            }
            catch (Exception e)
            {
                message = "PRODUCT_NOT_FOUND";//Produto não encontrado no banco.
                response = JsonSerializer.Serialize(new {message});
            }
            } 
        return response;
    }

        ///<summary>
        ///Exclui um café cadastrado.
        ///</summary>
        ///<response code="200">Café deletado com sucesso!</response>
        [HttpDelete ("/deleteCoffee/{Id}")]
       public static async Task<string> deleteCoffee ([FromRoute]int Id)//É necessário informar o Id para que seja possível deletar o produto.
        {
            string response = "";
            string message = response.ToString();
            using (var pgsql = new pgsql())
            {
                try
                {
                    Coffee coffee = pgsql.Coffee.Find(Id);//Acha o produto pelo Id no banco.
                    pgsql.Coffee.Remove(coffee);//Deleta o produto do banco.
                    pgsql.SaveChanges();
                    message = "OK";//Produto deletado com sucesso
                    response = JsonSerializer.Serialize(new {message});
                }
                 catch (Exception e)
                {
                    message = "PRODUCT_NOT_FOUND";//Produto não encontrado.
                    response = JsonSerializer.Serialize(new {message});
                }
            }
            return response;
        }
    }
}