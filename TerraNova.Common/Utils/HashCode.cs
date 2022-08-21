namespace TerraNova.Common.Utils
{
    public struct HashCodeBuilder
    {
        private int iHash;

        public static HashCodeBuilder Create()
        {
            return new HashCodeBuilder()
            {
                iHash = 17
            };
        }

        public HashCodeBuilder Add<T>(T value)
        {
            iHash = iHash * 31 + value.GetHashCode();

            return this;
        }

        public override int GetHashCode()
        {
            return iHash;
        }
    }
}