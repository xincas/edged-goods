using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Common.ValueObjects;
using EdgedGoods.Domain.Products.Errors;
using EdgedGoods.Domain.Products.ValueObjects;
using ErrorOr;

namespace EdgedGoods.Domain.Products;

public class Product : AuditableEntity<ProductId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Money Price { get; set; }
    public List<Image> Images { get; set; } = [];
    
    public bool IsHidden { get; set; }

    [SetsRequiredMembers]
    public Product(ProductId id, string name, Money price, string? description = null, List<Image>? images = null)
    {
        Name = name;
        Description = description;
        Price = price;
        IsHidden = false;
        Images.AddRange(images ?? Enumerable.Empty<Image>());
    }
    public Product() { }
    
    public ErrorOr<Success> DeleteImage(Image image)
    {
        var imageInCollection = Images.FirstOrDefault(i => i.Name == image.Name);
        if (imageInCollection is null)
        {
            return ProductErrors.ImageDoesNotExists;
        }

        Images.Remove(imageInCollection);
        return Result.Success;
    }
    
    public ErrorOr<Created> AddImage(Image image)
    {
        var imageInCollection = Images.FirstOrDefault(i => i.Name == image.Name);
        if (imageInCollection is not null)
        {
            return ProductErrors.ImageAlreadyExists;
        }

        Images.Add(image);
        return Result.Created;
    }
    
    public ErrorOr<Created> AddImages(IEnumerable<Image> images)
    {
        List<Error> errors = [];
        List<Image> imagesToAdd = [];
        

        foreach (var image in images)
        {
            var imageInCollection = Images.FirstOrDefault(i => i.Name == image.Name);
            if (imageInCollection is not null)
            {
                errors.Add(ProductErrors.ImageAlreadyExists);
                continue;
            }

            imagesToAdd.Add(image);
        }

        if (errors.Count > 0)
        {
            return errors;
        }
        
        Images.AddRange(imagesToAdd);
        return Result.Created;
    }
}