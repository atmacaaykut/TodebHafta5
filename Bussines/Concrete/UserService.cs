using System;
using System.Collections.Generic;
using AutoMapper;
using Bussines.Abstract;
using Bussines.Configuration.Auth;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DTO.User;
using Models.Entities;

namespace Bussines.Concrete
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public CommandResponse Register(CreateUserRegisterRequest register)
        {
            /// validasyonlar yazıldı.

            byte[] passwordHash,passwordSalt;
            HashHelper.CreatePasswordHash(register.UserPassword, out passwordHash,out passwordSalt);

            var user = _mapper.Map<User>(register);

            user.Password = new UserPassword()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return new CommandResponse()
            {
                Message = "Kullanıcı başarılı şekilde kaydedildi",
                Status = true
            };


        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
