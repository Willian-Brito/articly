using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Articly.Core.Domain.Base;

public abstract class BaseModel
{
    [Key, Column("id")]    
    public int ID { get; set; }
}