using Microsoft.EntityFrameworkCore;
using ProjetoAPI.DataBase;
using ProjetoAPI.Models;
namespace ProjetoAPI.Endpoints
{
    public static class Produtos
    {
        public static void RegistrarEndpointsProdutos(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaProdutos = rotas.MapGroup("/produtos");

                


            // GET      /produtos/{Id}
            rotaProdutos.MapGet("/{Id}", (ProdutosDbContext dbContext, int Id) =>
            {
            try 
            { 
                Produto produto = dbContext.Produtos.Find(Id);
                if (produto == null)
                {
                    return Results.NotFound();
                }

                return TypedResults.Ok(produto);
            }
            catch (Exception)
            {
               throw new ArgumentException("Ocorreu um erro durante a busca do produto.");
            }
            }).Produces<Produto>();


            // POST     /produtos
            rotaProdutos.MapPost("/", (ProdutosDbContext dbContext, Produto produto) =>
            {
                try
                {

                    var novoProduto = dbContext.Produtos.Add(produto);
                    dbContext.SaveChanges();

                    return TypedResults.Created($"/produtos/{produto.Id}", produto);
                }
                catch(Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante o cadastro do produto.");
                }
            });

            //TESTE
            // POST     /produtos/seed
            rotaProdutos.MapPost("/seed", (ProdutosDbContext dbContext, bool excluirProdutosExistentes = false) =>
            {
                Produto teclado = new Produto("Teclado" ,"Muito bom", "alexandre", 1, 52);
                Produto mouse = new Produto("Mouse", "Muito bom", "alexandre", 1, 52);
                Produto monitor = new Produto("Monitor", "Muito bom", "alexandre", 1, 52);
                Produto mousepad = new Produto("MousePad", "Muito bom", "alexandre", 1, 52);
                Produto microfone = new Produto("Microfone", "Muito bom", "alexandre", 1, 52);
                Produto fone = new Produto("Fone", "Muito bom", "alexandre", 1, 52);

                if (excluirProdutosExistentes)
                {
                    dbContext.Produtos.RemoveRange(dbContext.Produtos);
                }

                dbContext.Produtos.AddRange([
                    teclado,
                    mouse,
                    monitor,
                    mousepad,
                    microfone,
                    fone,
                ]);

                dbContext.SaveChanges();

                return TypedResults.Created();
            });


            // PUT /produtos/{Id}
            rotaProdutos.MapPut("/{Id}", (ProdutosDbContext dbContext, int Id, Produto produto) =>
            {
                try
                {
                    Produto produtoEncontrado = dbContext.Produtos.Find(Id);
                    if (produtoEncontrado == null)
                    {
                        return Results.NotFound();
                    }

                   // produto.Id = Id; (PERGUNTAR PARA O PROFESSOR)

                    dbContext.Entry(produtoEncontrado).CurrentValues.SetValues(produto);
                    dbContext.SaveChanges();

                    return TypedResults.NoContent();
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a atualização do produto.");
                }
            });


            // DELETE   /produtos/{Id}
            rotaProdutos.MapDelete("/{Id}", (ProdutosDbContext dbContext, int Id) =>
            {
                try
                {
                    Produto produtoEncontrado = dbContext.Produtos.Find(Id);
                    if (produtoEncontrado == null)
                    {
                        return Results.NotFound();
                    }

                    dbContext.Produtos.Remove(produtoEncontrado);

                    dbContext.SaveChanges();

                    return TypedResults.NoContent();
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a remoção do produto.");
                }
            });

        }

    }
}
