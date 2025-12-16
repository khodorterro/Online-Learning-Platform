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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Answer> GetByIdAsync(int id)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException("Answer not found");

            return answer;
        }

        public async Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId)
        {
            return await _answerRepository.GetByQuestionIdAsync(questionId);
        }

        public async Task<Answer> CreateAsync(int questionId, string answerText,bool isCorrect)
        {
            var answer = new Answer
            {
                QuestionId = questionId,
                AnswerText = answerText,
                IsCorrect = isCorrect
            };

            await _answerRepository.AddAsync(answer);
            return answer;
        }

        public async Task<Answer> UpdateAsync(int id,string answerText,bool isCorrect)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException("Answer not found");

            answer.AnswerText = answerText;
            answer.IsCorrect = isCorrect;

            await _answerRepository.UpdateAsync(answer);
            await _answerRepository.UpdateAsync(answer);
            return answer;
        }

        public async Task DeleteAsync(int id)
        {
            await _answerRepository.DeleteAsync(id);
        }
    }
}
