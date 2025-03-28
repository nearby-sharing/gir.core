using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class InterfaceMethods
{
    public static string Render(GirModel.Interface iface)
    {
        return $@"
using System;
using GObject;
using System.Runtime.InteropServices;

#nullable enable

namespace {Namespace.GetPublicName(iface.Namespace)};

// AUTOGENERATED FILE - DO NOT MODIFY

public partial interface {iface.Name} : IDisposable
{{
    {iface.Methods
        .Where(Method.IsEnabled)
        .Select(RenderMethodDefinition)
        .Join(Environment.NewLine)}
}}";
    }

    private static string RenderMethodDefinition(GirModel.Method method)
    {
        try
        {
            var parameters = ParameterToNativeExpression.Initialize(method.Parameters);

            return @$"
{VersionAttribute.Render(method.Version)}
{ReturnTypeRenderer.Render(method.ReturnType)} {Method.GetPublicName(method)}({RenderParameters(parameters)});";
        }
        catch (Exception ex)
        {
            Log.Warning($"Could not render interface method defintion {method.Name}: {ex.Message}");
            return string.Empty;
        }
    }

    private static string RenderParameters(IEnumerable<ParameterToNativeData> parameters)
    {
        var result = new List<string>();
        foreach (var parameter in parameters)
        {
            if (parameter.IsCallbackUserData)
                continue;

            if (parameter.IsDestroyNotify)
                continue;

            if (parameter.IsArrayLengthParameter)
                continue;

            if (parameter.IsGLibErrorParameter)
                continue;

            var typeData = ParameterRenderer.Render(parameter.Parameter);
            result.Add($"{typeData.Direction}{typeData.NullableTypeName} {parameter.GetSignatureName()}");
        }

        return result.Join(", ");
    }
}
