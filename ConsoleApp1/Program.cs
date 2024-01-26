namespace ClassLibrary;

class Program
{
    public static List<Book> books = new();
    public static Book myBook = new Book(" "," ",0,0,0);
    public static Library library = new();
   // public static List<BorrowedBooks> borrowedBooks = new();
    static void Main()
    {
        LibraryProgram();
    }

    public static void LibraryProgram()
    {
        string filename = "kitaplar.txt";
        string filePath = library.FindFilePath(filename);
        if (filePath != null)
        {
            Console.WriteLine("Dosya bulundu. Yolu: " + filePath);
        }
        else
        {
            Console.WriteLine("Dosya bulunamadi.");
        }
        library.LoadBooksFromFile(filePath,books);
        while (true)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Yapmak istediğiniz işlemi seçin:\n");
            Console.WriteLine("1. Kitap Ekle");
            Console.WriteLine("2. Tüm Kitaplari Görüntüle");
            Console.WriteLine("3. Basliga Göre Ara");
            Console.WriteLine("4. Yazara Göre Ara");
            Console.WriteLine("5. Ödünç Al");
            Console.WriteLine("6. İade Et");
            Console.WriteLine("7. Süresi Geçmiş Kitaplarla Ilgili Bilgileri Görüntüle");
            Console.WriteLine("0. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    library.AddNewBook(books);
                    break;

                case 2:
                   library.DisplayList(books);
                    break;

                case 3:
                    library.SearchTitle(books);
                    break;

                case 4:
                    library.SearchAuthor(books);
                    break;

                case 5:
                    library.BorrowBook(books);
                    break;

                case 6:
                    library.ReturnBook(books);
                    break;

                case 7:
                    // süresi geçmiş
                    break;

                case 0:
                    // Çıkış
                    library.Save(filePath,books);
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    Console.WriteLine("------------");
                    break;
            }
        }
    }
}
 