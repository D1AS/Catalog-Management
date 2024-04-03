﻿namespace Packs.Api;

public static class ApiEndPoints
{
    private const string ApiBase = "api";

    public static class Packs
    {
        private const string Base = $"{ApiBase}/packs";

        public const string Create = Base;

        public const string Get = $"{Base}/{{id}}";

        public const string GetAll = Base;

        public const string Update = $"{Base}/{{id}}";
    }
}
