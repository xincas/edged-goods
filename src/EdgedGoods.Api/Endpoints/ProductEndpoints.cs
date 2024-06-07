using EdgedGoods.Api.Endpoints.Common;
using EdgedGoods.Application.Products.Commands.Create;
using EdgedGoods.Application.Products.Commands.Delete;
using EdgedGoods.Application.Products.Commands.Update;
using EdgedGoods.Contracts.Products;
using EdgedGoods.Domain.Common.ValueObjects;
using EdgedGoods.Domain.Products.ValueObjects;
using MediatR;

namespace EdgedGoods.Api.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/products");

        group.MapPost("", Create)
            .WithDescription("Create new product");
        
        group.MapPut("", Update)
            .WithDescription("Update existing product");
        
        group.MapPut("{productId:guid}", Delete)
            .WithDescription("Delete existing product");
        
        group.MapPost("{productId:guid}/images", AddImage)
            .WithDescription("Adding images to existing product");
        
        group.MapPost("{productId:guid}/images/{imageName}", DeleteImage)
            .WithDescription("Adding images to existing product");

        return builder;
    }

    private static async Task<IResult> Delete(ISender sender, Guid productId)
    {
        var command = new DeleteProductCommand(productId);

        var result = await sender.Send(command);

        return result.Match(
            _ => TypedResults.NoContent(),
            ProblemGeneration.Problem);
    }

    private static async Task<IResult> Create(CreateProductRequest request, ISender sender)
    {
        var command = new CreateProductCommand(
            request.Name,
            new Money(request.Price.Value, request.Price.Currency), 
            request.Description,
            request.Images?.Select(i => new Image(i.Name, i.Url, i.Size)).ToList(),
            request.Quantity,
            request.BucketId);

        var result = await sender.Send(command);

        return result.Match(
            TypedResults.Ok,
            ProblemGeneration.Problem);
    }

    private static async Task<IResult> Update(UpdateProductRequest request, ISender sender)
    {
        var command = new UpdateProductCommand(
            request.Id,
            request.Name,
            new Money(request.Price.Value, request.Price.Currency),
            request.Description,
            request.Images?.Select(i => new Image(i.Name, i.Url, i.Size)).ToList(),
            request.Quantity,
            request.BucketId
        );

        var result = await sender.Send(command);

        return result.Match(
            TypedResults.Ok,
            ProblemGeneration.Problem);
    }

    private static async Task<IResult> AddImage(IFormFile[] files, ISender sender, Guid productId)
    {
        foreach (var file in files)
        {
            // Todo validation of file (file extension / only images must be accepted)
        }
        
        // Todo save images in file statics (create infrastructure file service)
        
        // Todo save all information about image in database

        return TypedResults.Ok();
    }
    
    private static async Task<IResult> DeleteImage(ISender sender, Guid productId, string imageName)
    {
        // Todo delete image from file statics (create infrastructure file service)
        
        // Todo delete information about image in database

        return TypedResults.Ok();
    }
}