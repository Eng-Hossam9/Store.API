using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.productParams
{
    public class ProductParams
    {
        public string? sort { get; set; }
        public int? typeId { get; set; }
        public int? brandId { get; set; }
        public int? pageIndex { get; set; } = 1;
        public int? pageSize { get; set; } = 5;

        private string? search;
        public string? Search { get => search; set => search = value?.ToLower(); }

    }
}
