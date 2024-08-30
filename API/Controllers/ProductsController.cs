using System;
using System.Net.Http.Headers;
using System.Reflection;
using Core.Entities;
using Core.Interfaces;
using Infrostructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        return Ok (await repository.GetProductsAsync(brand, type, sort));

    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id){

        var product = await repository.GetProductBYIdAsync(id);

        if(product == null) return NotFound();
        return product;

    }


    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct (Product product){
        repository.AddProduct(product);
        if( await repository.SaveChangesAsync()){
           return CreatedAtAction("GetProduct", new {id = product.Id}, product);
        }
        return BadRequest("Problem Creating Product");
    }

    [HttpPut("{id: int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product){
    
        if(product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");
        
        repository.UpdateProduct(product);
        if(await repository.SaveChangesAsync()){

            return NoContent();
        }
        return BadRequest("Problem Updating Product");
    }

    [HttpDelete("{id: int}")]
    public async Task<ActionResult> DeleteProduct (int id){
        var product = await repository.GetProductBYIdAsync(id);

        if(product == null) return BadRequest();
        repository.DeleteProduct(product);
        if(await repository.SaveChangesAsync()){

            return NoContent();
        }        return BadRequest("Problem deleting product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands(){

        return Ok(await repository.GetBrandsAsync());

    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes(){

        return Ok(await repository.GetTypesAsync());

    }


    private bool ProductExists (int id){
        return repository.ProductExists(id);
    }

}
