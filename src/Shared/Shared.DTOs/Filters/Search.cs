using System.Collections.Generic;

namespace CleanApi.Shared.DTOs.Filters
{
    public class Search
    {
        public List<string> Fields { get; set; } = new List<string>();
        public string Keyword { get; set; }
    }
}