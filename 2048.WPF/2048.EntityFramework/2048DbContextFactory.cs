using Microsoft.EntityFrameworkCore;

namespace _2048.EntityFramework;

public class _2048DbContextFactory
{
    private readonly DbContextOptions _options;

    public _2048DbContextFactory(DbContextOptions options)
    {
        _options = options;
    }

    public _2048DbContext Create()
    {
        return new _2048DbContext(_options);
    }
}
