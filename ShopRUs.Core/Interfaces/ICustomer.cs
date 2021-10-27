using ShopRUs.Core.DTO;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Interfaces
{
    public interface ICustomer
    {
        Task<CustomerSignUpResponseDto> CustomerSignUp(CustomerSignUpRequestDto signUpModel);
        Task<CustomerSignInResponseDto> CustomerSignIn(CustomerSignInRequestDto signInModel);
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        Customer GetByName(string name);

        Task<List<GetAllCustomerDto>> GetAllCustomer();
        Task<List<GetAllCustomerDto>> GetCustomerById(int customerId);

    }
}
