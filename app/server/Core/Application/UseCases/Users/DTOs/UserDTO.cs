using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Enums;

namespace Articly.Core.Application.DTOs;

public class UserDTO
{
    public int ID { get; set; }
    public string Name { get; set; }    
    public string Email { get; set; }
    public string[] Roles { get; set; }

    [JsonIgnore]
    public Collection<Article> Articles { get; set; }

    public UserDTO() { }
}
