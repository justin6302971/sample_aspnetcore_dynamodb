using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using SP.WebApi.Domain.DTO;
using SP.WebApi.Services.Interfaces;
using SP.WepApi.Domain.Models.AWS;

namespace SP.WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAmazonDynamoDB _amazonDynamoDBClient;
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly IMapper _mapper;

        public EmployeeService(
            IAmazonDynamoDB amazonDynamoDBClient,
            IDynamoDBContext dynamoDBContext,
            IMapper mapper)
        {
            _amazonDynamoDBClient = amazonDynamoDBClient;
            _dynamoDBContext = dynamoDBContext;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO model)
        {
            var entity = await GetByIDAsync(model.LoginAlias);
            if (entity != null)
            {
                throw new ArgumentException("the login alias is used");
            }
            var newEntity = _mapper.Map<EmployeeCreateDTO, Employee>(model);

            //error handling?
            await this._dynamoDBContext.SaveAsync<Employee>(newEntity);

            var result = _mapper.Map<EmployeeCreateDTO, EmployeeDTO>(model);
            return result;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var entity = await _dynamoDBContext.LoadAsync<Employee>(Id);
            if (entity == null)
            {
                return true;
            }

            await _dynamoDBContext.DeleteAsync(entity);

            return true;
        }

        public Task<bool> DeleteAsync(string Id, object RangeKey)
        {
            throw new System.NotImplementedException();
        }

        public async Task<EmployeeDTO> GetByIDAsync(string Id)
        {
            var entity = await _dynamoDBContext.LoadAsync<Employee>(Id);
            var result = _mapper.Map<Employee, EmployeeDTO>(entity);
            return result;
        }

        public async Task<List<EmployeeDTO>> GetByName(string FirstName)
        {
            string indexName = "Name";
            var filter = new QueryFilter();
            filter.AddCondition(nameof(FirstName), QueryOperator.Equal, FirstName);

            var asyncSearch = _dynamoDBContext.FromQueryAsync<Employee>(new QueryOperationConfig
            {
                Filter = filter,
                BackwardSearch = false,
                IndexName = indexName
            });

            var resultEntities = await asyncSearch.GetRemainingAsync();

            var result = resultEntities.Select(d => _mapper.Map<Employee, EmployeeDTO>(d));

            return result != null ? result.ToList() : new List<EmployeeDTO>();
        }

        public async Task<List<EmployeeDTO>> GetByName(string FirstName, string LastName)
        {
            if (string.IsNullOrEmpty(LastName))
            {
                return await GetByName(FirstName);
            }

            string indexName = "Name";

            var filter = new QueryFilter();
            filter.AddCondition(nameof(FirstName), QueryOperator.Equal, FirstName);
            filter.AddCondition(nameof(LastName), QueryOperator.Equal, LastName);

            var asyncSearch = _dynamoDBContext.FromQueryAsync<Employee>(new QueryOperationConfig
            {
                Filter = filter,
                BackwardSearch = false,
                IndexName = indexName
            });

            var resultEntities = await asyncSearch.GetRemainingAsync();

            var result = resultEntities.Select(d => _mapper.Map<Employee, EmployeeDTO>(d));

            return result != null ? result.ToList() : new List<EmployeeDTO>();
        }



        public async Task<EmployeeDTO> UpdateAsync(EmployeeUpdateDTO model)
        {
            var entity = await _dynamoDBContext.LoadAsync<Employee>(model.LoginAlias);
            if (entity == null)
            {
                throw new ArgumentException("this employee doesn't exist");
            }

            //update certain properties
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.ManagerLoginAlias = model.ManagerLoginAlias;
            entity.Skills = model.Skills;

            //error handling?
            await this._dynamoDBContext.SaveAsync<Employee>(entity);

            var result = _mapper.Map<Employee, EmployeeDTO>(entity);
            return result;
        }

        #region temp code
        // public Task<K> GetByIDAsync(string Id)
        // {
        //     return _dynamoDBContext.LoadAsync<K>(Id);
        // }
        // public Task<K> GetByIDAsync(string Id, object RangeKey)
        // {
        //     return _dynamoDBContext.LoadAsync<K>(Id, RangeKey);
        // }

        // public List<K> ScanGet(IEnumerable<ScanCondition> f)
        // {
        //     var temp = (_dynamoDBContext.ScanAsync<K>(f.ToArray())).GetRemainingAsync().Result;
        //     return temp != null ? temp.ToList() : new List<K>();
        // }

        // public async Task<List<K>> ScanGetAsync(IEnumerable<ScanCondition> f)
        // {
        //     var asyncSearch = _dynamoDBContext.ScanAsync<K>(f);
        //     var temp = await asyncSearch.GetRemainingAsync();
        //     return temp != null ? temp.ToList() : new List<K>();
        // }

        // public async Task<List<K>> ScanGetAsync(IEnumerable<ScanCondition> f, string indexName)
        // {
        //     var asyncSearch = _dynamoDBContext.ScanAsync<K>(f, new DynamoDBOperationConfig
        //     {
        //         IndexName = indexName,
        //     });
        //     var temp = await asyncSearch.GetRemainingAsync();
        //     return temp != null ? temp.ToList() : new List<K>();
        // }
        // public async Task<List<K>> QueryGetAsync(QueryOperationConfig f, string indexName)
        // {
        //     var asyncSearch = _dynamoDBContext.FromQueryAsync<K>(f, new DynamoDBOperationConfig
        //     {
        //         IndexName = indexName,
        //     });
        //     var temp = await asyncSearch.GetRemainingAsync();
        //     return temp != null ? temp.ToList() : new List<K>();
        // }

        // public async Task<List<K>> QueryGetAsync(QueryFilter filter, string indexName)
        // {

        //     var asyncSearch = _dynamoDBContext.FromQueryAsync<K>(new QueryOperationConfig
        //     {
        //         Filter = filter,
        //         BackwardSearch = false,
        //         IndexName = indexName

        //     });
        //     var temp = await asyncSearch.GetRemainingAsync();
        //     return temp != null ? temp.ToList() : new List<K>();
        // }

        #endregion
    }
}