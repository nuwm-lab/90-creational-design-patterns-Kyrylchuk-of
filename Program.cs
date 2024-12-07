using System;
using System.Collections.Generic;

namespace EducationalProgramBuilder
{
    /// <summary>
    /// Клас для представлення освітньої програми.
    /// </summary>
    public class EducationalProgram
    {
        public List<string> Subjects { get; private set; }
        public int DurationInWeeks { get; private set; }
        public string DifficultyLevel { get; private set; }

        public EducationalProgram()
        {
            Subjects = new List<string>();
        }

        public void SetDuration(int weeks)
        {
            DurationInWeeks = weeks;
        }

        public void SetDifficultyLevel(string difficulty)
        {
            DifficultyLevel = difficulty;
        }

        public void AddSubject(string subject)
        {
            Subjects.Add(subject);
        }

        public void ShowDetails()
        {
            Console.WriteLine("Освітня програма:");
            Console.WriteLine($"Тривалість: {DurationInWeeks} тижнів");
            Console.WriteLine($"Рівень складності: {DifficultyLevel}");
            Console.WriteLine("Предмети:");
            foreach (var subject in Subjects)
            {
                Console.WriteLine($"- {subject}");
            }
        }
    }

    /// <summary>
    /// Інтерфейс будівельника для освітньої програми.
    /// </summary>
    public interface IEducationalProgramBuilder
    {
        void SetDuration(int weeks);
        void SetDifficultyLevel(string difficulty);
        void AddSubject(string subject);
        EducationalProgram Build();
    }

    /// <summary>
    /// Конкретний будівельник для створення освітньої програми.
    /// </summary>
    public class EducationalProgramBuilder : IEducationalProgramBuilder
    {
        private EducationalProgram _program;

        public EducationalProgramBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _program = new EducationalProgram();
        }

        public void SetDuration(int weeks)
        {
            _program.SetDuration(weeks);
        }

        public void SetDifficultyLevel(string difficulty)
        {
            _program.SetDifficultyLevel(difficulty);
        }

        public void AddSubject(string subject)
        {
            _program.AddSubject(subject);
        }

        public EducationalProgram Build()
        {
            EducationalProgram result = _program;
            Reset();
            return result;
        }
    }

    /// <summary>
    /// Клас-директор для керування створенням програм.
    /// </summary>
    public class EducationalProgramDirector
    {
        private readonly IEducationalProgramBuilder _builder;

        public EducationalProgramDirector(IEducationalProgramBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructBasicProgram()
        {
            _builder.SetDuration(4);
            _builder.SetDifficultyLevel("Базовий");
            _builder.AddSubject("Математика");
            _builder.AddSubject("Інформатика");
        }

        public void ConstructAdvancedProgram()
        {
            _builder.SetDuration(12);
            _builder.SetDifficultyLevel("Просунутий");
            _builder.AddSubject("Алгоритми та структури даних");
            _builder.AddSubject("Штучний інтелект");
            _builder.AddSubject("Машинне навчання");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення будівельника та директора
            var builder = new EducationalProgramBuilder();
            var director = new EducationalProgramDirector(builder);

            // Побудова базової програми
            director.ConstructBasicProgram();
            var basicProgram = builder.Build();
            basicProgram.ShowDetails();

            Console.WriteLine();

            // Побудова просунутої програми
            director.ConstructAdvancedProgram();
            var advancedProgram = builder.Build();
            advancedProgram.ShowDetails();
        }
    }
}
