using System.Threading.Tasks;

namespace DocumentProcessing
{
    public interface IDocumentProcessingStep
    {
        Task InvokeAsync(ProcessingContext context, DocumentProcessingDelegate next);
    }
}
