 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Microsoft.EntityFrameworkCore ;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BackEndTask.Data.Entites;
using Microsoft.AspNetCore.Identity;
namespace BackEndTask.Persistance.DbContext
{
    public class WorkFlowTrakDbContext  :IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
    {
        public WorkFlowTrakDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<WorkFlow> WorkFlows { get; set; }
        public virtual DbSet<WorkFlowStep> WorkFlowSteps { get; set; }
    }
}
