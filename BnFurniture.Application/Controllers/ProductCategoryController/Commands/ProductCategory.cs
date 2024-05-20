using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductCategoryController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BnFurniture.Application.Extensions;

namespace BnFurniture.Application.Controllers.ProductCategoryController.Commands
{
    public sealed record ProductCategoryCommand(ProductCategoryDTO entityForm);

    public sealed class AddProductCategoryHandler : CommandHandler<ProductCategoryCommand>
    {
        private readonly IValidator<ProductCategoryDTO> _validator;

        public AddProductCategoryHandler(IValidator<ProductCategoryDTO> validator, IHandlerContext context)
            : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(ProductCategoryCommand request, CancellationToken cancellationToken)
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

            // Генерация Slug, 
            if (string.IsNullOrWhiteSpace(dto.Slug))
            {
                dto.Slug = GenerateSlug(dto.Name);
            }

            await SaveCategory(dto, cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.Created)
            {
                Message = "Category created successfully."
            };
        }

        private async Task SaveCategory(ProductCategoryDTO dto, CancellationToken cancellationToken)
        {
            await HandlerContext.DbContext.ProductCategory.AddAsync(new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                ParentId = dto.ParentId,
                Priority = dto.Priority,
                Slug = dto.Slug
            }, cancellationToken);

            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);
        }

        private string GenerateSlug(string name)
        {
            
            return name.ToLower().Replace(' ', '-').Replace('.', '-');
        }
    }

    public class GetProductCategoriesHandler : CommandHandler<NoParameters>
    {
        public GetProductCategoriesHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(NoParameters request, CancellationToken cancellationToken)
        {
            var categories = await HandlerContext.DbContext.ProductCategory
                .Select(pc => new ProductCategoryDTO
                {
                    Id = pc.Id,
                    Name = pc.Name,
                    ParentId = pc.ParentId,
                    Priority = pc.Priority,
                    Slug = pc.Slug
                })
                .ToListAsync(cancellationToken);

            var categoryNames = categories.Select(c => c.Name).ToList();
            var message = string.Join(", ", categoryNames);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Data = categories,
                Message = message
            };
        }
    }

    public sealed record DeleteProductCategoryCommand(Guid CategoryId);

    public sealed class DeleteProductCategoryHandler : CommandHandler<DeleteProductCategoryCommand>
    {
        public DeleteProductCategoryHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await HandlerContext.DbContext.ProductCategory
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Category not found."
                };
            }

            HandlerContext.DbContext.ProductCategory.Remove(category);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Category deleted successfully."
            };
        }
    }

    public sealed record UpdateProductCategoryCommand(ProductCategoryDTO UpdatedCategory);

    public sealed class UpdateProductCategoryHandler : CommandHandler<UpdateProductCategoryCommand>
    {
        private readonly IValidator<ProductCategoryDTO> _validator;

        public UpdateProductCategoryHandler(IValidator<ProductCategoryDTO> validator, IHandlerContext context)
            : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdatedCategory;

            var category = await HandlerContext.DbContext.ProductCategory
                .FirstOrDefaultAsync(c => c.Id == dto.Id, cancellationToken);

            if (category == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "Category not found."
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

            category.Name = dto.Name;
            category.ParentId = dto.ParentId;
            category.Priority = dto.Priority;
            category.Slug = dto.Slug;

            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "Category updated successfully."
            };
        }
    }


    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class NoParameters { }
}
