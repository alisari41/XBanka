using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Dapper;
using DapperExtensions;
using Dommel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.DataAccess.Dapper
{
    public class DapperEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        string _tableName;
        readonly string _connectionString = @"Data Source =.\sqlexpress;Initial Catalog = Banka; Integrated Security = True";
        private SqlConnection SqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
        private IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            conn.Open();
            return conn;
        }


        private IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();



        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {

            _tableName = typeof(TEntity).Name;
            using (var connection = CreateConnection())
            {
                var result = connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {_tableName} WHERE Id=@id", new { id = id });
                if (result == null)
                    return null;
                return result;
            }
        }



        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            _tableName = typeof(TEntity).Name;
            using (var connection = CreateConnection())
            {
                DommelMapper.SetTableNameResolver(new CustomTableNameResolver());
                return filter == null
                    ? connection.Query<TEntity>($"SELECT * FROM {_tableName}").ToList()
                    : connection.Select(filter).ToList();
            }
        }


        public void Add(TEntity entity)
        {
            _tableName = typeof(TEntity).Name;
            var insertQuery = GenerateInsertQuery_ScopeIdentity();
            using (var connection = CreateConnection())
            {
                connection.ExecuteScalar<int>(insertQuery, entity);
            }
        }
        public void AddRange(TEntity entity)
        {
            _tableName = typeof(TEntity).Name;
            var query = GenerateInsertQuery();
            using (var connection = CreateConnection())
            {
                connection.Execute(query, entity);
            }
        }
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
            insertQuery.Append("(");
            var properties = GenerateListIdOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");
            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");
            return insertQuery.ToString();
        }


        public void Update(TEntity entity)
        {

            _tableName = typeof(TEntity).Name;
            var updateQuery = GenerateUpdateQuery();
            using (var connection = CreateConnection())
            {
                connection.Execute(updateQuery, entity);
            }
        }


        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE Id=@Id");
            return updateQuery.ToString();
        }


        public void Delete(TEntity entity)
        {

            _tableName = typeof(TEntity).Name;
            var deleteQuery = GenerateDeleteQuery();
            using (var connection = CreateConnection())
            {
                connection.Execute(deleteQuery, entity);
            }
        }

        private string GenerateDeleteQuery()
        {
            var deleteQuery = new StringBuilder($"Delete From {_tableName} ");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    //  deleteQuery.Append($"{property}=@{property},");
                }
            });
            deleteQuery.Remove(deleteQuery.Length - 1, 1);
            deleteQuery.Append(" WHERE Id=@Id");
            return deleteQuery.ToString();
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore") && prop.Name != "Id"
                    select prop.Name).ToList();
        }
        private static List<string> GenerateListIdOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {//Id elle girilmek istenirse
            return (from prop in listOfProperties
                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                where (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore") 
                select prop.Name).ToList();
        }

        private string GenerateInsertQuery_ScopeIdentity()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
            insertQuery.Append("(");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");
            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");
            insertQuery = new StringBuilder(string.Format("{0}; SELECT SCOPE_IDENTITY();", insertQuery, _tableName));
            return insertQuery.ToString();
        }
    }
    public class CustomTableNameResolver : DommelMapper.ITableNameResolver
    {
        public string ResolveTableName(Type type)
        {
            // Every table has prefix 'tbl'.
            return $"{type.Name}";
        }
    }
}
//public class DapperRepository<T> : IRepository<T> where T : BaseEntity
//{
//    string _tableName;
//    readonly string _connectionString = @"Data Source =.\sqlexpress;Initial Catalog = Banka; Integrated Security = True"
//    private SqlConnection SqlConnection()
//    {
//        return new SqlConnection(_connectionString);
//    }
//    private IDbConnection CreateConnection()
//    {
//        var conn = SqlConnection();
//        conn.Open();
//        return conn;
//    }
//    private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
//    public int Insert(T t)
//    {
//        _tableName = typeof(T).Name;
//        var insertQuery = GenerateInsertQuery_ScopeIdentity();
//        using (var connection = CreateConnection())
//        {
//            return connection.ExecuteScalar<int>(insertQuery, t);
//        }
//    }
//    public int InsertRange(IEnumerable<T> list)
//    {
//        _tableName = typeof(T).Name;
//        var inserted = 0;
//        var query = GenerateInsertQuery();
//        using (var connection = CreateConnection())
//        {
//            inserted += connection.Execute(query, list);
//        }
//        return inserted;
//    }
//    public int Update(T t)
//    {
//        _tableName = typeof(T).Name;
//        var updateQuery = GenerateUpdateQuery();
//        using (var connection = CreateConnection())
//        {
//            return connection.Execute(updateQuery, t);
//        }
//    }
//    public int Delete(int id, bool? realDelete = null)
//    {
//        _tableName = typeof(T).Name;
//        using (var connection = CreateConnection())
//        {
//            if (realDelete.HasValue && realDelete.Value)
//                return connection.Execute($"DELETE FROM {_tableName} WHERE id = @id", new { id = id });
//            else
//                return connection.Execute($"UPDATE {_tableName} SET silindi = 1 WHERE id = @id", new { id = id });
//        }
//    }
//    public T Get(int id)
//    {
//        _tableName = typeof(T).Name;
//        using (var connection = CreateConnection())
//        {
//            var result = connection.QuerySingleOrDefault<T>($"SELECT * FROM {_tableName} WHERE id=@id", new { id = id });
//            if (result == null)
//                return null;
//            return result;
//        }
//    }
//    public IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null)
//    {
//        _tableName = typeof(T).Name;
//        using (var connection = CreateConnection())
//        {
//            DommelMapper.SetTableNameResolver(new CustomTableNameResolver());
//            return filter == null
//            ? connection.Query<T>($"SELECT * FROM {_tableName}")
//            : connection.Select(filter);
//        }
//    }
//    public long GetList_Count(Expression<Func<T, bool>> filter = null)
//    {
//        _tableName = typeof(T).Name;
//        using (var connection = CreateConnection())
//        {
//            DommelMapper.SetTableNameResolver(new CustomTableNameResolver());
//            return filter == null
//            ? connection.QueryFirstOrDefault<long>($"SELECT COUNT(1) FROM {_tableName}")
//            : connection.Count<T>(filter);
//        }
//    }
//    public IEnumerable<T> GetAllIds(string fieldName, List<int> Id)
//    {
//        _tableName = typeof(T).Name;
//        string ids = string.Empty;
//        foreach (var id in Id)
//            ids += string.Format("{0},", id);
//        string query = string.Format("SELECT * FROM {0} WHERE {1} IN ({2})", _tableName, fieldName, ids.TrimEnd(','));
//        using (var connection = CreateConnection())
//        {
//            return connection.Query<T>(query);
//        }
//    }
//    public IEnumerable<T> GetCustomQuery(string customQuery, DynamicParameters parameters, CommandType commandType)
//    {
//        using (var connection = CreateConnection())
//        {
//            return connection.Query<T>(customQuery, parameters, commandType: commandType);
//        }
//    }
//    public string GetCustomQueryToString(string customQuery, DynamicParameters parameters, CommandType commandType)
//    {
//        using (var connection = CreateConnection())
//        {
//            return connection.ExecuteScalar<string>(customQuery, parameters, commandType: commandType);
//        }
//    }
//    private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
//    {
//        return (from prop in listOfProperties
//                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
//                where (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore") && prop.Name != "id"
//                select prop.Name).ToList();
//    }
//    private string GenerateInsertQuery()
//    {
//        var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
//        insertQuery.Append("(");
//        var properties = GenerateListOfProperties(GetProperties);
//        properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
//        insertQuery
//        .Remove(insertQuery.Length - 1, 1)
//        .Append(") VALUES (");
//        properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
//        insertQuery
//        .Remove(insertQuery.Length - 1, 1)
//        .Append(")");
//        return insertQuery.ToString();
//    }
//    private string GenerateInsertQuery_ScopeIdentity()
//    {
//        var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
//        insertQuery.Append("(");
//        var properties = GenerateListOfProperties(GetProperties);
//        properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
//        insertQuery
//        .Remove(insertQuery.Length - 1, 1)
//        .Append(") VALUES (");
//        properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
//        insertQuery
//        .Remove(insertQuery.Length - 1, 1)
//        .Append(")");
//        insertQuery = new StringBuilder(string.Format("{0}; SELECT SCOPE_IDENTITY();", insertQuery, _tableName));
//        return insertQuery.ToString();
//    }
//    private string GenerateUpdateQuery()
//    {
//        var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
//        var properties = GenerateListOfProperties(GetProperties);
//        properties.ForEach(property =>
//        {
//            if (!property.Equals("id"))
//            {
//                updateQuery.Append($"{property}=@{property},");
//            }
//        });
//        updateQuery.Remove(updateQuery.Length - 1, 1);
//        updateQuery.Append(" WHERE id=@id");
//        return updateQuery.ToString();
//    }
//    public class CustomTableNameResolver : DommelMapper.ITableNameResolver
//    {
//        public string ResolveTableName(Type type)
//        {
//            // Every table has prefix 'tbl'.
//            return $"{type.Name}";
//        }
//    }
//}