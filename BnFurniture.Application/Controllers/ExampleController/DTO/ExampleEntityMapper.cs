//namespace BnFurniture.Application.Controllers.ExampleController.DTO
//{
//    public static class ExampleEntityMapper
//    {
//        public static ExampleEntityDTO EntityToDto(this Domain.Entities.ExampleEntity exampleEntity)
//        {
//            return new ExampleEntityDTO
//            {
//                Date = exampleEntity.Date,
//                TemperatureC = exampleEntity.TemperatureC,
//                TemperatureF = exampleEntity.TemperatureF,
//                Summary = exampleEntity.Summary
//            };
//        }

//        public static Domain.Entities.ExampleEntity DtoToEntity(this ExampleEntityDTO entityDto)
//        {
//            return new Domain.Entities.ExampleEntity
//            {
//                Date = entityDto.Date,
//                TemperatureC = entityDto.TemperatureC,
//                Summary = entityDto.Summary
//            };
//        }

//        public static Domain.Entities.ExampleEntity FormDtoToEntity(this ExampleEntityFormDTO entityForm)
//        {
//            return new Domain.Entities.ExampleEntity()
//            {
//                Date = entityForm.Date,
//                TemperatureC = entityForm.TemperatureC,
//                Summary = entityForm.Summary,
//            };
//        }
//    }
//}
