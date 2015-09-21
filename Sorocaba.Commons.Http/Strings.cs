using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Http {
    internal static class Strings {

        private static readonly IDictionary<string, string> strings = new Dictionary<string, string>() {
            { "HttpExceptionDefaultMessage", "Ocorreu um erro no processamento da requisição." },
            { "MalformedRequestData", "Formato de dados de requisição inválidos." }
        };

        internal static string HttpExceptionDefaultMessage { get {
            return GetString("HttpExceptionDefaultMessage");
        }}

        internal static string MalformedRequestData { get {
            return GetString("MalformedRequestData");
        }}

        private static string GetString(string name) {
            return strings[name];
        }
    }
}
