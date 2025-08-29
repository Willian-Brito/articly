using System.Text.Json.Serialization;
using Articly.Core.Domain.Entities;

namespace Articly.Core.Application.DTOs;

public class ArticleByCategoryDTO 
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public byte[]? Content { get; set; }

    public ArticleByCategoryDTO() { }
}