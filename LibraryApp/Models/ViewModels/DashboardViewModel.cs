namespace LibraryApp.Models.ViewModels
{
	public class DashboardViewModel
	{
		public int AuthorCount { get; set; }
		public int CategoryCount { get; set; }
		public int BookCount { get; set; }
		public IEnumerable<Book> Books { get; set; }
	}
}
