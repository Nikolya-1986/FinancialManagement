namespace FinancialManagement.Helpers
{
    public static class Rolehelper
    {
        public static string GetRolesDisplay(List<string> roles)
        {
            return roles != null && roles.Any()
                ? string.Join(", ", roles)
                : "Roles not assigned";
        }

        public static string GetRoleClass(List<string> roles)
        {
            return roles != null && roles.Any()
                ? roles.First().ToLower() switch
                {
                    "admin" => "role-admin",
                    "manager" => "role-manager",
                    _ => "role-default"
                }
                : "role-default";
        }
    }
}