using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionAnswerer {
    private static char[] opreators = { '+', '-', '*', '/' };
    // 中缀表达式转后缀表达式，运算符的相对次序会变化，如 A  op1 (B op2 (C op3 D)) => ABCD op3 op2 op1
    private static int[][] operatorOrder = new int[5][] { new int[] { 0, 1, 2 }, new int[] { 1, 0, 2 }, new int[] { 2, 1, 0 }, new int[] { 1, 2, 0 }, new int[] { 0, 2, 1 } };
    private static string[] patternStringPrefix = {
            "(({0} # {1}) # {2}) # {3}",
            "({0} # ({1} # {2})) # {3}",
            "{0} # ({1} # ({2} # {3}))",
            "{0} # (({1} # {2}) # {3})",
            "({0} # {1}) # ({2} # {3})"
        };
    private static string[] patternStringSuffix = {
            "{0} {1} # {2} # {3} #",
            "{0} {1} {2} # # {3} #",
            "{0} {1} {2} {3} # # #",
            "{0} {1} {2} # {3} # #",
            "{0} {1} # {2} {3} # #"
        };

    private List<int> numList;
    private int[,,,] duplicateFlag = new int[14, 14, 14, 14];
    private List<string> ansList = new List<string>();
    public List<string> run(int[] nums) {
        numList = new List<int>(nums);
        dfs(0);
        return ansList;
    }
    // (4-(4/7))*7
    private float calc(float a, float b, char c) {
        switch (c) {
            case '+':
                return a + b;
            case '-':
                return a - b;
            case '*':
                return a * b;
            case '/':
                return a % b == 0 ? a / b : a / (float)b;
            default:
                return 0;
        }
    }

    private float calcRPN(string expression) {
        string[] RPN = expression.Split(' ');
        Stack<float> num = new Stack<float>();
        for (int i = 0; i < RPN.Length; i++) {
            string s = RPN[i];
            if (s[0] >= '0' && s[0] <= '9') {
                num.Push(float.Parse(s));
            } else {
                float a = num.Pop(), b = num.Pop();
                float res = calc(b, a, s[0]);
                num.Push(res);
            }
        }
        return num.Peek();
    }

    private void search(int k, int n1, int n2, int n3, int n4) {
        string stringSuffix = string.Format(patternStringSuffix[k], n1, n2, n3, n4);
        string stringPrefix = string.Format(patternStringPrefix[k], n1, n2, n3, n4);
        for (int i = 0; i < 3; i++) {
            stringSuffix = Tools.replaceFirst(stringSuffix, "#", "{" + i.ToString() + "}");
            stringPrefix = Tools.replaceFirst(stringPrefix, "#", "{" + i.ToString() + "}");
        }

        foreach (char op1 in opreators) {
            foreach (char op2 in opreators) {
                foreach (char op3 in opreators) {
                    string expression = string.Format(stringSuffix, op1, op2, op3);
                    try {
                        float ans = calcRPN(expression);
                        char[] op = new char[3] { op1, op2, op3 };
                        if (Math.Abs(ans - 24) < 0.0001f) {
                            ansList.Add(string.Format(stringPrefix, op[operatorOrder[k][0]], op[operatorOrder[k][1]], op[operatorOrder[k][2]]));
                        }
                    } catch (Exception e) {
                        // do nothing
                    }

                }
            }
        }
    }

    private int[] curList = new int[4];
    private bool[] curFlag = new bool[4] { false, false, false, false };
    private void dfs(int cur) {
        if (cur == 4) {
            if (duplicateFlag[curList[0], curList[1], curList[2], curList[3]] == 0) {
                duplicateFlag[curList[0], curList[1], curList[2], curList[3]] = 1;
                //Console.WriteLine(curList[0] + " " + curList[1] + " " + curList[2] + " " + curList[3] + " ");
                for (int i = 0; i < 5; i++) {
                    search(i, curList[0], curList[1], curList[2], curList[3]);
                }
            }
        }

        for (int i = 0; i < 4; i++) {
            if (!curFlag[i]) {
                curList[cur] = numList[i];
                curFlag[i] = true;
                dfs(cur + 1);
                curFlag[i] = false;
            }
        }
    }
}

