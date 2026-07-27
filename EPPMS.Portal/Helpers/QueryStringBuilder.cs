using System.Globalization;

namespace EPPMS.Portal.Helpers;

public sealed class QueryStringBuilder
{
    private readonly string _baseUrl;
    private readonly List<string> _parameters = [];

    private QueryStringBuilder(string baseUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseUrl);

        _baseUrl = baseUrl;
    }

    public static QueryStringBuilder Create(string baseUrl)
    {
        return new QueryStringBuilder(baseUrl);
    }

    public QueryStringBuilder Add(string key, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _parameters.Add(
                $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
        }

        return this;
    }

    public QueryStringBuilder Add(string key, Guid? value)
    {
        if (value.HasValue)
        {
            _parameters.Add(
                $"{Uri.EscapeDataString(key)}={value.Value}");
        }

        return this;
    }

    public QueryStringBuilder Add(string key, int? value)
    {
        if (value.HasValue)
        {
            _parameters.Add(
                $"{Uri.EscapeDataString(key)}={value.Value}");
        }

        return this;
    }

    public QueryStringBuilder Add(string key, long? value)
    {
        if (value.HasValue)
        {
            _parameters.Add(
                $"{Uri.EscapeDataString(key)}={value.Value}");
        }

        return this;
    }

    public QueryStringBuilder Add(string key, bool? value)
    {
        if (value.HasValue)
        {
            _parameters.Add(
                $"{Uri.EscapeDataString(key)}={value.Value.ToString().ToLowerInvariant()}");
        }

        return this;
    }

    public QueryStringBuilder Add(string key, DateTime? value)
    {
        if (value.HasValue)
        {
            _parameters.Add(
                $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.Value.ToString("O"))}");
        }

        return this;
    }

    public QueryStringBuilder Add(string key, decimal? value)
    {
        if (value.HasValue)
        {
            _parameters.Add(
                $"{Uri.EscapeDataString(key)}={value.Value.ToString(CultureInfo.InvariantCulture)}");
        }

        return this;
    }

    public string Build()
    {
        if (_parameters.Count == 0)
        {
            return _baseUrl;
        }

        return $"{_baseUrl}?{string.Join("&", _parameters)}";
    }
}