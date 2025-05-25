using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Global;


public sealed class PaginationMeta
{
    public int TotalRecords { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int? NextPage { get; set; }
    public int? PrevPage { get; set; }
}

