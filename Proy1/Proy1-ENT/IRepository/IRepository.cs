using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.IRepository
{
    public interface IRepository <TEntity> where TEntity:class
    {
        //CREATES
        //Agrega un registro al repositorio (SQL SERVER) a la tabla TEntity
        void Add(TEntity entity);
        //Agrega un grupo de registros al repositorio (SQL SERVER) a la tabla TEntity
        void AddRange(IEnumerable<TEntity> entities);

        //READS
        //Obtiene el Registro con PrimaryKey= Id de la tabla Entity
        TEntity Get(int? Id);
        //Obtiene todos los registros de la tabla Entity
        IEnumerable<TEntity> GetAll();
        //Obtiene todos los registros  de la tabla Entity que cumplen con la condicion
        // predicate es una expresion lambda que tiene como parametro de entrada a Tentity y devolvera una expresion booleana.
        //Si la expresion es true se agrega a la lista de registros a devolver
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        
        //UPDATES
        //Actualiza un registro al repositorio (SQL SERVER) a la tabla TEntity
        void Update(TEntity entity);
        //Actualiza un grupo de registros al repositorio (SQL SERVER) a la tabla TEntity
        void UpdateRange(IEnumerable<TEntity> entities);

        //DELETES
        //Elimina un registro al repositorio (SQL SERVER) a la tabla TEntity
        void Delete(TEntity entity);
        //Elimina un grupo de registros al repositorio (SQL SERVER) a la tabla TEntity
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
