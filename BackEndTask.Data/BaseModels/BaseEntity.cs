using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Data.BaseModels
{
    public class BaseEntity<T>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
    public class FullAudit<T>:BaseEntity<T>
    {
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    
    }
    public class FullAudit : BaseEntity
    {
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }

    }
}
