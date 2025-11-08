using BackEndTask.Data.BaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Data.Entites
{
    public  class Step: FullAudit
    {
        
        public string StepName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }  // this is flage for this step is show in transactions or not 
     
        public bool IsHasExternalValidator { get; set; }
        public string ExtrnalValidatorUrl { get; set; }
       
        public virtual ICollection<WorkFlowStep> WorkFlows { get; set; }
    }
}
