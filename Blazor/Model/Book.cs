using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Article { get; set; }
    public Category Category { get; set; }
    public Publisher Publisher { get; set; }
    public IEnumerable<Author> Authors { get; set; }
}