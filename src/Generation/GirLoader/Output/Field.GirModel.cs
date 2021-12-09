﻿using OneOf;

namespace GirLoader.Output
{
    public partial class Field : GirModel.Field
    {
        string GirModel.Field.Name => OriginalName;
        bool GirModel.Field.IsReadable => Readable;
        bool GirModel.Field.IsPrivate => Private;
        bool GirModel.Field.IsPointer => TypeReference.CTypeReference?.IsPointer ?? false;

        OneOf<GirModel.AnyType, GirModel.Callback> GirModel.Field.AnyTypeOrCallback
        {
            get
            {
                if (Callback is not null)
                    return Callback;

                return TypeReference switch
                {
                    ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
                    _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
                };
            }
        }
    }

}
