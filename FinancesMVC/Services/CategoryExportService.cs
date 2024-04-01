//using ClosedXML.Excel;
//using FinancesMVC.Models;

//namespace FinancesMVC.Services
//{
//    public class CategoryExportService : IExportService<Category>
//    {
//        private const string RootWorksheetName = "";

//        private static readonly IReadOnlyList<string> HeaderNames =
//            new string[]
//            {
//                "Назва",
//                "Автор 1",
//                "Автор 2",
//                "Автор 3",
//                "Автор 4",
//                "Категорія",
//                "Інформація",
//            };
//        private readonly Db1Context _context;

//        private static void WriteHeader(IXLWorksheet worksheet)
//        {
//            for (int columnIndex = 0; columnIndex < HeaderNames.Count; columnIndex++)
//            {
//                worksheet.Cell(1, columnIndex + 1).Value = HeaderNames[columnIndex];
//            }
//            worksheet.Row(1).Style.Font.Bold = true;
//        }

//        private void WriteBook(IXLWorksheet worksheet, LibraryDomain.Model.Book book, int rowIndex)
//        {
//            var columnIndex = 1;
//            worksheet.Cell(rowIndex, columnIndex++).Value = book.Name;

//            var authorsbook = _context.AuthorBooks.Where(ab => ab.BookId == book.Id)
//                                                    .Include(ab => ab.Author)
//                                                    .Distinct();
//            //book.AuthorBooks.ToList();
//            foreach (var ab in authorsbook.Take(4))
//            {
//                //більше 4 авторів писати нікоди, ігноруємо їх
//                if ((ab.AuthorId is not null))
//                {
//                    var author = ab.Author;
//                    worksheet.Cell(rowIndex, columnIndex++).Value = author.Name;
//                }
//            }
//            worksheet.Cell(rowIndex, 7).Value = book.Info;
//        }

//        private void WriteBooks(IXLWorksheet worksheet, ICollection<LibraryDomain.Model.Book> books)
//        {
//            WriteHeader(worksheet);
//            int rowIndex = 2;
//            foreach (var book in books)
//            {
//                WriteBook(worksheet, book, rowIndex);
//                rowIndex++;
//            }
//        }

//        private void WriteCategories(XLWorkbook workbook, ICollection<LibraryDomain.Model.Category> categories)
//        {
//            //для усіх категорій формуємо сторінки
//            foreach (var cat in categories)
//            {

//                if (cat is not null)
//                {
//                    var worksheet = workbook.Worksheets.Add(cat.Name);
//                    WriteBooks(worksheet, cat.Books.ToList());
//                }
//            }
//        }

//        public CategoryExportService(DblibraryContext context)
//        {
//            _context = context;
//        }

//        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
//        {
//            if (!stream.CanWrite)
//            {
//                throw new ArgumentException("Input stream is not writable");
//            }
//            //тут для прикладу пишемо усі книги в усіх категоріях, в своїх проєктах потрібно писати лише вибрані категорії та книги
//            var categories = await _context.Categories
//                .Include(category => category.Books)
//                .ToListAsync(cancellationToken);

//            var workbook = new XLWorkbook();

//            WriteCategories(workbook, categories);
//            workbook.SaveAs(stream);
//        }

//    }

//}
