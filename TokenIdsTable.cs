
namespace compiler
{
    class TokenIdsTable
    {
        public string[] tokenIds = new string[801];

        public string[] setTokens()
        {
            tokenIds[1] = "+";
            tokenIds[2] = "-";
            tokenIds[3] = "*";
            tokenIds[4] = "/";
            tokenIds[5] = "<";
            tokenIds[6] = "<=";
            tokenIds[7] = ">";
            tokenIds[8] = ">=";
            tokenIds[9] = "==";
            tokenIds[10] = "!=";
            tokenIds[11] = "=";
            tokenIds[21] = "else";
            tokenIds[22] = "if";
            tokenIds[33] = "int";
            tokenIds[44] = "return";
            tokenIds[55] = "void";
            tokenIds[66] = "while";
            tokenIds[77] = "input";
            tokenIds[88] = "output";
            tokenIds[100] = ";";
            tokenIds[200] = ",";
            tokenIds[300] = "(";
            tokenIds[400] = ")";
            tokenIds[500] = "[";
            tokenIds[600] = "]";
            tokenIds[700] = "{";
            tokenIds[800] = "}";
            tokenIds[501] = "/*";
            tokenIds[502] = "*/";
            tokenIds[123] = "identifier";
            tokenIds[456] = "integer constant";
            tokenIds[503] = "***COMMENT***";

            return tokenIds;
        }
    }
}
