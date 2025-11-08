using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.SetpsService.Commands.Dto
{
    public class StepCreateDto
    {
        public string StepName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }  // this is flage for this step is show in  
 
        public bool IsHasExternalValidator { get; set; }
        public string ExtrnalValidatorUrl { get; set; }
    }
}
