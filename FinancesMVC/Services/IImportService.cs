using DocumentFormat.OpenXml.Vml.Office;

namespace FinancesMVC.Services
{
    public interface IImportService<TEntity>
    {
        Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}
