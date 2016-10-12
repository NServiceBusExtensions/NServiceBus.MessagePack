using System;
using System.Reflection.Emit;

static class ConstructorDelegateBuilder
{
    public static Func<object> BuildConstructorFunc(Type type)
    {
        var constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor == null)
        {
            throw new Exception($"There is no empty constructor: {type.FullName}");
        }
        var dynamic = new DynamicMethod(string.Empty,
            type,
            Type.EmptyTypes,
            type);
        var il = dynamic.GetILGenerator();

        il.DeclareLocal(type);
        il.Emit(OpCodes.Newobj, constructor);
        il.Emit(OpCodes.Stloc_0);
        il.Emit(OpCodes.Ldloc_0);
        il.Emit(OpCodes.Ret);

        return (Func<object>)dynamic.CreateDelegate(typeof(Func<object>));
    }
}