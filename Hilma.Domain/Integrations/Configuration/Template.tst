${
    using Typewriter.Extensions.Types;

    Template(Settings settings)
    {
        settings.IncludeProject("Hilma.Domain");
        settings.OutputExtension = "Configuration.cs";
    }
    static List<string> ImportedTypes = new List<string>();
    
    string LoudName(Property property)
    {

        return property.Name.ToUpperInvariant();
    }
    
    string ConvertDefault(Type type) {
               
        if ((type.IsEnumerable && !type.IsPrimitive && !type.IsEnum) || type.Attributes.Any(x => x.Name == "Contract")) {
            return $"= new {ConfigurationName(type.ClassName())}();";
        }

        return "= false;";
    }
    
    string ConfigurationName( string name ) {
        return $"{name}Configuration";
    }

    string ConvertType(Type type) {
        if ((type.IsEnumerable && !type.IsPrimitive && !type.IsEnum ) || type.Attributes.Any(x => x.Name == "Contract" )) {
            return ConfigurationName(type.ClassName());
        }
                
        return "bool";
    }

    string ImportType(Type type) {
        if ((type.IsEnumerable && (type.IsEnum || !type.IsPrimitive)) || type.Attributes.Any(x => x.Name == "Contract"  || x.Name == "EnumContract" )) {
            if (!ImportedTypes.Contains(type.Namespace)) {
                ImportedTypes.Add(type.Namespace);
                return $"using {ConfigurationName(type.Namespace)};{Environment.NewLine}";
            }
        }

        // handle generics
        var foundTypes = type.TypeArguments.Where(y => y.Attributes.Any(x => x.Name == "Contract" || x.Name == "EnumContract"));
        foreach (var foundType in foundTypes) {
            if (!ImportedTypes.Contains(foundType.Namespace)) {
                ImportedTypes.Add(foundType.Namespace);
                return $"using {ConfigurationName(foundType.Namespace)};{Environment.NewLine}";
            }
        }

        return string.Empty;
    }

    string Clear(string dummy) {
        ImportedTypes.Clear();
        return string.Empty;
    }
}
$Classes([Contract])[namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of $Name for Ted integration
    /// </summary>
    public class $Name[$ConfigurationName]
    {
        $BaseClass[$Properties[
        public $Type[$ConvertType] $Name {get; set;} $Type[$ConvertDefault]]]
        $Properties[
        public $Type[$ConvertType] $Name {get; set;} $Type[$ConvertDefault]]
    }$Name[$Clear]
}]
