using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.SearchResultDto;
using Weblog.Application.Features;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            List<SearchResultDto> searchResultDtos = await _mediator.Send(new SearchContentQuery(searchText));
            return Ok(searchResultDtos);
        }
    }
}