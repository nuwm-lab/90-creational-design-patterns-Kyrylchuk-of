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
            if (weeks <= 0)
                throw new ArgumentException("Тривалість навчання має бути більше 0.");
            DurationInWeeks = weeks;
        }

        public void SetDifficultyLevel(string difficulty)
        {
            if (string.IsNullOrEmpty(difficulty))
                throw new ArgumentException("Рівень складності не може бути порожнім.");
            DifficultyLevel = difficulty;
        }

        public void AddSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentException("Назва предмета не може бути порожньою.");
            Subjects.Add(subject);
        }

        public void ShowDetails()
        {
            Console.WriteLine("\n=== Освітня програма ===");
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

        private void Reset()
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
            var result = _program;
            Reset();
            return result;
        }
    }

    /// <summary>
    /// Клас-директор для керування створенням освітньої програми.
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
            _builder.SetDuration(8);
            _builder.SetDifficultyLevel("Базовий");
            _builder.AddSubject("Математика");
            _builder.AddSubject("Історія");
        }

        public void ConstructAdvancedProgram()
        {
            _builder.SetDuration(16);
            _builder.SetDifficultyLevel("Складний");
            _builder.AddSubject("Алгоритми");
            _builder.AddSubject("Машинне навчання");
            _builder.AddSubject("Фізика");
        }
    }

    /// <summary>
    /// Головний клас програми.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Створення будівельника
            var builder = new EducationalProgramBuilder();

            // Побудова програми через директора
            var director = new EducationalProgramDirector(builder);

            // Створення базової програми
            Console.WriteLine("Створення базової програми...");
            director.ConstructBasicProgram();
            var basicProgram = builder.Build();
            basicProgram.ShowDetails();

            // Створення просунутої програми
            Console.WriteLine("\nСтворення просунутої програми...");
            director.ConstructAdvancedProgram();
            var advancedProgram = builder.Build();
            advancedProgram.ShowDetails();

            // Побудова кастомної програми користувачем
            Console.WriteLine("\nСтворення кастомної програми...");
            Console.Write("Введіть тривалість навчання (у тижнях): ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Введіть рівень складності (наприклад, Легкий, Середній, Складний): ");
            string difficulty = Console.ReadLine();

            builder.SetDuration(duration);
            builder.SetDifficultyLevel(difficulty);

            Console.WriteLine("Додайте предмети до програми. Для завершення введіть 'стоп'.");
            while (true)
            {
                Console.Write("Введіть предмет: ");
                string subject = Console.ReadLine();
                if (subject.ToLower() == "стоп")
                    break;
                builder.AddSubject(subject);
            }

            var customProgram = builder.Build();
            customProgram.ShowDetails();
        }
    }
}
