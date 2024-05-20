using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BnFurniture.Application.Controllers.ProductCategoryController.Commands;
using BnFurniture.Application.Extensions;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands
{
    // Команда для создания ProductType
    public sealed record CreateProductTypeCommand(ProductTypeDTO entityForm);

    public sealed class CreateProductTypeHandler : CommandHandler<CreateProductTypeCommand>
    {
        private readonly IValidator<ProductTypeDTO> _validator;

        public CreateProductTypeHandler(IValidator<ProductTypeDTO> validator, IHandlerContext context)
            : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.entityForm;

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
                {
                    Message = "Validation failed.",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            var newProductType = new ProductType
            {
                Id = Guid.NewGuid(),
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Slug = dto.Slug,
                Priority = dto.Priority
            };

            await HandlerContext.DbContext.ProductType.AddAsync(newProductType, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.Created)
            {
                Message = "Product type created successfully."
            };
        }
    }

    // Команда для получения всех ProductType
    public class GetAllProductTypesHandler : CommandHandler<NoParameters>
    {
        public GetAllProductTypesHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(NoParameters request, CancellationToken cancellationToken)
        {
            var productTypes = await HandlerContext.DbContext.ProductType
                .Select(pt => new ProductTypeDTO
                {
                    Id = pt.Id,
                    CategoryId = pt.CategoryId,
                    Name = pt.Name,
                    Slug = pt.Slug,
                    Priority = pt.Priority
                })
                .ToListAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Data = productTypes,
                Message = "Product types retrieved successfully."
            };
        }
    }

    public sealed record DeleteProductTypeCommand(Guid Id);

    public sealed class DeleteProductTypeHandler : CommandHandler<DeleteProductTypeCommand>
    {
        public DeleteProductTypeHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = await HandlerContext.DbContext.ProductType
                .FirstOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);

            if (productType == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Product type not found."
                };
            }

            HandlerContext.DbContext.ProductType.Remove(productType);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Product type deleted successfully."
            };
        }
    }

    public sealed record UpdateProductTypeCommand(ProductTypeDTO UpdatedProductType);

    public sealed class UpdateProductTypeHandler : CommandHandler<UpdateProductTypeCommand>
    {
        private readonly IValidator<ProductTypeDTO> _validator;

        public UpdateProductTypeHandler(IValidator<ProductTypeDTO> validator, IHandlerContext context)
            : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdatedProductType;

            var productType = await HandlerContext.DbContext.ProductType
                .FirstOrDefaultAsync(pt => pt.Id == dto.Id, cancellationToken);

            if (productType == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Product type not found."
                };
            }

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
                {
                    Message = "Validation failed.",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            productType.Name = dto.Name;
            productType.Slug = dto.Slug;
            productType.Priority = dto.Priority;

            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Product type updated successfully."
            };

        }
    }
}
