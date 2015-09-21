using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity {
    internal static class Strings {

        private static readonly IDictionary<string, string> strings = new Dictionary<string, string>() {
            { "CollectionFilterNotSupported", "O filtro por propriedades de coleção não é suportado." },
            { "CouldNotConvertValueToPropertyType", "Não foi possível converter o valor \"{0}\" para o tipo \"{1}\" da propriedade \"{2}\"." },
            { "CouldNotConvertToType", "Não foi possível converter o valor para o tipo especificado." },
            { "DbCommandExecutionError", "Erro na execução do comando de dados." },
            { "DbConnectionError", "A aplicação não conseguiu se conectar ao banco de dados." },
            { "DbUpdateError", "Houve um erro ao tentar atualizar os dados." },
            { "DbValidationError", "Houveram erros na validação dos dados." },
            { "EntityIncorrectlySpecified", "A entidade \"{0}\" não foi especificada corretamente." },
            { "EntityNotFound", "A entidade \"{0}\" especificada não foi encontrada." },
            { "ExpressionMustBePropertyExpression", "A expressão \"{0}\" deve ser uma expressão de propriedade." },
            { "FieldCannotBeNull", "O campo {0} não pode ser vazio." },
            { "FullSearchValueRequired", "O valor da busca múltipla é obrigatório." },
            { "InvalidFilterExpression", "Formato de campo de filtro inválido." },
            { "InvalidPaginationParameters", "Os parâmetros de paginação informados são inválidos." },
            { "InvalidSorterFormat", "Formato de campo de ordenação inválido" },
            { "LikeNotSupportedForDataType", "O operador \"like\" não é suportado para o tipo de dado \"{0}\" da propriedade \"{1}\"." },
            { "ThereAreValidationErrors", "Houveram erros na validação dos dados." },
            { "TypeNotNullable", "O tipo de dado informado não aceita valores nulos." },
            { "PropertyMustBeValueType", "A propriedade \"{0}\" não é um de tipo de dado primitivo." },
            { "EntityPropertyInvalid", "A propriedade \"{0}\" não foi encontrada." },
            { "CollectionPropertyNotSupported", "A propriedade \"{0}\" possui coleções em seu caminho e não é suportada." }
        };

        internal static string CollectionFilterNotSupported { get {
            return GetString("CollectionFilterNotSupported");
        }}

        internal static string CouldNotConvertValueToPropertyType(string propertyValue, string propertyType, string propertyName) {
            return string.Format(GetString("CouldNotConvertValueToPropertyType"), propertyValue, propertyType, propertyName);
        }

        internal static string CouldNotConvertToType { get {
            return GetString("CouldNotConvertToType");
        }}

        internal static string CollectionPropertyNotSupported(string property) {
            return string.Format(GetString("CollectionPropertyNotSupported"), property);
        }

        internal static string DbCommandExecutionError { get {
            return GetString("DbCommandExecutionError");
        }}

        internal static string DbConnectionError { get {
            return GetString("DbConnectionError");
        }}

        internal static string DbUpdateError { get {
            return GetString("DbUpdateError");
        }}

        internal static string DbValidationError { get {
            return GetString("DbValidationError");
        }}

        internal static string EntityIncorrectlySpecified(string entityName) {
            return string.Format(GetString("EntityIncorrectlySpecified"), entityName);
        }

        internal static string EntityNotFound(string entityName) {
            return string.Format(GetString("EntityNotFound"), entityName);
        }

        internal static string EntityPropertyInvalid(string property) {
            return string.Format(GetString("EntityPropertyInvalid"), property);
        }

        internal static string ExpressionMustBePropertyExpression(string expression) {
            return string.Format(GetString("ExpressionMustBePropertyExpression"), expression);
        }

        internal static string FieldCannotBeNull { get {
            return GetString("FieldCannotBeNull");
        }}

        internal static string FullSearchValueRequired { get {
            return GetString("FullSearchValueRequired");
        }}

         internal static string InvalidFilterExpression { get {
            return GetString("InvalidFilterExpression");
        }}

        internal static string InvalidPaginationParameters { get {
            return GetString("InvalidPaginationParameters");
        }}

        internal static string InvalidSorterFormat { get {
            return GetString("InvalidSorterFormat");
        }}

        internal static string PropertyMustBeValueType(string property) {
            return string.Format(GetString("PropertyMustBeValueType"), property);
        }

        internal static string LikeNotSupportedForDataType(string propertyType, string property) {
            return string.Format(GetString("LikeNotSupportedForDataType"), property);
        }

        internal static string ThereAreValidationErrors { get {
            return GetString("ThereAreValidationErrors");
        }}

        internal static string TypeNotNullable { get {
            return GetString("TypeNotNullable");
        }}

        private static string GetString(string name) {
            return strings[name];
        }

        internal static Regex[] ConstraintRegexes { get {
            return new Regex[] {
                new Regex("A instrução [A-Z]+ conflitou com a restrição do [A-Z]+ \"(?<cName>[_.A-Z0-9]+)\"\\..*"),
                new Regex("Não é possível inserir uma linha de chave duplicada no objeto '[_.A-Z0-9]+' com índice exclusivo '(?<cName>[_.A-Z0-9]+)'\\..*"),
                new Regex("Violação da restrição UNIQUE KEY '(?<cName>[_.A-Z0-9]+)'. Não é possível inserir a chave duplicada no objeto '[_.A-Z0-9]+'\\..*")
            };
        }}
    }
}
