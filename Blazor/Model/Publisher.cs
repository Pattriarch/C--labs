using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Book> Books { get; set; }
}