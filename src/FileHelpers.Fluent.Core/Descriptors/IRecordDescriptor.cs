using System.Collections.Generic;

namespace FileHelpers.Fluent.Core.Descriptors
{
    public interface IRecordDescriptor
    {
        char NullChar { get; set; }

        IDictionary<string, IFieldInfoTypeDescriptor> Fields { get; }

        void Add(string fieldName, IFieldInfoTypeDescriptor fieldDescriptor);
    }
}
