namespace openhabUWP.Helper
{
    public static class StringFluent
    {
        public static bool IsNullOrEmpty(this string self)
        {
            if (string.IsNullOrEmpty(self)) return true;
            return string.IsNullOrEmpty(self.Trim());
        }
    }
}
