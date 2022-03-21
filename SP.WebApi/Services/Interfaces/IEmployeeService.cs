using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SP.WebApi.Domain.DTO;
using SP.WepApi.Domain.Models.AWS;

namespace SP.WebApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        //Creating a record 
        Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO model);

        //Updating a record 
        Task<EmployeeDTO> UpdateAsync(EmployeeUpdateDTO model);

        //Deleting a record 
        Task<bool> DeleteAsync(string Id);
        Task<bool> DeleteAsync(string Id, Object RangeKey);

        //Get a record ID
        Task<EmployeeDTO> GetByIDAsync(string Id);
        Task<List<EmployeeDTO>> GetByName(string FirstName);
        Task<List<EmployeeDTO>> GetByName(string FirstName,string LastName);

    }
}