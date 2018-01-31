using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate;
using Microsoft.AspNet.Identity;
using FluentNHibernate.Mapping;
using System.ComponentModel.DataAnnotations;

namespace BEEL.Models.Identity
{
    public class User : IUser<int>
    {
        public virtual int Id { get; protected set; }
        [Display(Name = "Пользователь")]
        public virtual string UserName { get; set; }
        [Display(Name = "Пароль")]
        public virtual string PasswordHash { get; set; }
        [Display(Name = "ФИО")]
        public virtual string FullName { get; set; }
        [Display(Name = "Дата изменений")]
        public virtual DateTime ChangeDate { get; set; }

        [Display(Name = "Пользователь.Просмотр")]
        public virtual bool user_list { get; set; }
        [Display(Name = "Пользователь.Изменить")]
        public virtual bool user_edit { get; set; }
        [Display(Name = "Пользователь.Удалить")]
        public virtual bool user_delete { get; set; }
        [Display(Name = "Товар.Просмотр")]
        public virtual bool prod_list { get; set; }
        [Display(Name = "Товар.Изменить")]
        public virtual bool prod_edit { get; set; }
        [Display(Name = "Товар.Удалить")]
        public virtual bool prod_delete { get; set; }

        

        public class Map : ClassMap<User>
        {
            public Map()
            {
                Id(x => x.Id).GeneratedBy.Identity();
                Map(x => x.UserName).Not.Nullable();
                Map(x => x.PasswordHash).Not.Nullable();
                Map(x => x.FullName);
                Map(x => x.ChangeDate);

                Map(x => x.user_list);
                Map(x => x.user_edit);
                Map(x => x.user_delete);
                Map(x => x.prod_list);
                Map(x => x.prod_edit);
                Map(x => x.prod_delete);
            }
        }
    }
}