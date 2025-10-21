using System.Data;

namespace Phoenix.Domain;

public static class Consts
{
    public static readonly (int MinLength, int MaxLength, SqlDbType Type) SUBSCRIPTION_PLAN_TITLE = (2, 128, SqlDbType.NVarChar);
    public static readonly (int MinLength, int MaxLength, SqlDbType Type) SUBSCRIPTION_PLAN_DESCRIPTION = (2, 256, SqlDbType.NVarChar);
    public static readonly (int MinValue, long MaxValue, SqlDbType Type) SUBSCRIPTION_PLAN_PRICE = (10000, 100000000, SqlDbType.Decimal);
    public static readonly (int MinValue, int MaxValue, SqlDbType Type) SUBSCRIPTION_PLAN_DURATION = (1, 365, SqlDbType.Int);
    public static readonly (int MinLength, int MaxLength, SqlDbType Type) USER_USERNAME = (2, 128, SqlDbType.NVarChar);
    public static readonly (int MinLength, int MaxLength, SqlDbType Type) EMAIL = (6, 100, SqlDbType.VarChar);

}