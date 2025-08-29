﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Articly.Core.Domain.Entities;

namespace Articly.Core.Application.DTOs;

public class CategoryDTO
{
    public int? ID { get; set; }
    
    public string? Name { get; set; }

    public int? ParentId { get; set; }
    
    [JsonIgnore]
    public CategoryDTO? Parent { get; set; }

    public List<CategoryDTO>? Children { get; set; }

    public CategoryDTO() { }
}
