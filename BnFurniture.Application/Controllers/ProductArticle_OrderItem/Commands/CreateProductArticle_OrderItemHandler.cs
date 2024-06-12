//using BnFurniture.Application.Abstractions;
//using BnFurniture.Application.Controllers.ProductArticle_OrderItem.DTO;
//using BnFurniture.Application.Extensions;
//using BnFurniture.Domain.Responses;
//using System;
//using System.Collections.Generic;
//using System.Linq;    
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace BnFurniture.Application.Controllers.ProductArticle_OrderItem.Commands
//{
//    public sealed record CreateProductArticle_OrderItemCommand(CreateProductArticle_OrderItemDTO Dto);
//    public sealed class CreateProductArticle_OrderItemHandler : CommandHandler<CreateProductArticle_OrderItemCommand>
//    {
//        private readonly CreateProductArticle_OrderItemDTOValidator _validator;

//        public CreateProductArticle_OrderItemHandler(CreateProductArticle_OrderItemDTOValidator validator, IHandlerContext context) : base(context)
//        {
//            _validator = validator;
//        }

//        public override async Task<ApiCommandResponse> Handle(CreateProductArticle_OrderItemCommand command, CancellationToken cancellationToken)
//        {
//            var dto = command.Dto;

//            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

//            if (!validationResult.IsValid)
//            {
//                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
//                {
//                    Message = "Validation failed.",
//                    Errors = validationResult.ToApiResponseErrors()
//                };
//            }

//            var newProductArticleOrderItem = new ProductArticle_OrderItem
//            {
//                ProductArticleId = dto.ProductArticleId,
//                OrderItemId = dto.OrderItemId
//            };

//            await HandlerContext.DbContext.ProductArticleOrderItem.AddAsync(newProductArticleOrderItem, cancellationToken);
//            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

//            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
//            {
//                Message = "ProductArticle_OrderItem created successfully."
//            };
//        }
//    }
//}
//using BnFurniture.Application.Abstractions;
//using BnFurniture.Application.Controllers.ProductArticle_OrderItem.DTO;
//using BnFurniture.Application.Extensions;
//using BnFurniture.Domain.Entities;
//using BnFurniture.Domain.Responses;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace BnFurniture.Application.Controllers.ProductArticle_OrderItem.Commands
//{
//    public sealed record CreateProductArticle_OrderItemCommand(CreateProductArticle_OrderItemDTO Dto);

//    public sealed class CreateProductArticle_OrderItemHandler : CommandHandler<CreateProductArticle_OrderItemCommand>
//    {
//        public CreateProductArticle_OrderItemHandler(IHandlerContext context) : base(context) { }


//        public override async Task<ApiCommandResponse> Handle(CreateProductArticle_OrderItemCommand request, CancellationToken cancellationToken)
//        {


//            var configuration = new BnFurniture.Domain.Entities.ProductArticle_OrderItem
//            {

//                ProductArticleId = request.Dto.ProductArticleId,
//                OrderItemId = request.Dto.OrderItemId
//            };

//            await HandlerContext.DbContext.ProductArticleOrderItem.AnyAsync(configuration, cancellationToken);
//            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

//            return new ApiCommandResponse(true, (int)System.Net.HttpStatusCode.OK)
//            {
//                Message = "ProductArticle_OrderItem created successfully."
//            };
//        }
//    }
//}
using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticle_OrderItem.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductArticle_OrderItem.Commands
{
    public sealed record CreateProductArticle_OrderItemCommand(CreateProductArticle_OrderItemDTO Dto);

    public sealed class CreateProductArticle_OrderItemHandler : CommandHandler<CreateProductArticle_OrderItemCommand>
    {
        public CreateProductArticle_OrderItemHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiCommandResponse> Handle(CreateProductArticle_OrderItemCommand request, CancellationToken cancellationToken)
        {
            // Проверка существования ProductArticle и OrderItem
            var productArticleExists = await HandlerContext.DbContext.ProductArticle
                .AnyAsync(pa => pa.Article == request.Dto.ProductArticleId, cancellationToken);
            var orderItemExists = await HandlerContext.DbContext.OrderItem
                .AnyAsync(oi => oi.Id == request.Dto.OrderItemId, cancellationToken);

            if (!productArticleExists)
            {
                return new ApiCommandResponse(false, 404)
                {
                    Message = "ProductArticle with the specified ID does not exist."
                };
            }

            if (!orderItemExists)
            {
                return new ApiCommandResponse(false, 404)
                {
                    Message = "OrderItem with the specified ID does not exist."
                };
            }

            var configuration = new BnFurniture.Domain.Entities.ProductArticle_OrderItem
            {
                ProductArticleId = request.Dto.ProductArticleId,
                OrderItemId = request.Dto.OrderItemId
            };

            await HandlerContext.DbContext.ProductArticle_OrderItem.AddAsync(configuration, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)System.Net.HttpStatusCode.OK)
            {
                Message = "ProductArticle_OrderItem created successfully."
            };
        }
    }
}

