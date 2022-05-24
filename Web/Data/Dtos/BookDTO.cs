namespace Web.Data.DataSource;

public class BookDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Article { get; set; }
    public int Category { get; set; }
    public int Publisher { get; set; }
    public int[] Authors { get; set; }

}