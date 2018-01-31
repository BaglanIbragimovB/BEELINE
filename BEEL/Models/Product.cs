using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate;
using Microsoft.AspNet.Identity;
using FluentNHibernate.Mapping;
using System.ComponentModel.DataAnnotations;

namespace BEEL.Models
{
    public class Product
    {
        public virtual int Id { get; protected set; }
        [Display(Name = "Наименование товара")]
        public virtual string ProductName { get; set; }
        [Display(Name = "Количество")]
        public virtual string Count { get; set; }
        [Display(Name = "Дата изменений")]
        public virtual DateTime ChangeDate { get; set; }

        public class Map : ClassMap<Product>
        {
            public Map()
            {
                Id(x => x.Id).GeneratedBy.Identity();
                Map(x => x.ProductName);
                Map(x => x.Count);                
                Map(x => x.ChangeDate);
            }
        }
    }
}