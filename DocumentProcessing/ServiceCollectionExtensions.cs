using Microsoft.Extensions.DependencyInjection;

namespace DocumentProcessing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDocumentProcessing(this IServiceCollection services)
        {
            return services
                .AddTransient<IDocumentProcessingContextFactory, ProcessingContextInternalFactory>()
                .AddTransient<IDocumentProcessingBuilderFactory, DocumentProcessingBuilderFactory>()
                .AddTransient<IDocumentProcessingInvoker, DocumentProcessingInvoker>();
        }
    }
}
