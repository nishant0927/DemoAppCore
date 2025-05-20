using ASPCoreWebApp.DB;
using ASPCoreWebApp.DB.Table;
using ASPCoreWebApp.Models;
using ASPCoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASPCoreWebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        //private readonly DBContex _context;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Index(ItemViewModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await _itemService.AddItem(item);                    
                    TempData["SuccessMessage"] = "Item added successfully!";
                    return RedirectToAction("DispalyList");
                }
                return View(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
               
            }
           
        }

        public async Task< IActionResult> DispalyList()
        {
            try
            {
                ItemViewModel item = new ItemViewModel();
                item.Items = await _itemService.GetItems();
                return View(item);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
           
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                byte[] data = Convert.FromBase64String(id);
                string decodedId = System.Text.Encoding.UTF8.GetString(data);
                //string decodedId = Uri.UnescapeDataString(id);
                ItemViewModel tblItem = await _itemService.GetItemByCode(decodedId);
                return View(tblItem);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ItemViewModel itemViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _itemService.Update(itemViewModel);
                    TempData["SuccessMessage"] = "Item Updated successfully!";
                    return RedirectToAction("DispalyList");
                }
                return View(itemViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }

        //public string GenerateItemCode(string itemName)
        //{
        //    int currentYear = DateTime.Now.Year;
        //    int nextYear = currentYear + 1;
        //    if (DateTime.Now.Month < 4)
        //    {
        //        currentYear -= 1;
        //        nextYear -= 1;
        //    }
        //    string fyear = (currentYear % 100).ToString("D2") + "-" + (nextYear % 100).ToString("D2");
        //    var lastIte = _context.TblItemMasters.OrderByDescending(i => i.Id).FirstOrDefault();
        //    int newItemNumber = 1;
        //    if (lastIte != null && lastIte.ItemCode.StartsWith("Item"))
        //    {
        //        string[] parts = lastIte.ItemCode.Split('/');
        //        if (parts.Length > 0 && int.TryParse(parts[0].Substring(4), out int lastNumber))
        //        {
        //            newItemNumber = lastNumber + 1;
        //        }
        //    }
        //    string formattedNumber = $"Item{newItemNumber:000}";

        //    // Combine parts to generate the item code
        //    return $"{formattedNumber}/{fyear}/{itemName}";
        //}
    }
}
