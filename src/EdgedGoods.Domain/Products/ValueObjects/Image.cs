using EdgedGoods.Domain.Common.Shared;

namespace EdgedGoods.Domain.Products.ValueObjects;

public class Image : IAuditable
{
    public string Name { get; set; }
    public string Url { get; set; }
    public int Size { get; set; }
    public bool IsAuditable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public Image(string name, string url, int size)
    {
        Name = name;
        Url = url;
        Size = size;
    }
}