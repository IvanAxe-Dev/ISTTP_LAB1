using DocumentFormat.OpenXml.Vml.Office;

namespace FinancesMVC.Services
{
    public interface IExportService
    {
        public interface IExportService<TEntity>
        {
            Task WriteToAsync(Stream stream, CancellationToken cancellationToken);
        }

    }
}
