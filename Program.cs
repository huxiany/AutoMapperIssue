using System;
using AutoMapper;

namespace AutoMapperIssue
{
    class Student
    {
        public string Class { get; set; }
        public JsonObject<UserInfo> UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class StudentDTO
    {
        public string Class { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            CreateMap<Student, StudentDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserInfo.Object.Name))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.UserInfo.Object.Age));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(x => x.AddProfile<AutoMapperProfileConfiguration>());

            var student = new Student
            {
                Class = "Grade 5",
                UserInfo = new UserInfo
                {
                    Name = "Rachel",
                    Age = 10
                }

            };

            var studentDTO = Mapper.Map<StudentDTO>(student);
            Console.WriteLine("Class = {0}", studentDTO.Class);
            Console.WriteLine("Name = {0}", studentDTO.Name);
            Console.WriteLine("Age = {0}", studentDTO.Age);
        }
    }
}
