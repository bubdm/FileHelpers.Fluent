using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Core
{
    public class MultiRecordItem
    {
        /// <summary>
        /// Name that identifies the record
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Regex based pattern used to recognize a record
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// Record descriptor with line descriptor
        /// </summary>
        public IRecordDescriptor Descriptor { get; set; }
    }
}
