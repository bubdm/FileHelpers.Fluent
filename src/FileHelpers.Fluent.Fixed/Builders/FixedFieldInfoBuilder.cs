using FileHelpers.Fluent.Core.Builders;

namespace FileHelpers.Fluent.Fixed
{
    public class FixedFieldInfoBuilder : FieldInfoBuilder, IFixedFieldInfoDescriptor
    {
        public int Length { get; set; }

        public FixedFieldInfoBuilder SetLength(int length)
        {
            Length = length;
            return this;
        }
    }
}
