using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeusPark.Data.MySql;
using ZeusPark.Service.Model;

namespace ZeusPark.Service.Product
{
    public class CommentService
    {
        public CommentService()
        {

        }

        public List<QuestionVM> GetQuestionsByProductID(int productId)
        {
            List<QuestionVM> questionlist = new List<QuestionVM>();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var questions = entity.questions.Where(x => x.ProductID == productId).OrderByDescending(x => x.SubmitTime).ToList();
                if (null != questions)
                {
                    foreach(var ques in questions)
                    {
                        QuestionVM vm = new QuestionVM()
                        {
                            QuestionID = ques.QuestionId,
                            ProductID = ques.ProductID,
                            Question = ques.Question,
                            Answer = ques.Answer,
                            SubmitTime = ques.SubmitTime,
                            SubmitBy = ques.SubmitBy
                        };

                        questionlist.Add(vm);
                    }
                }

            }

            return questionlist;
        }

        public void AddQuestion(QuestionVM questionvm)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                question ques = new question();
                ques.ProductID = questionvm.ProductID;
                ques.Question = questionvm.Question;
                ques.SubmitBy = string.IsNullOrEmpty(ques.SubmitBy) ? "匿名" : ques.SubmitBy;
                ques.SubmitTime = DateTime.Now;

                entity.questions.Add(ques);
                entity.SaveChanges();
            }
        }

        public List<CommentVM> GetCommentByProductID(int productId)
        {
            List<CommentVM> commentlist = new List<CommentVM>();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var comments = entity.comments.Where(x => x.ProductID == productId).OrderByDescending(x => x.SubmitTime).ToList();
                if (null != comments)
                {
                    foreach (var comment in comments)
                    {
                        CommentVM vm = new CommentVM()
                        {
                            CommentID = comment.CommentID,
                            ProductID = comment.ProductID,
                            Content = comment.Content,
                            SubmitTime = comment.SubmitTime,
                            SubmitBy = comment.SubmitBy
                        };

                        commentlist.Add(vm);
                    }
                }

            }

            return commentlist;
        }


    }
}
