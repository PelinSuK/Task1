using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System;

namespace ClassLibrary;

public class Book //Kitap Sınıfı
{
    // Özellikler (Properties)
    public string Title { get; set; }
    public string Author { get; set; }
    public int ISBN { get; set; }
    public int CopyCount { get; set; }
    public int BorrowedCopies { get; set; }
    // Kurucu Metot (Constructor)
    public Book(string title, string author, int isbn, int copyCount, int borrowedCopies)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        CopyCount = copyCount;
        BorrowedCopies = borrowedCopies;
    }
}
public class BorrowedBooks
{
    public string title;
    public List<DateTime> BorrowedDates { get; set; }
    public List<DateTime> ReturnDates { get; set; }
}
class Library //Kütüphane Sınıfı
{   
    // Metotlar (Methods/Functions)
    public void DisplayBookInfo(Book book)
    {
        Console.WriteLine("Kitap Bilgileri:");
        Console.WriteLine($"Başlik: {book.Title}");
        Console.WriteLine($"Yazar: {book.Author}");
        Console.WriteLine($"ISBN: {book.ISBN}");
        Console.WriteLine($"Toplam Kopya Sayisi: {book.CopyCount}");
        Console.WriteLine($"Ödünç Alinan Kopya Sayisi: {book.BorrowedCopies}");
        /*Console.WriteLine("Ödünç Alma Tarihleri: \n");
        for (int i = 0; i < book.BorrowedCopies; i++)
        {
            Console.WriteLine($"{book.BorrowedBooks[i]}");
        }*/
        Console.WriteLine("------------");
    }
    public void DisplayList(List<Book> books)
    {
        Console.WriteLine("-----Library-------");
        foreach (Book b in books)
        {
            DisplayBookInfo(b);
            Console.WriteLine("------------");
        }
    }
    public void SearchTitle(List<Book> books)
    {
        
        Console.WriteLine("Aramak istediğiniz kitap basliğini yazin:");
        string title = Console.ReadLine();
        bool exist = false;
        foreach(Book b in books)
        {
            if(b.Title == title)
            {
                DisplayBookInfo(b);
                exist = true;
            }
        }
        if(!exist)
        {
            Console.WriteLine("Aradiğiniz kitap yoktur.");
            Console.WriteLine("------------");
        }

    }
    public void SearchAuthor(List<Book> books)
    {
        Console.WriteLine("Aramak istediğiniz yazarin ismini yazin:");
        string name = Console.ReadLine();
        bool exist = false;
        foreach(Book b in books)
        {
            if(b.Author == name)
            {
                DisplayBookInfo(b);
                exist = true;
            }
        }
        if(!exist)
        {
            Console.WriteLine("Aradiğiniz yazar yoktur.");
            Console.WriteLine("------------");
        }

    }

    public void AddNewBook(List<Book> books)
    {
        string filePath = FindFilePath("kitaplar.txt");
        //List<DateTime> dates = new();
       // List<DateTime> returnDates = new();

        Console.WriteLine("Kitap Bilgilerini Giriniz:");

            Console.Write("Başlik: ");
            string title = Console.ReadLine();

            Console.Write("Yazar: ");
            string author = Console.ReadLine();

            Console.Write("ISBN: ");
            int isbn = int.Parse(Console.ReadLine());

            Console.Write("Toplam Kopya Sayisi: ");
            int copyCount = int.Parse(Console.ReadLine());

            Console.Write("Ödünç Alinan Kopya Sayisi: ");
            int borrowedCopies = int.Parse(Console.ReadLine());
            /*if (borrowedCopies>0)
            {
                for (int i = 0; i < borrowedCopies; i++)
                {
                    dates[i] = DateTime.Now;
                    returnDates[i] = dates[i].AddDays(3); 
                }
            }*/
            
            // Kullanıcının girdiği bilgilerle yeni bir Book nesnesi oluşturup listeye ekleme
            Book newBook = new Book(title, author, isbn, copyCount, borrowedCopies);
            books.Add(newBook);
            //BorrowedBooks newBorrow = new BorrowedBooks(title,dates,returnDates);
            //borrowed.Add(newBorrow);
            Save(filePath,books);
            Console.WriteLine("Kitap başariyla eklendi.\n");
            Console.WriteLine("------------");

    }
    public void BorrowBook(List<Book> books)
    {
        Console.WriteLine("Ödünc almak istediğiniz kitabin basliğini yazin:");
        string title = Console.ReadLine();
        bool exist = false;
        foreach(Book b in books)
        {
            if(b.Title == title)
            {
                if (b.CopyCount >0)
                {
                    b.BorrowedCopies++;
                    b.CopyCount--;

                    Console.WriteLine("Ödünç alindi.");
                    Console.WriteLine("------------");
                }
                else
                {
                    Console.WriteLine("Ödünç alinacak kopya sayisi yetersiz.");
                    Console.WriteLine("------------");
                }
                exist = true;
            }
        }
        if(!exist)
        {
            Console.WriteLine("Aradiğiniz kitap yoktur.");
            Console.WriteLine("------------");
        }
        
    }

    public void ReturnBook(List<Book> books)
    {
        Console.WriteLine("Iade etmek istediğiniz kitabin basliğini yazin:");
        string title = Console.ReadLine();
        bool exist = false;
        foreach(Book b in books)
        {
            if(b.Title == title)
            {
                if (b.BorrowedCopies >0)
                {
                    b.BorrowedCopies--;
                    b.CopyCount++;
                    Console.WriteLine("Iade edildi.");
                    Console.WriteLine("------------");
                }
                else
                {
                    Console.WriteLine("Iade edilecek kopya sayisi yetersiz.");
                    Console.WriteLine("------------");
                }
                exist = true;
            }
        }
        if(!exist)
        {
            Console.WriteLine("Aradiğiniz kitap yoktur.");
            Console.WriteLine("------------");
        }
    }

/// <summary>
/// Save/Load txt file
/// </summary>

    public string FindFilePath(string fileName)
    {
        // Uygulamanın çalıştığı dizini al
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Dosya yolu oluştur
        string filePath = Path.Combine(appDirectory, fileName);

        // Dosyanın varlığını kontrol et
        if (File.Exists(filePath))
        {
            return filePath;
        }
        else
        {
            return null;
        }

    }
   public void Save(string filePath, List<Book> books)
    {
        // Kitapları dosyaya kaydet
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (Book book in books)
            {
                writer.WriteLine($"Başlik: {book.Title}, Yazar: {book.Author}, ISBN: {book.ISBN}, Toplam Kopya: {book.CopyCount}, Ödünç Alinan Kopya: {book.BorrowedCopies}");
            }
        }
    }
    public void LoadBooksFromFile(string filePath, List<Book> books)
    {
        
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // Satırdaki bilgileri ayırarak Book nesnesi oluşturun ve listeye ekleyin
                        string[] bookInfo = line.Split(',');
                        string title = bookInfo[0].Split(':')[1].Trim();
                        string author = bookInfo[1].Split(':')[1].Trim();
                        int isbn = int.Parse(bookInfo[2].Split(':')[1].Trim());
                        int copyCount = int.Parse(bookInfo[3].Split(':')[1].Trim());
                        int borrowedCopies = int.Parse(bookInfo[4].Split(':')[1].Trim());
                        Book loadedBook = new Book(title, author, isbn, copyCount, borrowedCopies);
                        books.Add(loadedBook);
                    }
                }
            }
        }

    }
}

