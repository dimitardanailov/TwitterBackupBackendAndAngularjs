using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TwitterWebApplicationRepositories;

namespace TwitterWebApplication.Controllers
{
    public class ApplicationApiController<TEnity> : ApiController where TEnity : class
    {
        protected Repository<TEnity> repository;

        public ApplicationApiController(Repository<TEnity> repository)
        {
            this.repository = repository;
        }
    }
}
