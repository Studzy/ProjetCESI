﻿using ProjetCESI.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetCESI.Metier.Main
{
    public interface IAdminMetier
    {
        Task<IEnumerable<User>> GetUser();
        Task<bool> AnonymiseUser(User user);
        Task<bool> BanUserTemporary(User user);
        Task<bool> DeBan(User user);
    }
}