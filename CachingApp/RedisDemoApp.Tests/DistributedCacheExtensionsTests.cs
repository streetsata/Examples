using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Caching.Distributed;
using RedisDemoApp.Extensions;
using System.Text.Json;
using System;

namespace DistributedCacheExtensions.Tests;
public class DistributedCacheExtensionsTests
{
    private readonly Mock<IDistributedCache> _mockDistributedCache;

    public DistributedCacheExtensionsTests()
    {
        _mockDistributedCache = new Mock<IDistributedCache>();
    }

    [Fact]
    public async Task SetRecordAsync_SetsRecordInCache()
    {
        // Arrange
        var recordId = "testRecord";
        var data = new { TestProperty = "TestValue" };
        var jsonData = JsonSerializer.Serialize(data);
        _mockDistributedCache.Setup(x => x.SetStringAsync(recordId, jsonData, It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<System.Threading.CancellationToken>()));

        // Act
        await _mockDistributedCache.Object.SetRecordAsync(recordId, data);

        // Assert
        _mockDistributedCache.Verify(x => x.SetStringAsync(recordId, jsonData, It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<System.Threading.CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetRecordAsync_ReturnsRecordFromCache_WhenRecordExists()
    {
        // Arrange
        var recordId = "testRecord";
        var data = new { TestProperty = "TestValue" };
        var jsonData = JsonSerializer.Serialize(data);
        _mockDistributedCache.Setup(x => x.GetStringAsync(recordId, It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(jsonData);

        // Act
        var result = await _mockDistributedCache.Object.GetRecordAsync<dynamic>(recordId);

        // Assert
        result.Should().BeEquivalentTo(data);
    }

    [Fact]
    public async Task GetRecordAsync_ReturnsDefault_WhenRecordDoesNotExist()
    {
        // Arrange
        var recordId = "testRecord";
        _mockDistributedCache.Setup(x => x.GetStringAsync(recordId, It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync((string)null);

        // Act
        var result = await _mockDistributedCache.Object.GetRecordAsync<dynamic>(recordId);

        // Assert
        result.Should().BeNull();
    }
}