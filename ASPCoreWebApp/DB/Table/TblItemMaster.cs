using System;
using System.Collections.Generic;

namespace ASPCoreWebApp.DB.Table;

public partial class TblItemMaster
{
    public int Id { get; set; }

    public string ItemCode { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public string ItemDescription { get; set; } = null!;

    public string ItemUmo { get; set; } = null!;

    public decimal ItemUnitPrice { get; set; }
}
