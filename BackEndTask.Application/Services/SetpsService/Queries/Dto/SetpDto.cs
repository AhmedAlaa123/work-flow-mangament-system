using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.SetpsService.Queries.Dto
{
    public class SetpDto
    {
        public Guid Id { get; set; }
        public string StepName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }  // this is flage for this step is show in  
        public bool IsHasExternalValidator { get; set; }
        public string ExtrnalValidatorUrl { get; set; }
    }
}
