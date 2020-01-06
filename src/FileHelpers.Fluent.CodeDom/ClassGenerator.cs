using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Extensions;
using FileHelpers.Fluent.Core.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Text;

namespace FileHelpers.Fluent.CodeDom
{
    public static class ClassGenerator
    {
        public static string GenerateCSharpClass(this IRecordDescriptor descriptor, string className, string @namespace) =>
            descriptor.GenerateCode("CSharp", className, @namespace);

        public static string GenerateVisualBasicClass(this IRecordDescriptor descriptor, string className, string @namespace) =>
            descriptor.GenerateCode("visualbasic", className, @namespace);

        private static string GenerateCode(this IRecordDescriptor descriptor, string language, string className, string @namespace)
        {
            var codeNamespace = new CodeNamespace(@namespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));

            codeNamespace.GenerateClass(descriptor, className, out bool hasArrayType);

            if (hasArrayType)
                codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));

            var targetUnit = new CodeCompileUnit();
            targetUnit.Namespaces.Add(codeNamespace);

            var provider = CodeDomProvider.CreateProvider(language);
            var options = new CodeGeneratorOptions
            {
                BracingStyle = "C",
                BlankLinesBetweenMembers = true
            };

            var sb = new StringBuilder();
            using (var writer = new StringWriterWithEncoding(sb, Encoding.UTF8))
            {
                provider.GenerateCodeFromCompileUnit(targetUnit, writer, options);
            }
            return sb.ToString();
        }

        private static void GenerateClass(this CodeNamespace codeNamespace, IRecordDescriptor descriptor, string className, out bool hasArrayType)
        {
            var targetClass = new CodeTypeDeclaration(className)
            {
                IsClass = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed
            };

            targetClass.AddProperties(codeNamespace, descriptor, out hasArrayType);

            codeNamespace.Types.Add(targetClass);
        }

        private static void AddProperties(this CodeTypeDeclaration targetClass, CodeNamespace codeNamespace, IRecordDescriptor descriptor, out bool hasArrayType)
        {
            hasArrayType = false;
            foreach (var field in descriptor.Fields)
            {
                if (field.Value.IsArray)
                {
                    codeNamespace.GenerateClass((IRecordDescriptor)field.Value, $"{field.Key}Item", out hasArrayType);
                    targetClass.AddArrayProperty(field.Key, $"{field.Key}Item");
                    hasArrayType = true;
                    continue;
                }
                targetClass.AddProperty(field.Key, field.Value);
            }
        }

        private static void AddArrayProperty(this CodeTypeDeclaration targetClass, string propertyName, string typeName)
        {
            var codeSnippet = new CodeSnippetTypeMember
            {
                Text = string.Concat("        public IList<", typeName, "> ", propertyName, " { get; set; }")
            };
            targetClass.Members.Add(codeSnippet);
        }

        private static void AddProperty(this CodeTypeDeclaration targetClass, string propertyName, IFieldInfoTypeDescriptor fieldDescriptor)
        {
            var codeSnippet = new CodeSnippetTypeMember
            {  
                Text = string.Concat("        public ", ((IFieldInfoDescriptor)fieldDescriptor).ResolveType(), " ", propertyName, " { get; set; }")
            };
            targetClass.Members.Add(codeSnippet);
        }
    }
}
