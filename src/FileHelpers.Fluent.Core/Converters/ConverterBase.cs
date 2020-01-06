namespace FileHelpers.Fluent.Core.Converters
{
    public abstract class ConverterBase
    {
        public abstract string FieldType { get; }

        public abstract object StringToField(string from);

        public virtual string FieldToString(object from) =>
            from == null ? string.Empty : from.ToString();
    }
}
