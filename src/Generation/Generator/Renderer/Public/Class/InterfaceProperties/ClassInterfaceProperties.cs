﻿using System;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassInterfaceProperties
{
    public static string Render(GirModel.Class cls)
    {
        return $@"
using System;
using GObject;
using System.Runtime.InteropServices;
#nullable enable
namespace {Namespace.GetPublicName(cls.Namespace)}
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public partial class {cls.Name}
    {{
        {cls.Implements.SelectMany(@interface => @interface.Properties
            .Where(Property.IsEnabled)
            .Select(prop => Render(@interface, prop)))
            .Join(Environment.NewLine)}
    }}
}}";
    }

    private static string Render(GirModel.Interface @interface, GirModel.Property property)
    {
        try
        {
            Property.ThrowIfNotSupported(@interface, property);

            var builder = new StringBuilder();
            builder.AppendLine(RenderAccessor(@interface, property));

            return builder.ToString();
        }
        catch (Exception ex)
        {
            var message = $"Did not generate property '{@interface.Name}.{property.Name}': {ex.Message}";

            if (ex is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }
}
