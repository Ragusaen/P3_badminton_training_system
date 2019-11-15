using System;
using System.Collections.Generic;
using System.Text;

namespace application.Controller
{
    static class StringSearch
    {
        public static int LongestCommonSubsequence(string listText, string searchText)
        {
            int n = listText.Length, m = searchText.Length;
            int[,] M = new int[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
            {
                M[i, 0] = 0;
            }
            for (int j = 0; j <= m; j++)
            {
                M[0, j] = 0;
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (listText[i - 1] == searchText[j - 1])
                    {
                        M[i, j] = M[i - 1, j - 1] + 1;
                    }
                    else
                        M[i, j] = Math.Max(M[i, j - 1], M[i - 1, j]);
                }
            }
            return M[n, m];
        }
    }
}
