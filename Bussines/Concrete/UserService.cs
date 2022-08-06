using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bussines.Abstract;
using Bussines.Configuration.Auth;
using Bussines.Configuration.Helper;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DTO.User;
using Microsoft.Extensions.Caching.Distributed;
using Models.Entities;

namespace Bussines.Concrete
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public UserService(IUserRepository userRepository,IMapper mapper, IDistributedCache distributedCache)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
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

            // burada yaptığımız işlemi AutoMapper aldık.
            //user.Permissions = register.UserPermissions.Select(x => new UserPermission()
            //{
            //    Permission = x
            //}).ToList();

            _userRepository.Add(user);
            _userRepository.SaveChanges();

            var key = StringHelper.CreateCacheKey(user.Name, user.Id);
            var cachePermission = System.Text.Json.JsonSerializer.Serialize(register.UserPermissions);

            _distributedCache.SetString(key,cachePermission);

   

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
