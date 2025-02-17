﻿using ProjetCESI.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetCESI.Metier
{
    public interface IAdminMetier : IMetier<User>
    {
        Task<IEnumerable<User>> GetUser();
        Task<bool> AnonymiseUser(User user);
        Task<bool> BanUserTemporary(User user, int time);
        Task<bool> DeBan(User user);
        Task<bool> BanUserPermanent(User user);
        Task<User> UpdateInfoUser(User user, string newUsername);
    }
}