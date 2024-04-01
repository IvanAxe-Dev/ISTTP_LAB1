//using FinancesMVC.Models;
//using static FinancesMVC.Services.IExportService;

//namespace FinancesMVC.Services
//{
//    public class CategoryDataPortServiceFactory
//    {
//        public class CategoryDataPortServiceFactory
//       : IDataPortServiceFactory<Category>
//        {
//            private readonly DblibraryContext _context;
//            public CategoryDataPortServiceFactory(DblibraryContext context)
//            {
//                _context = context;
//            }
//            public IImportService<Category> GetImportService(string contentType)
//            {
//                if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
//                {
//                    return new CategoryImportService(_context);
//                }
//                throw new NotImplementedException($"No import service implemented for movies with content type {contentType}");
//            }
//            public IExportService<Category> GetExportService(string contentType)
//            {
//                if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
//                {
//                    return new CategoryExportService(_context);
//                }
//                throw new NotImplementedException($"No export service implemented for movies with content type {contentType}");
//            }
//        }
//    }
//}
