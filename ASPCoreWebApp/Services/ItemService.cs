using ASPCoreWebApp.DB;
using ASPCoreWebApp.DB.Table;
using ASPCoreWebApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebApp.Services
{
    public class ItemService : IItemService

    {
        private readonly DBContex _context;
        public ItemService(DBContex context)
        {
            _context = context;
        }
        public async Task<bool> AddItem(ItemViewModel itemViewModel)
        {
            using var transation=_context.Database.BeginTransaction();
            try
            {
                string itemCode = GenerateItemCode(itemViewModel.ItemName);
                TblItemMaster tblItemMaster = new TblItemMaster();
                tblItemMaster.ItemCode = itemCode;
                tblItemMaster.ItemName = itemViewModel.ItemName;
                tblItemMaster.ItemDescription = itemViewModel.ItemDescription;
                tblItemMaster.ItemUmo = itemViewModel.ItemUOM;
                tblItemMaster.ItemUnitPrice = itemViewModel.ItemUnitCost;
                _context.Add(tblItemMaster);
                await _context.SaveChangesAsync();
                transation.Commit();
                return true;
            }
            catch (Exception ex)
            {
                await transation.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(ItemViewModel itemViewModel)
        {
            using var transation = _context.Database.BeginTransaction();
            try
            {
                TblItemMaster tblItem=await _context.TblItemMasters.FirstOrDefaultAsync(x=>x.ItemCode==itemViewModel.ItemCode);
                if (tblItem==null)
                    throw new Exception("Item not found");
                tblItem.ItemName = itemViewModel.ItemName;
                tblItem.ItemDescription = itemViewModel.ItemDescription;
                tblItem.ItemUnitPrice= itemViewModel.ItemUnitCost;
                tblItem.ItemUmo = itemViewModel.ItemUOM;
                _context.Update(tblItem);
                await _context.SaveChangesAsync();
                transation.Commit();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemViewModel> GetItemByCode(string code)
        {
            try
            {
                var itemCodeParam = new SqlParameter("@ItemCode", code);
                var qry = _context.Database.ExecuteSqlRaw("EXEC GetItemByCode @ItemCode", code);
                TblItemMaster tblItem = _context.TblItemMasters.FirstOrDefault(x => x.ItemCode == code);

                if (tblItem != null)
                {
                    ItemViewModel itemViewModel = new ItemViewModel();
                    itemViewModel.ItemCode = tblItem.ItemCode;
                    itemViewModel.ItemName = tblItem.ItemName;
                    itemViewModel.ItemUnitCost = tblItem.ItemUnitPrice;
                    itemViewModel.ItemUOM = tblItem.ItemUmo;
                    itemViewModel.ItemDescription = tblItem.ItemDescription;
                    return await Task.Run(() => itemViewModel);
                }
                else
                    throw new Exception("No Data Found");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
           
        }

        public async Task<List<ItemViewModel>> GetItems()
        {
            try
            {
                List<ItemViewModel> lst = (from i in _context.TblItemMasters
                                           select new ItemViewModel
                                           {
                                               ItemCode = i.ItemCode,
                                               ItemName = i.ItemName,
                                               ItemDescription = i.ItemDescription,
                                               ItemUOM = i.ItemUmo,
                                               ItemUnitCost = i.ItemUnitPrice
                                           }).ToList();
                return await Task.Run(() => lst);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
           
        }

        public string GenerateItemCode(string itemName)
        {
            int currentYear = DateTime.Now.Year;
            int nextYear = currentYear + 1;
            if (DateTime.Now.Month < 4)
            {
                currentYear -= 1;
                nextYear -= 1;
            }
            string fyear = (currentYear % 100).ToString("D2") + "-" + (nextYear % 100).ToString("D2");
            var lastIte = _context.TblItemMasters.OrderByDescending(i => i.Id).FirstOrDefault();
            int newItemNumber = 1;
            if (lastIte != null && lastIte.ItemCode.StartsWith("Item"))
            {
                string[] parts = lastIte.ItemCode.Split('/');
                if (parts.Length > 0 && int.TryParse(parts[0].Substring(4), out int lastNumber))
                {
                    newItemNumber = lastNumber + 1;
                }
            }
            string formattedNumber = $"Item{newItemNumber:000}";

            // Combine parts to generate the item code
            return $"{formattedNumber}/{fyear}/{itemName}";
        }
    }
}
