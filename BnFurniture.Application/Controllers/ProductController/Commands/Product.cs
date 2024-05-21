using System;
using System.Threading;
using System.Threading.Tasks;
using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BnFurniture.Application.Extensions;
using BnFurniture.Application.Controllers.ProductCategoryController.Commands;

namespace BnFurniture.Application.Controllers.ProductController.Commands
{
    public sealed record CreateProductCommand(ProductDTO entityForm);

    public sealed class CreateProductHandler : CommandHandler<CreateProductCommand>
    {
        private readonly IValidator<ProductDTO> _validator;

        public CreateProductHandler(IValidator<ProductDTO> validator, IHandlerContext context)
            : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
            //var slug = GenerateUniqueSlug(dto.Name);
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                ProductTypeId = dto.ProductTypeId,
                AuthorId = dto.AuthorId,
                Name = dto.Name,
                Description = dto.Description,
                Summary = dto.Summary,
                ProductDetails = dto.ProductDetails,
                Priority = dto.Priority,
                Active = dto.Active,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug=dto.Slug
            };

            await HandlerContext.DbContext.Product.AddAsync(newProduct, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.Created)
            {
                Message = "Product created successfully."
            };
        }
        private string GenerateUniqueSlug(string name)
        {
          
            return name.ToLower().Replace(" ", "-");
        }

        public class GetAllProductsHandler : CommandHandler<NoParameters>
        {
            public GetAllProductsHandler(IHandlerContext context) : base(context) { }

            public override async Task<ApiCommandResponse> Handle(NoParameters request, CancellationToken cancellationToken)
            {
                var products = await HandlerContext.DbContext.Product
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        ProductTypeId = p.ProductTypeId,
                        AuthorId = p.AuthorId,
                        Name = p.Name,
                        Description = p.Description,
                        Summary = p.Summary,
                        ProductDetails = p.ProductDetails,
                        Priority = p.Priority,
                        Active = p.Active,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt
                       
                    })
                    .ToListAsync(cancellationToken);

                return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
                {
                    Data = products,
                    Message = "Products retrieved successfully."
                };
            }
        }

        public sealed record DeleteProductCommand(Guid Id);

        public sealed class DeleteProductHandler : CommandHandler<DeleteProductCommand>
        {
            public DeleteProductHandler(IHandlerContext context) : base(context) { }

            public override async Task<ApiCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await HandlerContext.DbContext.Product
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (product == null)
                {
                    return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                    {
                        Message = "Product not found."
                    };
                }

                HandlerContext.DbContext.Product.Remove(product);
                await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

                return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
                {
                    Message = "Product deleted successfully."
                };
            }
        }

        public sealed record UpdateProductCommand(ProductDTO UpdatedProduct);

        public sealed class UpdateProductHandler : CommandHandler<UpdateProductCommand>
        {
            private readonly IValidator<ProductDTO> _validator;

            public UpdateProductHandler(IValidator<ProductDTO> validator, IHandlerContext context)
                : base(context)
            {
                _validator = validator;
            }

            public override async Task<ApiCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var dto = request.UpdatedProduct;

                var product = await HandlerContext.DbContext.Product
                    .FirstOrDefaultAsync(p => p.Id == dto.Id, cancellationToken);

                if (product == null)
                {
                    return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                    {
                        Message = "Product not found."
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

                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Summary = dto.Summary;
                product.ProductDetails = dto.ProductDetails;
                product.Priority = dto.Priority;
                product.Active = dto.Active;
                product.UpdatedAt = DateTime.Now;

                await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

                return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
                {
                    Message = "Product updated successfully."
                };
            }
        }
    }
}
