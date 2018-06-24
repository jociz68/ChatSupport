using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    interface IDataManager
    {
        bool CreatQuestion(Question question);
        bool CreateAnswer(Answer answer, int questionId);
        
        ICollection<Question> GetQuestionsByAnswerId(int answerId);
        
        ICollection<Question> GetActiveQuestions(string session);

        int GetActiveQuestionsCount();

        ICollection<Question> GetPagedActiveQuestions(int startNumber, int pageSize);
    }
}
