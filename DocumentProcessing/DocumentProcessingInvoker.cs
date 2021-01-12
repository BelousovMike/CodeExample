using System;
using System.Threading.Tasks;

namespace DocumentProcessing
{
    public interface IDocumentProcessingInvoker
    {
        IDocumentProcessingInvoker UseStartup<TStartup>()
            where TStartup : DocumentProcessingStartup, new();

        IDocumentProcessingInvoker ConfigureContext(Action<ProcessingContext> configuration);

        Task<ProcessingContext> InvokeAsync();
    }

    internal class DocumentProcessingInvoker : IDocumentProcessingInvoker
    {
        private readonly IDocumentProcessingBuilderFactory _processingFactory;
        private readonly IDocumentProcessingContextFactory _contextFactory;

        private Func<DocumentProcessingStartup> _startupFactory;
        private Action<ProcessingContext> _contextConfiguration;

        public DocumentProcessingInvoker(
            IDocumentProcessingBuilderFactory processingFactory,
            IDocumentProcessingContextFactory contextFactory)
        {
            _processingFactory = processingFactory;
            _contextFactory = contextFactory;
        }

        public IDocumentProcessingInvoker ConfigureContext(Action<ProcessingContext> configuration)
        {
            _contextConfiguration = configuration;
            return this;
        }

        public async Task<ProcessingContext> InvokeAsync()
        {
            var processing = _processingFactory.Create();
            var startup = _startupFactory?.Invoke();
            startup?.Configure(processing);
            var context = _contextFactory.Create();
            _contextConfiguration?.Invoke(context);
            await processing.Build().Invoke(context);
            return context;
        }

        public IDocumentProcessingInvoker UseStartup<TStartup>() where TStartup : DocumentProcessingStartup, new()
        {
            _startupFactory = () => new TStartup();
            return this;
        }
    }
}
