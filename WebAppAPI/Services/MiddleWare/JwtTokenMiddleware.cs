using Microsoft.Extensions.Caching.Memory;

public class JwtTokenMiddleware
{
	private readonly RequestDelegate _next;
	private readonly IMemoryCache _cache;

	public JwtTokenMiddleware(RequestDelegate next, IMemoryCache cache)
	{
		_next = next;
		_cache = cache;
	}

	public async Task Invoke(HttpContext context)
	{
		if (!context.Request.Headers.ContainsKey("Authorization"))
		{
			var username = context.User?.Identity?.Name;

			if (!string.IsNullOrEmpty(username))
			{
				// Пытаемся получить токен из кэша
				if (!_cache.TryGetValue(username, out string token))
				{
					// Токена нет в кэше, генерируем новый
					token = JwtTokenGenerator.GenerateJwtToken(username, "SECRET_KEY_SECRET_KEY_SECRET_KEY_");

					// Кэшируем токен
					_cache.Set(username, token, TimeSpan.FromHours(1));
				}

				// Добавляем токен в заголовок запроса
				context.Request.Headers.Add("Authorization", $"Bearer {token}");
			}
		}

		await _next(context);
	}
}