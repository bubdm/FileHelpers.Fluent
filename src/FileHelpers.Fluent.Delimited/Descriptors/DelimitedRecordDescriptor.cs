using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Delimited.Descriptors
{
    public class DelimitedRecordDescriptor : RecordDescriptor
    {
        public DelimitedRecordDescriptor(string delimiter)
        {
            Delimiter = delimiter;
        }

        public string Delimiter { get; set; }
    }
}
