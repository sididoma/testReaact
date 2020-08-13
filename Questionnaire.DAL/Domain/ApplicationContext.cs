using Microsoft.EntityFrameworkCore;
using Questionnaire.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data.Domain
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataType>().HasData(
                new DataType[]
                {
                    new DataType()
                    {
                        Id = 1,
                        Type = "string",
                        EnumDescription = null
                    },
                    new DataType()
                    {
                        Id = 2,
                            Type = "int",
                            EnumDescription = null
                    },
                    new DataType
                    {

                        Id = 3,
                        Type = "enum",
                        EnumDescription = "Мужской***женский"
                    },
                    new DataType
                    {

                        Id = 4,
                        Type = "date",
                        EnumDescription = null
                    },
                    new DataType
                    {
                        Id = 5,
                        Type = "enum",
                        EnumDescription = "Женат/замужем***Разведен(-а)***Холост(-ая)"
                    },
                    new DataType
                    {

                        Id = 6,
                        Type = "bool",
                        EnumDescription = null
                    }
                });
            builder.Entity<Question>().HasData(
                new Question[] {
                    new Question
                    {
                    Id = 1,
                    AnswerTypeId = 1,
                    QuestionDescription = "Введите имя"
                },
                    new Question
                    {
                        AnswerTypeId = 2,
                    Id = 2,
                        QuestionDescription = "Введите возраст"
                    },
                    new Question
                    {
                        AnswerTypeId = 3,
                    Id = 3,
                        QuestionDescription = "Выберите пол"
                    },
                    new Question
                    {
                        AnswerTypeId = 4,
                    Id = 4,
                        QuestionDescription = "Введите дату рождения"
                    },
                    new Question
                    {AnswerTypeId = 5,
                    Id = 5,
                        QuestionDescription = "Выберите семейное положение"
                    },
                    new Question
                    {
                        AnswerTypeId = 6,
                        Id = 6,
                        QuestionDescription = "Любите ли вы программировать?"
                    }
                }
                );
        }

        public DbSet<DataType> DataTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
