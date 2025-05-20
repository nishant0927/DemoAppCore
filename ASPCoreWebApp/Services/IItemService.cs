using ASPCoreWebApp.Models;

namespace ASPCoreWebApp.Services
{
    public interface IItemService
    {
        Task<bool>AddItem(ItemViewModel itemViewModel);
        Task<List<ItemViewModel>> GetItems();
        Task<ItemViewModel>GetItemByCode(string code);
        Task<bool> Update(ItemViewModel itemViewModel);
    }
}
