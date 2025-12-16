using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            return question;
        }

        public async Task<IEnumerable<Question>> GetByQuizIdAsync(int quizId)
        {
            return await _questionRepository.GetByQuizIdAsync(quizId);
        }

        public async Task<Question> CreateAsync(int quizId,string questionText,string questionType)
        {
            var question = new Question
            {
                QuizId = quizId,
                QuestionText = questionText,
                QuestionType = questionType
            };

            await _questionRepository.AddAsync(question);
            return question;
        }

        public async Task<Question> UpdateAsync(int id, string questionText,string questionType)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            question.QuestionText = questionText;
            question.QuestionType = questionType;

            await _questionRepository.UpdateAsync(question);
            return question;
        }

        public async Task DeleteAsync(int id)
        {
            await _questionRepository.DeleteAsync(id);
        }
    }
}
