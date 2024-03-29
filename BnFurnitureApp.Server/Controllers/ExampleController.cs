using BnFurniture.Application.ExampleController.Commands;
using BnFurniture.Application.ExampleController.DTO;
using BnFurniture.Application.ExampleController.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers
{
    /// <summary>
    /// Пример API-контроллера.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        /// <summary>
        /// Медиатор должен быть подключён в каждом контроллере.
        /// </summary>
        private readonly IMediator _mediator;



        /// <summary>
        /// Конструктор, в который подключаются сервисы через Dependency Injection.
        /// </summary>
        public ExampleController(IMediator mediator)
        {
            _mediator = mediator;
        }



        /// <summary>
        /// Пример метода GET с параметром. Идёт запрос в слой <see cref="BnFurniture.Application"/> (где хранится бизнес-логика),
        /// и возвращается требуемый результат в соотсветствии с Запросом (Query) (в данном случае - <see cref="GetEntity"/>).
        /// </summary>
        /// <param name="days">
        /// Параметр, который передаётся в <see cref="GetEntity"/>. На реальном примере это может быть User ID
        /// и любые другие параметры.
        /// </param>
        /// <returns>Возвращает результат, описанный в <see cref="GetEntity.Response"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> GetExampleEntity(int days)
        {
            var query = new GetEntity.Query(days);
            var response = await _mediator.Send(query);

            return Ok(response.ExampleEntity);
        }



        /// <summary>
        /// Пример метода GET. Идёт запрос в слой <see cref="BnFurniture.Application"/> (где хранится бизнес-логика),
        /// и возвращается требуемый результат в соотсветствии с запросом (Query) (в данном случае - <see cref="GetEntityList"/>).
        /// </summary>
        /// <returns>Возвращает результат, описанный в <see cref="GetEntityList.Response"/>.</returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetExampleEntityList()
        {
            var query = new GetEntityList.Query();
            var response = await _mediator.Send(query);

            return Ok(response.ExampleEntityList);
        }



        /// <summary>
        /// Пример метода POST. Идёт запрос в слой <see cref="BnFurniture.Application"/> (где хранится бизнес-логика),
        /// и команда срабатывает без возвращения результата.
        /// </summary>
        /// <param name="model">Модель формы сайта.</param>
        /// <returns>Возвращает код 200 (ОК).</returns>
        [HttpPost]
        public async Task<IActionResult> CreateExampleEntity([FromForm] ExampleEntityFormDTO model)
        {
            var query = new CreateEntity.Command(model);
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
