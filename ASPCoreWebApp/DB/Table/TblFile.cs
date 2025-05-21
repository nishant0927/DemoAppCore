using System;
using System.Collections.Generic;

namespace ASPCoreWebApp.DB.Table;

public partial class TblFile
{
    public int Id { get; set; }

    public Guid EmpGuid { get; set; }

    public Guid FileGuid { get; set; }

    public string FileName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public virtual TableEmployee Emp { get; set; } = null!;
}
