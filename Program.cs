//*****************************************************************************
//** 2901. Longest Unequal Adjacent Groups Subsequence II           leetcode **
//*****************************************************************************

/**
 * Note: The returned array must be malloced, assume caller calls free().
 */
char** getWordsInLongestSubsequence(char** words, int wordsSize, int* groups, int groupsSize, int* returnSize) {
    int** hashbrown = (int**)malloc(sizeof(int*) * 3);
    hashbrown[0] = (int*)malloc(sizeof(int) * wordsSize); 
    hashbrown[1] = (int*)malloc(sizeof(int) * wordsSize); 
    hashbrown[2] = (int*)malloc(sizeof(int) * wordsSize); 

    int* f = hashbrown[0];
    int* g = hashbrown[1];
    int* stk = hashbrown[2];

    int i, j, k;
    for (i = 0; i < wordsSize; i++)
    {
        f[i] = 1;
        g[i] = -1;
    }

    int mx = 1;
    int mxIndex = 0;

    for (i = 0; i < wordsSize; i++)
    {
        for (j = 0; j < i; ++j)
        {
            if (groups[i] != groups[j])
            {
                char *s = words[i], *t = words[j];
                int cnt = 0;
                for (k = 0; s[k] && t[k]; k++)
                {
                    cnt += (s[k] != t[k]);
                    if (cnt > 1)
                        break;
                }
                if (s[k] || t[k])
                    continue;

                if (cnt == 1 && f[i] < f[j] + 1)
                {
                    f[i] = f[j] + 1;
                    g[i] = j;
                    if (f[i] > mx)
                    {
                        mx = f[i];
                        mxIndex = i;
                    }
                }
            }
        }
    }

    *returnSize = mx;
    char** ans = (char**)malloc(sizeof(char*) * mx);
    int top = 0;

    for (i = mxIndex; i != -1; i = g[i])
    {
        stk[top++] = i;
    }

    for (i = 0; i < mx; ++i)
    {
        ans[i] = words[stk[mx - 1 - i]];
    }

    free(hashbrown[0]);
    free(hashbrown[1]);
    free(hashbrown[2]);
    free(hashbrown);
    return ans;
}