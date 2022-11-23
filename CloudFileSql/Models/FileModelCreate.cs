namespace CloudFileSql.Models
{
    public class FileModelCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public IFormFile File { get; set; }
    }
}
