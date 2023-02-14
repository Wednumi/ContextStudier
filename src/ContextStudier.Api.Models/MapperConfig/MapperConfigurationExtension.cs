using AutoMapper;
using System.ComponentModel;

namespace ContextStudier.Api.Models.MapperConfig
{
    public static class MapperConfigurationExtension
    {
        public static IMappingExpression<TSource, TDestination> IgnoreNoMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var sorceProperties = sourceType.GetProperties();

            var destinationType = typeof(TDestination);
            var destinationProperties = destinationType.GetProperties();

            var sharedProperties = sorceProperties.Intersect(destinationProperties);
            foreach (var property in sharedProperties)
            {
                PropertyDescriptor? descriptor = TypeDescriptor.GetProperties(destinationType)[property.Name];
                if (descriptor is null)
                {
                    continue;
                }
                NoMapAttribute? attribute = (NoMapAttribute?)descriptor.Attributes[typeof(NoMapAttribute)];
                if (attribute is not null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
