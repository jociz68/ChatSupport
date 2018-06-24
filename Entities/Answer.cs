using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Answer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime AnswerTime { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

    }

}
