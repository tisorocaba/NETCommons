using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.Pagination {
    public static class ParametersParser {

        public static PaginationParameters FromRequest(HttpRequestMessage request)  {
            try {
                var requestParameters = request.GetQueryNameValuePairs();
                Func<string, string> GetParameter = (key) => requestParameters.Where(p => p.Key == key).FirstOrDefault().Value;

                int page;
                if (GetParameter("page") == null || !Int32.TryParse(GetParameter("page"), out page)) {
                    page = 1;
                }
                
                int itensPerPage;
                if (GetParameter("itens_per_page") == null || !Int32.TryParse(GetParameter("itens_per_page"), out itensPerPage)) {
                    itensPerPage = 10;
                }

                List<Sorter> sorters = new List<Sorter>();
                if (GetParameter("sort_fields") != null) {
                    string[] sortFields = GetParameter("sort_fields").Split(',');
                    foreach (string sortField in sortFields) {
                        if (!Regex.IsMatch(sortField, @"^\w+(\.\w+)*:(asc|desc)+$")) {
                            throw new PaginationException(Strings.InvalidSorterFormat);
                        }
                        string name = sortField.Substring(0, sortField.IndexOf(":"));
                        string value = sortField.Substring(sortField.IndexOf(":") + 1);
                        sorters.Add(new Sorter(name, value));
                    }
                }

                List<Filter> filters = new List<Filter>();
                if (GetParameter("filter_fields") != null) {
                    foreach (string filterExpression in GetParameter("filter_fields").Split(',')) {
                        filters.Add(Filter.CreateFilter(filterExpression));
                    }
                }

                List<Filter> fullFilters = new List<Filter>();
                if (GetParameter("fullsearch_fields") != null) {
                    string fullValue = GetParameter("fullsearch_value");
                    if (string.IsNullOrWhiteSpace(fullValue)) {
                        throw new PaginationException(Strings.FullSearchValueRequired);
                    }
                    foreach (string fullField in GetParameter("fullsearch_fields").Split(',')) {
                        fullFilters.Add(Filter.CreateSimpleFilter(fullField, fullValue));
                    }
                }

                return new PaginationParameters {
                    Page = page,
                    ItensPerPage = itensPerPage,
                    Sorters = sorters,
                    Filters = filters,
                    FullFilters = fullFilters
                };

            } catch (Exception e) {
                throw (e is PaginationException) ? e : new PaginationException(Strings.InvalidPaginationParameters);
            }
        }
    }
}
