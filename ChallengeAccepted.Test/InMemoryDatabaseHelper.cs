using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ChallengeAccepted.Test
{
    public static class InMemoryDatabaseHelper
    {
        private const string ErrorMessageEntity = "Could not access entity '{1}' on entity context '{0}'.";
        private const string ErrorMessageEentityContext = "Could not create instance of entity context '{0}'.";

        
        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1>(
            IReadOnlyCollection<TEntity1> entries1)
            where TEntity1 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, object, object, object, object, object, object, object, object>(
                 entries1, null, null, null, null, null, null, null, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2)
            where TEntity1 : class
            where TEntity2 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, object, object, object, object, object, object, object>(
                entries1, entries2, null, null, null, null, null, null, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2, TEntity3>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2,
            IReadOnlyCollection<TEntity3> entries3)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, TEntity3, object, object, object, object, object, object>(
                entries1, entries2, entries3, null, null, null, null, null, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2,
            IReadOnlyCollection<TEntity3> entries3,
            IReadOnlyCollection<TEntity4> entries4)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, object, object, object, object, object>(
                entries1, entries2, entries3, entries4, null, null, null, null, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2,
            IReadOnlyCollection<TEntity3> entries3,
            IReadOnlyCollection<TEntity4> entries4,
            IReadOnlyCollection<TEntity5> entries5)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
            where TEntity5 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, object, object, object, object>(
                entries1, entries2, entries3, entries4, entries5, null, null, null, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2,
            IReadOnlyCollection<TEntity3> entries3,
            IReadOnlyCollection<TEntity4> entries4,
            IReadOnlyCollection<TEntity5> entries5,
            IReadOnlyCollection<TEntity6> entries6)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
            where TEntity5 : class
            where TEntity6 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, object, object, object>(
                entries1, entries2, entries3, entries4, entries5, entries6, null, null, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2,
            IReadOnlyCollection<TEntity3> entries3,
            IReadOnlyCollection<TEntity4> entries4,
            IReadOnlyCollection<TEntity5> entries5,
            IReadOnlyCollection<TEntity6> entries6,
            IReadOnlyCollection<TEntity7> entries7)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
            where TEntity5 : class
            where TEntity6 : class
            where TEntity7 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, object, object>(
                entries1, entries2, entries3, entries4, entries5, entries6, entries7, null, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2,
            IReadOnlyCollection<TEntity3> entries3,
            IReadOnlyCollection<TEntity4> entries4,
            IReadOnlyCollection<TEntity5> entries5,
            IReadOnlyCollection<TEntity6> entries6,
            IReadOnlyCollection<TEntity7> entries7,
            IReadOnlyCollection<TEntity8> entries8)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
            where TEntity5 : class
            where TEntity6 : class
            where TEntity7 : class
            where TEntity8 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, object>(
                entries1, entries2, entries3, entries4, entries5, entries6, entries7, entries8, null);
        }

        public static DbContextOptions<TEntityContext> Create<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9>(
            IReadOnlyCollection<TEntity1> entries1,
            IReadOnlyCollection<TEntity2> entries2,
            IReadOnlyCollection<TEntity3> entries3,
            IReadOnlyCollection<TEntity4> entries4,
            IReadOnlyCollection<TEntity5> entries5,
            IReadOnlyCollection<TEntity6> entries6,
            IReadOnlyCollection<TEntity7> entries7,
            IReadOnlyCollection<TEntity8> entries8,
            IReadOnlyCollection<TEntity9> entries9)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
            where TEntity5 : class
            where TEntity6 : class
            where TEntity7 : class
            where TEntity8 : class
            where TEntity9 : class
            where TEntityContext : DbContext
        {
            return CreateDbAndEntities<TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9>(
                entries1, entries2, entries3, entries4, entries5, entries6, entries7, entries8, entries9);
        }

        private static DbContextOptions<TEntityContext> CreateDbAndEntities
            <TEntityContext, TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9>(
            IReadOnlyCollection<TEntity1>? entries1,
            IReadOnlyCollection<TEntity2>? entries2,
            IReadOnlyCollection<TEntity3>? entries3,
            IReadOnlyCollection<TEntity4>? entries4,
            IReadOnlyCollection<TEntity5>? entries5,
            IReadOnlyCollection<TEntity6>? entries6,
            IReadOnlyCollection<TEntity7>? entries7,
            IReadOnlyCollection<TEntity8>? entries8,
            IReadOnlyCollection<TEntity9>? entries9)
            where TEntity1 : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
            where TEntity5 : class
            where TEntity6 : class
            where TEntity7 : class
            where TEntity8 : class
            where TEntity9 : class
            where TEntityContext : DbContext
        {
            var name = Guid.NewGuid().ToString();
            var dbContextOptions = new DbContextOptionsBuilder<TEntityContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;

            using (var entityContext = (TEntityContext?)Activator.CreateInstance(typeof(TEntityContext), dbContextOptions))
            {
                if (entityContext == null)
                {
                    var errorMessage = string.Format(ErrorMessageEentityContext, typeof(TEntityContext).Name);
                    throw new InvalidOperationException(errorMessage);
                }

                var propertiesContext = typeof(TEntityContext).GetProperties();

                foreach (var propertyInfo in propertiesContext)
                {
                    if (propertyInfo.PropertyType.GenericTypeArguments.Length > 0)
                    {
                        var entityType = propertyInfo.PropertyType.GenericTypeArguments[0];
                        if (entries9 != null &&
                            entityType == typeof(TEntity9))
                        {
                            LoadEntities(entityContext, propertyInfo, entries9);
                        }
                        else if (entries8 != null &&
                            entityType == typeof(TEntity8))
                        {
                            LoadEntities(entityContext, propertyInfo, entries8);
                        }
                        else if (entries7 != null &&
                            entityType == typeof(TEntity7))
                        {
                            LoadEntities(entityContext, propertyInfo, entries7);
                        }
                        else if (entries6 != null &&
                            entityType == typeof(TEntity6))
                        {
                            LoadEntities(entityContext, propertyInfo, entries6);
                        }
                        else if (entries5 != null &&
                            entityType == typeof(TEntity5))
                        {
                            LoadEntities(entityContext, propertyInfo, entries5);
                        }
                        else if (entries4 != null &&
                            entityType == typeof(TEntity4))
                        {
                            LoadEntities(entityContext, propertyInfo, entries4);
                        }
                        else if (entries3 != null &&
                                 entityType == typeof(TEntity3))
                        {
                            LoadEntities(entityContext, propertyInfo, entries3);
                        }
                        else if (entries2 != null &&
                                 entityType == typeof(TEntity2))
                        {
                            LoadEntities(entityContext, propertyInfo, entries2);
                        }
                        else if (entries1 != null &&
                                 entityType == typeof(TEntity1))
                        {
                            LoadEntities(entityContext, propertyInfo, entries1);
                        }
                    }
                }
            }

            return dbContextOptions;
        }

        private static void LoadEntities<TEntityContext, TEntity>(
            TEntityContext entityContext,
            PropertyInfo propertyInfo,
            IReadOnlyCollection<TEntity> entries)
            where TEntity : class
            where TEntityContext : DbContext
        {
            var dbSet = (DbSet<TEntity>?)propertyInfo.GetValue(entityContext);

            if (dbSet == null)
            {
                var errorMessage = string.Format(ErrorMessageEntity, typeof(TEntityContext).Name, typeof(TEntity).Name);
                throw new InvalidOperationException(errorMessage);
            }

            foreach (var entry in entries)
            {
                dbSet.Add(entry);
            }

            entityContext.SaveChanges();
        }
    }
}
