using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// 
    /// </summary>
    public  class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreateTime { get; set; }

        public string SessionId { get; set; }

        public int? AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}
