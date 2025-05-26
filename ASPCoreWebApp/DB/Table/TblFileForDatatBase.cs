namespace ASPCoreWebApp.DB.Table
{
    public class TblFileForDatatBase
    {
        public int Id { get; set; }
        public Guid EmpGUId { get; set; }
        public Guid FileGuid { get; set; }
        public byte[] Data { get; set; } // Corresponds to varbinary(max)
        public string Description { get; set; } = string.Empty; // varchar(50)
        public virtual TableEmployee Emp { get; set; } = null!;
    }
}
