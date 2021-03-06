﻿using Bssom.Serializer.Binary;
using Bssom.Serializer.BssMap;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Bssom.Serializer.Internal
{
    internal static class CommonExpressionMeta
    {
        //Parameter: ref BssomReader reader
        public static readonly ParameterExpression Par_Reader;
        //Parameter: ref BssomWriter writer
        public static readonly ParameterExpression Par_Writer;

        public static readonly MethodInfo Type_Buffer_Seek;
        public static readonly PropertyInfo Type_Reader_Buffer;
        //Field: reader.Buffer
        public static readonly Expression Field_ReaderBuffer;
        //Field: writer.BufferWriter
        public static readonly Expression Field_WriterBufferWriter;

        //Parameter: ref BssomSerializeContext context
        public static readonly ParameterExpression Par_SerializeContext;
        //Parameter: ref BssomDeserializeContext context
        public static readonly ParameterExpression Par_DeserializeContext;
        //Parameter: ref BssomSizeContext context
        public static readonly ParameterExpression Par_SizeContext;

        public static readonly PropertyInfo Type_SerializeContext_Option;
        public static readonly PropertyInfo Type_DeserializeContext_Option;
        public static readonly PropertyInfo Type_SizeContext_Option;

        //Field: serializeContext.Option
        public static readonly Expression Field_SerializeContext_Option;
        //Field: dserializeContext.Option
        public static readonly Expression Field_DeserializeContext_Option;
        //Field: sizeContext.Option
        public static readonly Expression Field_SizeContext_Option;

        //Type: option.Security
        public static readonly PropertyInfo Type_OptionSecurity;
        //Type: option.Security.DepthStep
        public static readonly MethodInfo Type_OptionSecurity_DepthStep;
        //Field: dserializeContext.Option.Security
        public static readonly MemberExpression Field_DeserializeContext_Option_Security;
        //Call: deserContext.option.Security.DepthStep(ref reader)
        public static readonly Expression Call_DeserializeContext_Option_Security_DepthStep;

        //Type: dserializeContext.Depth
        public static readonly PropertyInfo Type_DeserializeContext_Depth;
        //Call: context.Depth--;
        public static readonly Expression Call_DeserializeContext_Depth_DecrementAssign;

        //Field: deserializeContext.Option.Resolver
        public static readonly Expression DeserializeContextOptionResolver;
        //Field: serializeContext.Option.Resolver
        public static readonly Expression SerializeContextOptionResolver;
        //Field: sizeContext.Option.Resolver
        public static readonly Expression SizeContextOptionResolver;

        //Field: serializeContext.option.IsUseStandardDateTime
        public static readonly Expression Field_SerializeOption_IsUseStandardDateTime;
        //Field: sizeContext.option.IsUseStandardDateTime
        public static readonly Expression Field_SizeOption_IsUseStandardDateTime;

        //Filed: writer.Posting
        public static readonly MemberExpression Field_WriterPos;
        //Type: reader.TryReadNull
        public static readonly MethodInfo Type_TryReadNull;
        //Type: reader.SkipBlankCharacterAndReadBssomType()
        public static readonly MethodInfo Type_SkipBlankCharacterAndReadBssomType;
        //Call: reader.TryReadNull()
        public static readonly Expression Call_Reader_TryReadNull;
        //Type: reader.TryReadNullWithEnsureBuildInType()
        public static readonly MethodInfo Type_Reader_TryReadNullWithEnsureBuildInType;
        //Type: reader.SkipObject()
        public static readonly MethodInfo Type_Reader_SkipObject;

        //Call: reader.ReadVariableNumber()
        public static readonly Expression Call_Reader_ReadVariableNumber;
        //Call: reader.SkipVariableNumber()
        public static readonly Expression Call_Reader_SkipVariableNumber;
        //Call: writer.WriteNull();
        public static readonly Expression Call_Writer_WriteNull;

        //Type: MapHeadPackInfo.Create()
        public static readonly MethodInfo Type_MapHeadPackInfo_Create;
        //Type: MapHeadPackInfo.DataEndPosition
        public static readonly PropertyInfo Type_MapHeadPackInfo_DataEndPosition;
        //Type: MapHeadPackInfo.MapHead
        public static readonly FieldInfo Type_MapHeadPackInfo_MapHead;
        //Type: MapHead.Count
        public static readonly FieldInfo Type_MapHead_Count;
        //Type: MapHead.RouteLength
        public static readonly FieldInfo Type_MapHead_RouteLength;
        //Type: Buffer.TryReadFixedRef()
        public static readonly MethodInfo Type_Buffer_TryReadFixedRef;
        //Type: Buffer.UnFixed()
        public static readonly MethodInfo Type_Buffer_UnFixed;
        //Type: Writer.WriteArray3Header()
        public static readonly MethodInfo Type_Writer_WriteArray3Header;
        //Type: Writer.WriteBackArray3Header()
        public static readonly MethodInfo Type_Writer_WriteBackArray3Header;

        //Call: reader.GetMapStringKeyLength();
        public static readonly Expression Call_ReaderGetMapStringKeyLength;
        //Call: reader.SkipObject();
        public static readonly Expression Call_Reader_SkipObject;


        private const BindingFlags instanceAndInternalFlag = BindingFlags.NonPublic | BindingFlags.Instance;

        static CommonExpressionMeta()
        {

            Par_Reader = Expression.Parameter(typeof(BssomReader).MakeByRefType(), "reader");
            Par_Writer = Expression.Parameter(typeof(BssomWriter).MakeByRefType(), "writer");

            Type_Buffer_Seek = typeof(IBssomBuffer).GetMethod(nameof(IBssomBuffer.Seek));
            Type_Reader_Buffer = typeof(BssomReader).GetProperty(nameof(BssomReader.BssomBuffer));
            Field_ReaderBuffer = Expression.Property(Par_Reader, Type_Reader_Buffer);
            PropertyInfo Type_Writer_Buffer = typeof(BssomWriter).GetProperty(nameof(BssomWriter.BufferWriter));
            Field_WriterBufferWriter = Expression.Property(Par_Writer, Type_Writer_Buffer);

            Par_SerializeContext = Expression.Parameter(typeof(BssomSerializeContext).MakeByRefType());
            Par_DeserializeContext = Expression.Parameter(typeof(BssomDeserializeContext).MakeByRefType());
            Par_SizeContext = Expression.Parameter(typeof(BssomSizeContext).MakeByRefType());

            Type_SerializeContext_Option = typeof(BssomSerializeContext).GetProperty(nameof(BssomSerializeContext.Option));
            Type_DeserializeContext_Option = typeof(BssomDeserializeContext).GetProperty(nameof(BssomDeserializeContext.Option));
            Type_SizeContext_Option = typeof(BssomSizeContext).GetProperty(nameof(BssomSizeContext.Option));

            Field_SerializeContext_Option =
                Expression.Property(Par_SerializeContext, Type_SerializeContext_Option);
            Field_DeserializeContext_Option =
                Expression.Property(Par_DeserializeContext, Type_DeserializeContext_Option);
            Field_SizeContext_Option =
                Expression.Property(Par_SizeContext, Type_SizeContext_Option);

            Type_OptionSecurity = typeof(BssomSerializerOptions).GetProperty(nameof(BssomSerializerOptions.Security));
            Type_OptionSecurity_DepthStep = typeof(BssomSecurity).GetMethod(nameof(BssomSecurity.DepthStep));
            Field_DeserializeContext_Option_Security = Expression.Property(Field_DeserializeContext_Option, Type_OptionSecurity);
            Call_DeserializeContext_Option_Security_DepthStep = Expression.Call(Field_DeserializeContext_Option_Security, Type_OptionSecurity_DepthStep, Par_DeserializeContext);

            Type_DeserializeContext_Depth = typeof(BssomDeserializeContext).GetProperty(nameof(BssomDeserializeContext.Depth));
            var field_DeserializeContext_Depth = Expression.Property(Par_DeserializeContext, Type_DeserializeContext_Depth);
            Call_DeserializeContext_Depth_DecrementAssign = Expression.Assign(field_DeserializeContext_Depth, Expression.Subtract(field_DeserializeContext_Depth, Expression.Constant(1)));

            PropertyInfo optionResolverType = typeof(BssomSerializerOptions).GetProperty(nameof(BssomSerializerOptions.FormatterResolver));
            DeserializeContextOptionResolver = Expression.Property(Field_DeserializeContext_Option, optionResolverType);
            SerializeContextOptionResolver = Expression.Property(Field_SerializeContext_Option, optionResolverType);
            SizeContextOptionResolver = Expression.Property(Field_SizeContext_Option, optionResolverType);

            Field_SerializeOption_IsUseStandardDateTime = Expression.Property(Field_SerializeContext_Option, typeof(BssomSerializerOptions).GetProperty(nameof(BssomSerializerOptions.IsUseStandardDateTime)));
            Field_SizeOption_IsUseStandardDateTime = Expression.Property(Field_SizeContext_Option, typeof(BssomSerializerOptions).GetProperty(nameof(BssomSerializerOptions.IsUseStandardDateTime)));

            Field_WriterPos = Expression.Property(Par_Writer, typeof(BssomWriter).GetProperty(nameof(BssomWriter.Position), BindingFlags.Public | BindingFlags.Instance));
            Type_TryReadNull = typeof(BssomReader).GetMethod(nameof(BssomReader.TryReadNull));
            Type_SkipBlankCharacterAndReadBssomType = typeof(BssomReader).GetMethod(nameof(BssomReader.SkipBlankCharacterAndReadBssomType), instanceAndInternalFlag);
            Call_Reader_TryReadNull = Expression.Call(Par_Reader, Type_TryReadNull);
            Type_Reader_TryReadNullWithEnsureBuildInType = typeof(BssomReader).GetMethod(nameof(BssomReader.TryReadNullWithEnsureBuildInType), instanceAndInternalFlag);
            Type_Reader_SkipObject = typeof(BssomReader).GetMethod(nameof(BssomReader.SkipObject), instanceAndInternalFlag, null, new Type[] { }, null);

            Call_Reader_ReadVariableNumber = Expression.Call(Par_Reader, typeof(BssomReader).GetMethod(nameof(BssomReader.ReadVariableNumber)));
            Call_Reader_SkipVariableNumber = Expression.Call(Par_Reader, typeof(BssomReader).GetMethod(nameof(BssomReader.SkipVariableNumber), instanceAndInternalFlag));
            Call_Writer_WriteNull = Expression.Call(Par_Writer, typeof(BssomWriter).GetMethod(nameof(BssomWriter.WriteNull)));
            Type_MapHeadPackInfo_Create = typeof(BssMapHeadPackInfo).GetMethod(nameof(BssMapHeadPackInfo.Create));
            Type_MapHeadPackInfo_DataEndPosition =
                typeof(BssMapHeadPackInfo).GetProperty(nameof(BssMapHeadPackInfo.DataEndPosition));
            Type_MapHeadPackInfo_MapHead = typeof(BssMapHeadPackInfo).GetField(nameof(BssMapHeadPackInfo.MapHead));
            Type_MapHead_Count = typeof(BssMapHead).GetField(nameof(BssMapHead.ElementCount));
            Type_MapHead_RouteLength = typeof(BssMapHead).GetField(nameof(BssMapHead.RouteLength));
            Type_Buffer_TryReadFixedRef = typeof(IBssomBuffer).GetMethod(nameof(IBssomBuffer.TryReadFixedRef));
            Type_Buffer_UnFixed = typeof(IBssomBuffer).GetMethod(nameof(IBssomBuffer.UnFixed));
            Type_Writer_WriteArray3Header = typeof(BssomWriter).GetMethod(nameof(BssomWriter.WriteArray3Header), instanceAndInternalFlag);
            Type_Writer_WriteBackArray3Header = typeof(BssomWriter).GetMethod(nameof(BssomWriter.WriteBackArray3Header), instanceAndInternalFlag);

            Call_ReaderGetMapStringKeyLength = Expression.Call(Par_Reader, typeof(BssomReader).GetMethod(nameof(BssomReader.GetMapStringKeyLength), instanceAndInternalFlag));

            Call_Reader_SkipObject = Expression.Call(Par_Reader, Type_Reader_SkipObject);
        }

        //Call: MapFormatterHelper.Deserialize(ref reader,option)
        public static Expression Call_MapFormatterHelper_Deserialize(Type keyType, Type valueType)
        {
            return Expression.Call(null, typeof(MapFormatterHelper).GetMethod(nameof(MapFormatterHelper.Deserialize)).MakeGenericMethod(keyType, valueType), Par_Reader, Par_DeserializeContext);
        }

        //Call: writer.WriteFixUInt32WithRefPos(uint _ , long pos)
        public static Expression Call_WriteFixUInt32WithRefPos(Expression value, Expression refb)
        {
            return Expression.Call(Par_Writer, typeof(BssomWriter).GetMethod(nameof(BssomWriter.WriteFixUInt32WithRefPos), instanceAndInternalFlag, null, new Type[] { value.Type, typeof(long) }, null), value, refb);
        }

        //Call: writer.Buffer.Seek(pos)
        public static Expression Call_Writer_Seek(Expression pos)
        {
            return Expression.Call(Field_WriterBufferWriter, typeof(IBssomBufferWriter).GetMethod(nameof(IBssomBufferWriter.Seek)), pos, Expression.Constant(BssomSeekOrgin.Begin, typeof(BssomSeekOrgin)));
        }

        //Call: reader.Buffer.Seek(pos)
        public static Expression Call_Reader_BufferSeek(Expression pos, BssomSeekOrgin orgin = BssomSeekOrgin.Begin)
        {
            return Expression.Call(Field_ReaderBuffer, Type_Buffer_Seek, pos, Expression.Constant(orgin, typeof(BssomSeekOrgin)));
        }

        //reader.TryReadNullWithEnsureBuildInType(BssomType.Array2)
        public static Expression Call_Reader_TryReadNullWithEnsureBuildInType(byte code)
        {
            return Expression.Call(Par_Reader, Type_Reader_TryReadNullWithEnsureBuildInType, Expression.Constant(code, typeof(byte)));
        }

        //reader.TryReadNullWithEnsureArray1BuildInType(BssomType.Int8Code)
        public static Expression Call_Reader_TryReadNullWithEnsureArray1BuildInType(byte code)
        {
            return Expression.Call(Par_Reader, typeof(BssomReader).GetMethod(nameof(BssomReader.TryReadNullWithEnsureArray1BuildInType), instanceAndInternalFlag), Expression.Constant(code, typeof(byte)));
        }

        //reader.TryReadNullWithEnsureArray1NativeType(NativeBssomType.CharCode)
        public static Expression Call_Reader_TryReadNullWithEnsureArray1NativeType(byte code)
        {
            return Expression.Call(Par_Reader, typeof(BssomReader).GetMethod(nameof(BssomReader.TryReadNullWithEnsureArray1NativeType), instanceAndInternalFlag), Expression.Constant(code, typeof(byte)));
        }

        //Call: reader.ReadRaw64(ref remaining);
        public static Expression Call_Reader_ReadRaw64Ref(Expression remaining)
        {
            return Expression.Call(Par_Reader, typeof(BssomReader).GetMethod(nameof(BssomReader.ReadRaw64), instanceAndInternalFlag, null, new Type[] { typeof(int).MakeByRefType() }, null), remaining);
        }

        //Call: formatter.Serialize(ref writer,ref context, value)
        public static Expression Call_FormatterSerialize(Expression formatter, Expression value)
        {
            return Expression.Call(formatter, formatter.Type.GetMethod(nameof(IBssomFormatter<int>.Serialize)), Par_Writer, Par_SerializeContext, value);
        }

        //Call: formatter.Deserialize(ref reader,ref context)
        public static Expression Call_FormatterDeserialize(Expression formatter)
        {
            return Expression.Call(formatter, formatter.Type.GetMethod(nameof(IBssomFormatter<int>.Deserialize)), Par_Reader, Par_DeserializeContext);
        }

        //Call: formatter.Size(ref context, value)
        public static Expression Call_FormatterSize(Expression formatter, Expression value)
        {
            return Expression.Call(formatter, formatter.Type.GetMethod(nameof(IBssomFormatter<int>.Size)), Par_SizeContext, value);
        }

        //Call: resolver.GetFormatterWithVerify<formatterType>()
        public static Expression Call_GetFormatterWithVerify(Expression resolver, Type formatterType)
        {
            return Expression.Call(null, typeof(FormatterResolverExtensions).GetMethod(nameof(FormatterResolverExtensions.GetFormatterWithVerify), new Type[] { typeof(IFormatterResolver) }).MakeGenericMethod(formatterType), resolver);
        }

        //Call: deserContext.Option.Resolver.GetFormatterWithVerify<formatterType>().Deserialize(ref reader, ref context)
        public static Expression Call_DeserializeContextOptionResolver_GetFormatterWithVerify_Deserialize(Type type)
        {
            return Expression.Call(Call_GetFormatterWithVerify(DeserializeContextOptionResolver, type), typeof(IBssomFormatter<>).MakeGenericType(type).GetMethod(nameof(IBssomFormatter<int>.Deserialize)), Par_Reader, Par_DeserializeContext);
        }

        //Call: serContext.Option.resolver.GetFormatterWithVerify<formatterType>().Serialize(ref writer,ref context,value)
        public static Expression Call_SerializeContextOptionResolver_GetFormatterWithVerify_Serialize(Expression value)
        {
            return Expression.Call(Call_GetFormatterWithVerify(SerializeContextOptionResolver, value.Type), typeof(IBssomFormatter<>).MakeGenericType(value.Type).GetMethod(nameof(IBssomFormatter<int>.Serialize)), Par_Writer, Par_SerializeContext, value);
        }

        //Call: sizeContext.Option.resolver.GetFormatterWithVerify<formatterType>().Size(ref context, value,option)
        public static Expression Call_SizeContextOptionResolver_GetFormatterWithVerify_Size(Expression value)
        {
            return Expression.Call(Call_GetFormatterWithVerify(SizeContextOptionResolver, value.Type), typeof(IBssomFormatter<>).MakeGenericType(value.Type).GetMethod(nameof(IBssomFormatter<int>.Size)), Par_SizeContext, value);
        }

        //Call: writer.Write(val);
        public static Expression Call_WriteDateTime(Expression value)
        {
            return Expression.Call(Par_Writer, typeof(BssomWriter).GetMethod(nameof(BssomWriter.Write), instanceAndInternalFlag, null, new Type[] { typeof(DateTime), typeof(bool), typeof(bool) }, null), value, Field_SerializeOption_IsUseStandardDateTime, Expression.Constant(true));
        }

        //Call: BssomBinaryPrimitives.DateTimeSize(val);
        public static Expression Call_DateTimeSize()
        {
            return Expression.Call(null, typeof(BssomBinaryPrimitives).GetMethod(nameof(BssomBinaryPrimitives.DateTimeSize)), Field_SizeOption_IsUseStandardDateTime);
        }

        //Call: reader.Read{TypeName}(val);
        public static Expression Call_Read(string typeName)
        {
            return Expression.Call(Par_Reader, typeof(BssomReader).GetMethod("Read" + typeName, instanceAndInternalFlag, null, new Type[0], null));
        }

        //Call: writer.Write(val);
        public static Expression Call_Write(Expression value)
        {
            return Expression.Call(Par_Writer, typeof(BssomWriter).GetMethod(nameof(BssomWriter.Write), instanceAndInternalFlag, null, new Type[] { value.Type }, null), value);
        }

        //Call: writer.WriteRaw(val);
        public static Expression Call_WriteRaw(Expression value)
        {
            return Expression.Call(Par_Writer, typeof(BssomWriter).GetMethod(nameof(BssomWriter.WriteRaw), new Type[] { value.Type }), value);
        }

        //Call: writer.WriteArray3Header(count);
        public static Expression Call_WriteArray3Header(int count)
        {
            return Expression.Call(Par_Writer, Type_Writer_WriteArray3Header, Expression.Constant(count));
        }

        //Call: writer.WriteBackArray3Header
        public static Expression Call_WriteBackArray3Header(Expression basePosition, Expression offset1, int count)
        {
            return Expression.Call(Par_Writer, Type_Writer_WriteBackArray3Header, basePosition, offset1, Expression.Constant(count));
        }

        //Call: writer.WriteBuildInType(val);
        public static Expression Call_WriteBuildInType(Expression value)
        {
            return Expression.Call(Par_Writer, typeof(BssomWriter).GetMethod(nameof(BssomWriter.WriteBuildInType), new Type[] { value.Type }), value);
            //return Expression.Call(Par_Writer, typeof(BssomWriter).GetMethod(nameof(BssomWriter.WriteBuildInType), instanceAndInternalFlag, null, new Type[] { value.Type }, null), value);
        }

        //Block: If(t==null) { writeNull();return; }
        public static Expression Block_IfNullWriteNullWithReturn(Expression instance, Type t, LabelTarget label)
        {

            return Expression.IfThen(Expression.Equal(instance, Expression.Constant(null, t)), Expression.Block(Call_Writer_WriteNull, Expression.Return(label)));
        }

        //Block: If(t==null) { return 1; }
        public static Expression Block_IfNullSize(Expression instance, Type t, LabelTarget label)
        {
            return Expression.IfThen(Expression.Equal(instance, Expression.Constant(null, t)), Expression.Return(label, Expression.Constant(BssomBinaryPrimitives.NullSize)));
        }

        //Block: { reader = map.Reader; context = map.Context; }
        public static Expression Block_MapReaderAndContextAssignLocalReaderAndContext(Expression map)
        {
            return Expression.Block(
                Expression.Assign(Par_Reader, Expression.Property(map, nameof(IMapDataSource<int, int>.Reader))),
                Expression.Assign(Par_DeserializeContext, Expression.Property(map, nameof(IMapDataSource<int, int>.Context)))
                );
        }



        //Call: reader.SeekAndSkipObject(len);
        public static Expression Call_Reader_SeekAndSkipObject(Expression len)
        {
            return Expression.Call(Par_Reader, typeof(BssomReader).GetMethod(nameof(BssomReader.SeekAndSkipObject), instanceAndInternalFlag), len);
        }

        public static Expression GenerateInjectCtor(Type t, ConstructorInfo constructor, Type injectType)
        {
            LabelTarget returnTarget = Expression.Label(t, "returnLable");
            return Expression.Block(
              //if (reader.PeekIsNull())
              //      goto label;
              Expression.IfThen(CommonExpressionMeta.Call_Reader_TryReadNull, Expression.Return(returnTarget, Expression.Default(t))),
            //return new Ctor(context.option.FormatterResolver.GetFormatter<injectType>().Deserialize(ref reader,option));
              Expression.Return(returnTarget, Expression.New(constructor, CommonExpressionMeta.Call_DeserializeContextOptionResolver_GetFormatterWithVerify_Deserialize(injectType))),
              //label default(T)
              Expression.Label(returnTarget, Expression.Default(t))
              );
        }

    }

    internal static class ExpressionTreeAux
    {
        public static readonly MethodInfo AsPointerMethodType = typeof(ExpressionTreeAux).GetMethod(nameof(ExpressionTreeAux.AsPointer));

        public unsafe static IntPtr AsPointer<T>(ref T t)
        {
            return new IntPtr(Unsafe.AsPointer(ref t));
        }

        public static Expression AsPointerExpression(Expression instance)
        {
            var asPointerMethodType = typeof(ExpressionTreeAux).GetMethod(nameof(ExpressionTreeAux.AsPointer)).MakeGenericMethod(instance.Type);
            return Expression.Call(null, asPointerMethodType, instance);
        }
    }
}
