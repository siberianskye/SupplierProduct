using Microsoft.EntityFrameworkCore.Infrastructure;
using SupplierProductExercise.Business.Context;
using SupplierProductExercise.Business.Entities;
using SupplierProductExercise.Business.Enums;
using SupplierProductExercise.Business.Repositories;
using SupplierProductExercise.Business.Services.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SupplierProductExercise.Business.Services
{
    public class SupplierProductService : ISupplierProductService
    {
        private readonly ISupplierServiceParameterRepository __SupplierServiceParameterRepository;

        public SupplierProductService(ISupplierServiceParameterRepository supplierServiceParameterRepository)
        {
            __SupplierServiceParameterRepository = supplierServiceParameterRepository ?? throw new ArgumentNullException(nameof(supplierServiceParameterRepository));
        }

        public async Task<GetSupplierProductsServiceResult> GetSupplierProducts(Service service)
        {
            GetSupplierProductsServiceResult _Result = new();

            SupplierServiceParameter _Parameter = await __SupplierServiceParameterRepository.GetByService(service);

            if (_Parameter == null || string.IsNullOrWhiteSpace(_Parameter.BearerToken))
            {
                return _Result;
            }

            string _BearerToken = _Parameter.BearerToken;

            using HttpClient _Client = new HttpClient();

            HttpRequestMessage _Request = new HttpRequestMessage(HttpMethod.Get, "https://api-white.impressioneurope.com/v1/supplier/products");
            _Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _BearerToken);         

            using HttpResponseMessage _Response = await _Client.SendAsync(_Request);

            if (_Response.IsSuccessStatusCode)
            {
                var _JsonString = await _Response.Content.ReadAsStringAsync();

                _Result.SupplierProductModels = JsonSerializer.Deserialize<List<SupplierProductModel>>(_JsonString);
                _Result.IsSuccuessful = true;
            }

            return _Result;      
        }
    }
}
