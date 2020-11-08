using POLYCLINIC.BLL.Infrastructure;
using POLYCLINIC.BLL.Models;
using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.BLL.Interfaces
{
    public static class EntityModelConverterExtension
    {
        public static ICollection<Model> ModelList<Entity, Model>(this ICollection<Entity> data) where Entity : BaseEntity where Model : BaseModel<Entity>, new()
        {
            return data.Select(e => new Model() { Entity = e }).ToList();
        }

        public static ICollection<Entity> EntityList<Entity, Model>(this ICollection<Model> data) where Entity : BaseEntity where Model : BaseModel<Entity>
        {
            return data.Select(i => i.Entity).ToList();
        }
    }
}
