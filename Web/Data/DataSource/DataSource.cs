using Web.Data.Models;

public class DataSource
{
    private static DataSource instance;
    private DataSource()
    {
    }
    public static DataSource GetInstance()
    {
        if (instance == null)
        {
            instance = new DataSource();
        }
        return instance;
    }

    public List<Book> _books = new List<Book>();
    public List<Author> _authors = new List<Author>();
    public List<Category> _categories = new List<Category>();
    public List<Publisher> _publishers = new List<Publisher>();
}
