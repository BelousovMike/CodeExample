using System;
using System.IO;

namespace DocumentProcessing
{
    internal interface IDocumentProcessingContextFactory
    {
        ProcessingContext Create();
    }

    internal class ProcessingContextInternalFactory : IDocumentProcessingContextFactory
    {
        private readonly IServiceProvider _services;

        public ProcessingContextInternalFactory(IServiceProvider services)
        {
            _services = services;
        }

        public ProcessingContext Create()
        {
            return new ProcessingContextInternal(_services, new MemoryStream());
        }
    }
}
