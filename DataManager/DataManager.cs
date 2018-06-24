using System;
using System.Collections.Generic;
using System.Linq;
using DataDbModel;
using Entities;


namespace Database
{
    public class DataManager : IDataManager
    {
        #region Public Methods

        public bool AddServices(string login, string password)
        {
            using (var context = new ChatModel())
            {
                var service = new Service()
                {
                    Login = login,
                    Password = password
                };
                context.Services.Add(service);
                return context.SaveChanges() == 1;
            }
        }

        public int GetServiceUserId(string login)
        {
            using (var context = new ChatModel())
            {
                var serviceUser = context.Services.FirstOrDefault(u => u.Login.Equals(login, StringComparison.InvariantCulture));
                return serviceUser != null ? serviceUser.Id : -1;
            }
        }

        public bool IsValidLogin(string login, string password)
        {
            using (var context = new ChatModel())
            {
                var service = context.Services.SingleOrDefault(s => s.Login.Equals(login, StringComparison.InvariantCulture));
                if (service == null)
                {
                    return false;
                }
                return service.Password.Equals(password);
            }
        }

        public bool CreateAnswer(Answer answer, int questionId)
        {
            using (var context = new ChatModel())
            {
                var answerToBe = new Answer()
                {
                    Text = answer.Text,
                    ServiceId = answer.ServiceId,
                    AnswerTime = DateTime.Now
                };
                context.Answers.Add(answerToBe);
                if (context.SaveChanges() == 1)
                {
                    return AddAnswerToQuestion(answerToBe.Id, questionId);
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CreatQuestion(Question questionData)
        {
            using (var context = new ChatModel())
            {
                var question = new Question()
                {
                    Text = questionData.Text,
                    SessionId = questionData.SessionId,
                    CreateTime = DateTime.Now
                };
                context.Questions.Add(question);
                return context.SaveChanges() == 1;
            }
        }

        public int GetActiveQuestionsCount()
        {
            throw new NotImplementedException();
        }

        public ICollection<Question> GetPagedActiveQuestions(int startNumber, int pageSize)
        {
            throw new NotImplementedException();
        }


        public ICollection<Question> GetQuestionsByAnswerId(int answerId)
        {
            using (var context = new ChatModel())
            {
                return context.Questions.Where(q => q.Answer.Id == answerId).ToList();
            }
        }

        public ICollection<Question> GetQuestionsNotAnswered()
        {
            using (var context = new ChatModel())
            {
                return context.Questions.Where(q => q.AnswerId == null).ToList();
            }
        }

        public ICollection<Question> GetActiveQuestions(string session)
        {
            using (var context = new ChatModel())
            {

                var list = context.Questions
                    .Where(q => q.SessionId.Equals(session))
                    .ToList();
                foreach (var item in list)
                {
                    var answer = context.Answers.FirstOrDefault(a => a.Id == item.AnswerId);
                    if (answer != null)
                    {
                        item.Answer = answer;
                    }

                }
                return list.OrderBy(o =>
                {
                    if (o.Answer != null)
                    {
                        return o.Answer.AnswerTime;
                    }
                    else
                    {
                        return o.CreateTime;
                    }
                }).ToList();
            }
        }

        #endregion

        #region private
        private bool AddAnswerToQuestion(int answernId, int questionId)
        {
            using (var context = new ChatModel())
            {
                var question = context.Questions.SingleOrDefault(q => q.Id == questionId);
                question.AnswerId = answernId;
                return context.SaveChanges() == 1;
            }
        }
        #endregion private
    }
}
