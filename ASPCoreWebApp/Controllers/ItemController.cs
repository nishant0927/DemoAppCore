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
        private readonly IWebHostEnvironment _envernoment;
        //private readonly DBContex _context;
        public ItemController(IItemService itemService, IWebHostEnvironment envernoment)
        {
            _itemService = itemService;
            _envernoment = envernoment;
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
                string uploadPath = Path.Combine(_envernoment.ContentRootPath, "uploads");
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

       
    }
}
