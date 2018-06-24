using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApp.Models
{
    public class Complex
    {
        public Question CurrentQuestion { get; set; }
        public Answer CurrentAnswer { get; set; }
        public List<Question> Question { get; set; }
        public List<Answer> Answers { get; set; }
        public  string Message { get; set; }

        public string[] Selected { get; set; }

        public string[] PrevSelected { get; set; }

        public int deviceId { get; set; }

        public bool IsUpdate { get; set; }

    }
}