using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Templatez.Api.Controllers.Base;
using Templatez.Api.Controllers.V1.Request.Customers;
using Templatez.Api.Controllers.V1.Response.Common;
using Templatez.Api.Controllers.V1.Response.Customers;
using Templatez.Api.Http.Errors;
using Templatez.Application.Core.Results;
using Templatez.Application.Services.Customers;
using Templatez.Domain.Commands.Customers;
using Templatez.Infra.CrossCutting.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Templatez.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/customer")]
    [Produces("application/json")]
    public class CustomerController : ApiController
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerAppService _service;

        public CustomerController(ILogger<CustomerController> logger,
            IMapper mapper,
            ICustomerAppService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        //[Authorize("read:customer")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorRequestMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorRequestValidation), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomer([Required] Guid id)
        {
            try
            {
                var result = await _service.GetCustomer(id);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Response(Result<CustomerResponse>.Fail("unable to list customer"));
            }
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize("read:customer")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorRequestMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorRequestValidation), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var result = await _service.GetAllCustomers();
                return Response(_mapper.Map<Result<IEnumerable<CustomerResponse>>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Response(Result<CustomerResponse>.Fail("unable to list customers"));
            }
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize("create:customer")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorRequestMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorRequestValidation), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCustomer([Required, FromBody] CreateCustomerRequest request)
        {
            try
            {
                var result = await _service.CreateCustomer(_mapper.Map<CreateCustomerCommand>(request));
                return Response(_mapper.Map<Result<CreateResponse>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Response(Result<Guid>.Fail("unable to create customer"));
            }
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        //[Authorize("update:customer")]
        [ProducesResponseType(typeof(UpdateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorRequestMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorRequestValidation), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCustomer([Required] Guid id, [FromBody] UpdateCustomerRequest request)
        {
            try
            {
                if (request.NoFields())
                    return Response(Result<bool>.Fail("unable to update the customer without fields"));

                var result = await _service.UpdateCustomer(id, _mapper.Map<UpdateCustomerCommand>(request));
                return Response(_mapper.Map<Result<UpdateResponse>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Response(Result<bool>.Fail("unable to update customer"));
            }
        }

        /// <summary>
        /// Remove customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        //[Authorize("delete:customer")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorRequestMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorRequestValidation), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCustomer([Required] Guid id)
        {
            try
            {
                var result = await _service.DeleteCustomer(id);
                return Response(_mapper.Map<Result<DeleteResponse>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Response(Result<bool>.Fail("unable to delete customer"));
            }
        }
    }
}
