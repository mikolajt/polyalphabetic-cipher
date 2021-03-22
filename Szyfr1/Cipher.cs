using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szyfr1 {
    class Cipher {
        private string OrginalText { get; set; }
        private string Key { get; set; }
        private List<int> RightSkew { get; set; }
        private List<int> LeftSkew { get; set; }

        public Cipher(string text, string k, List<int> rs, List<int> ls) {
            OrginalText = text;
            Key = k;
            RightSkew = rs;
            LeftSkew = ls;
            RightSkew.Sort();
            LeftSkew.Sort();
        }

        public Cipher() { }

        private char?[] ChangeStringToCharList(string text) {
            char?[] list = new char?[text.Length];
            for (int i = 0; i < text.Length; i++) {
                list[i] = text[i];
            }
            return list;
        }

        private char?[] FillWithNulls(char?[] orginalText) {
            int modulo = orginalText.Length % Key.Length;
            char?[] nulls = new char?[modulo];
            if (modulo != 0) {
                for (int i = 0; i < modulo; i++) {
                    nulls[i] = null;
                }
            }
            int lineNumber = orginalText.Length / Key.Length;
            int lineModulo = lineNumber % 5;
            lineModulo *= Key.Length;
            char?[] lineNulls = new char?[lineModulo * Key.Length];
            if (lineModulo != 0) {
                for (int i = 0; i < lineModulo; i++) {
                    lineNulls[i] = null;
                }
            }
            List<char?> itemsList = orginalText.ToList<char?>();
            itemsList.AddRange(nulls);
            itemsList.AddRange(lineNulls);
            char?[] newArray = itemsList.ToArray();
            return newArray;
        }

        public Dictionary<int, char> SortKey(string key) {
            string alphabeth = "AaĄąBbCcĆćDdEeĘęFfGgHhIiJjKkLlŁłMmNnŃńOoÓóPpRrSsŚśTtUuWwQqXxYyZzŹźŻż";
            int count = 1;
            char[] charsInKey = new char[key.Length];
            int[] intsInKey = new int[key.Length];
            Dictionary<int, char> numberedKey = new Dictionary<int, char>();
            foreach (char letter in alphabeth) {
                for (int i = 0; i < key.Length; i++) {
                    if (letter == key[i]) {
                        charsInKey[i] = key[i];
                        intsInKey[i] = count;
                        count++;
                    }
                }
            }
            for (int i = 0; i < key.Length; i++) {
                numberedKey.Add(intsInKey[i], charsInKey[i]);
            }
            return numberedKey;
        }

        public List<int> GenerateDigits(string direction, string key) {
            List<int> digits = new List<int>();
            Dictionary<int, char> dictKey = new Dictionary<int, char>();
            dictKey = SortKey(key);
            if(direction == "r" || direction == "right" || direction == "R") {
                for(int i=0; i<key.Length; i++) {
                    if (i % 5 == 0) {
                        digits.Add(dictKey.ElementAt(i).Key);
                    }
                }
            }
            else if(direction == "l" || direction == "left" || direction == "L") {
                for (int i = 0; i < key.Length; i++) {
                    if (i % 5 == 3) {
                        digits.Add(dictKey.ElementAt(i).Key);
                    }
                }
            }
            return digits;
        }

        public string Encryption() {
            char?[] orginalText = new char?[OrginalText.Length];
            Dictionary<int, char> numberedKey = new Dictionary<int, char>();
            string result = "";
            int blocks;
            orginalText = ChangeStringToCharList(OrginalText);
            orginalText = FillWithNulls(orginalText);
            numberedKey = SortKey(Key);
            var sortedNumberedKey = numberedKey.OrderBy(x => x.Key);

            blocks = (OrginalText.Length / numberedKey.Count) / 5;

            for (int blockNumber = 0; blockNumber <= blocks; blockNumber++) {
                foreach (int skewNumber in RightSkew) {
                    for (int keyNumber = 0; keyNumber < numberedKey.Keys.Count; keyNumber++) {
                        if (numberedKey.Keys.ElementAt(keyNumber) == skewNumber) {
                            if (orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + numberedKey.Count + 1) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + numberedKey.Count + 1) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + numberedKey.Count + 1) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count + 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count + 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count + 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count + 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count + 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count + 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count + 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count + 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count + 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                        }
                    }

                }
                foreach (int skewNumber in LeftSkew) {
                    for (int keyNumber = 0; keyNumber < numberedKey.Keys.Count; keyNumber++) {
                        if (numberedKey.Keys.ElementAt(keyNumber) == skewNumber) {
                            if (orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + numberedKey.Count - 1) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + numberedKey.Count - 1) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + numberedKey.Count - 1) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count - 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count - 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count - 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count - 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count - 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count - 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count - 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count - 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count - 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                        }
                    }

                }
                for (int number = 1; number <= numberedKey.Count; number++) {
                    for (int keyNumber = 0; keyNumber < numberedKey.Keys.Count; keyNumber++) {
                        if (number == numberedKey.Keys.ElementAt(keyNumber)) {
                            if (orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[keyNumber + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + numberedKey.Count) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + numberedKey.Count) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + numberedKey.Count) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                            if (orginalText[(keyNumber + ((numberedKey.Count) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] != null) {
                                result += orginalText[(keyNumber + ((numberedKey.Count) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))];
                                orginalText[(keyNumber + ((numberedKey.Count) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = null;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
