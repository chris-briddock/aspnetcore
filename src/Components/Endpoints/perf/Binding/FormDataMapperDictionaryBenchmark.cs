// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Components.Endpoints.Binding;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.Components.Binding;

public class FormDataMapperDictionaryBenchmark
{
    private FormDataMapperOptions _formMapperOptions;
    private Dictionary<string, StringValues> _formDataEntries;
    private FormDataReader _formDataReader;

    [Params(0, 1, 10, 100, 1000)]
    public int DictionarySize { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _formMapperOptions = new FormDataMapperOptions();
        _formDataEntries = Enumerable.Range(0, DictionarySize)
            .ToDictionary(i => $"[{i}]", i => new StringValues(i.ToString(CultureInfo.InvariantCulture)));
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _formDataReader = new FormDataReader(_formDataEntries, CultureInfo.InvariantCulture);
    }

    [Benchmark]
    public Dictionary<int, int> MapPrimitiveTypeDictionary()
    {
        return FormDataMapper.Map<Dictionary<int, int>>(_formDataReader, _formMapperOptions);
    }
}
