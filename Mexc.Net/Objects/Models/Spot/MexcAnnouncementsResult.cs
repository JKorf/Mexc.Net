using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Announcements
/// </summary>
public record MexcAnnouncements
{
    /// <summary>
    /// ["<c>details</c>"] Details
    /// </summary>
    [JsonPropertyName("details")]
    public MexcAnnouncement[] Announcements { get; set; } = [];
    /// <summary>
    /// ["<c>totalPage</c>"] Total pages
    /// </summary>
    [JsonPropertyName("totalPage")]
    public int Pages { get; set; }
}

/// <summary>
/// Announcement
/// </summary>
public record MexcAnnouncement
{
    /// <summary>
    /// ["<c>title</c>"] Title
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>url</c>"] Url
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>language</c>"] Language
    /// </summary>
    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>postTime</c>"] Post time
    /// </summary>
    [JsonPropertyName("postTime")]
    public DateTime PostTime { get; set; }
}

