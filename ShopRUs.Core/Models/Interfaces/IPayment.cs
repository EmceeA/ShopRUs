using ShopRUs.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Interfaces
{
    public interface IPayment
    {
        Task<List<GetAllItemDto>> GetAllItem();
        Task<AddItemResponseDto> AddItem(AddItemRequestDto itemModel);

        Task<CustomerItemResponseDto> AddInvoice(CustomerInvoiceRequest itemModel);
        Task<AddItemTypeResponseDto> AddItemType(AddItemTypeRequestDto itemType);
    }
}
