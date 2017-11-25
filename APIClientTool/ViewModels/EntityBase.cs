using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClientTool.ViewModels
{
    public class EntityBase
    {
        public enum StatusCode
        {
            Continue = 100,
            Success = 200,
            MultipleChoices = 300,
            BadRequest = 400,
            Forbidden = 403,
            NotFound = 404,
            InternalServerError = 500
        }
    }
}
