using Application.Core.Models;
using Application.Core.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core.Services.Concrete
{
    public class User : IUser
    {
        private readonly IUserRepository _userRepository;
        public User(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel Register(string name, string lastName, string password, string email)
        {
            var user = new UserModel
            {
                Usr_Name = name,
                Usr_LastName = lastName,
                Usr_Password = password,
                Usr_Email = email,
                Usr_Created = DateTime.Now.Date
            };
            return user;
        }

        public bool Login(string email, string password)
        {
            var user = _userRepository.GetUserByMail(email);
            if (user != null && user.Usr_Password == password)
                return true;
            return false;
        }

        public UserModel GetFirstUser()
            => _userRepository.GetFirstUser();

        public UserModel AddNewUserToDB(UserModel user)
            => _userRepository.AddNewUsertToDB(user);

        public string? ChangePassword(string email, string password, string newPassword)
            => _userRepository.ChangePassword(email, password, newPassword);

        public int? AddBookToUserAccount(Books book)
            => _userRepository.AddBookToUserAccount(book);
    }
}

