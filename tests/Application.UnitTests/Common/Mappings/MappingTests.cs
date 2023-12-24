#region

using System.Reflection;
using System.Runtime.Serialization;
using NUnit.Framework;
using TheHub.Application.Common.Models;
using TheHub.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using TheHub.Application.TodoLists.Queries.GetTodos;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.UnitTests.Common.Mappings;

public class MappingTests
{
   
    [Test]
    [TestCase(typeof(TodoList), typeof(TodoListDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemDto))]
    [TestCase(typeof(TodoList), typeof(LookupDto))]
    [TestCase(typeof(TodoItem), typeof(LookupDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemBriefDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        object instance = GetInstanceOf(source);
        HasExplicitConversion(source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
        {
            return Activator.CreateInstance(type)!;
        }

        // Type without parameterless constructor
        // TODO: Figure out an alternative approach to the now obsolete `FormatterServices.GetUninitializedObject` method.
#pragma warning disable SYSLIB0050 // Type or member is obsolete
        return FormatterServices.GetUninitializedObject(type);
#pragma warning restore SYSLIB0050 // Type or member is obsolete
    }
    public static bool HasExplicitConversion(Type fromType, Type toType)
    {
        return fromType.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Any(m => m.Name == "op_Explicit" && m.ReturnType == toType);
    }
}
