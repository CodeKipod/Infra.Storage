using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Roman.Ambinder.Storage.Common.Interfaces.Common;
using Storage.Examples.AspCore.Entities;
using Storage.Examples.AspCore.Repositories;

namespace Storage.Examples.AspCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IRepositoryFor<int, Person> _repository;

        public PeopleController(ILogger<PeopleController> logger,
            IRepositoryFor<int, Person> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public Task<IEnumerable<Person>> Get()
        {
            IEnumerable<Person> people = new Person[0];
            return Task.FromResult(people);
        }
    }
}
