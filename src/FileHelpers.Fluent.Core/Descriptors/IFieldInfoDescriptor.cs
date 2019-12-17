using System;

namespace FileHelpers.Fluent.Core.Descriptors
{
    public interface IFieldInfoDescriptor : IFieldInfoTypeDescriptor
    {
        Type Converter { get; set; }

        object NullValue { get; set; }

        AlignMode AlignMode { get; set; }

        char AlignChar { get; set; }

        string ConverterFormat { get; set; }

        TrimMode TrimMode { get; set; }

        Type Type { get; set; }
    }
}
