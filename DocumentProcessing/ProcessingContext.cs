using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentProcessing
{
    public abstract class ProcessingContext
    {
        public IServiceProvider Services { get; }

        public IDictionary<string, object> Items { get; }

        public Stream Output { get; }

        public ProcessingContext(IServiceProvider services, Stream output)
        {
            Services = services;
            Output = output;
            Items = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get { return Items[key]; }
            set { Items[key] = value; }
        }
    }

    internal class ProcessingContextInternal : ProcessingContext
    {
        public ProcessingContextInternal(IServiceProvider services, Stream output) :
            base(services, output)
        {

        }
    }
}
