using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Utils.Wrappers
{
    public record struct ServiceResult<T>
    {
        public ResultTypes ResultType { get; set; }
        public string? Message { get; set; }
        public T Obj { get; set; }
        public string? Ext { get; set; }
    }

    public record struct serviceResultSmall<T>
    {
        public ResultTypes resultType { get; set; }
        public string? message { get; set; }
        public T obj { get; set; }
        public string? ext { get; set; }
    }
}
